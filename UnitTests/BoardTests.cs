using System;
using System.Collections.Generic;
using NUnit.Framework;
using SudokuSolver;

namespace UnitTests
{
	[TestFixture]
	public class BoardTests
	{
		[Test]
		public void Main()
		{
			// Invalid path
			string path = ".";
			Assert.Catch<Exception>(() => FileInterface.readFromFile(path));
			
			// Incorrect number of characters
			path = Constants.TEST_INPUT + @"\wrong_num_chars.txt";
			Assert.Catch<Exception>(() => FileInterface.readFromFile(path));
			
			// Working file
			path = Constants.TEST_INPUT + @"\working_file.txt";
			Board board = FileInterface.readFromFile(path);
			List<List<char>> expected = new List<List<char>>
			{
				new List<char> {'2', '-', '3', '1'},
				new List<char> {'1', '3', '-', '4'},
				new List<char> {'3', '1', '4', '-'},
				new List<char> {'-', '2', '1', '3'}
			};
			Assert.True(TestUtils.boardsEqual(expected, board.board));
			
			// Copy constructor
			Board otherBoard = new Board(board);
			Assert.True(TestUtils.boardsEqual(otherBoard.board, board.board));

			Assert.Catch<Exception>(() => board.setCell(100, 100, '1'));
			Assert.Catch<Exception>(() => board.setCell(0, 0, '0'));
			
			Assert.IsTrue(board.isValidChar('-'));
			Assert.IsTrue(board.isValidChar('1'));
			Assert.IsFalse(board.isValidChar('5'));
				
			board.printBoard();
		}
	}
}
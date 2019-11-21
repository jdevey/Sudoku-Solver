using System.Collections.Generic;
using NUnit.Framework;
using SudokuSolver;

namespace UnitTests
{
	[TestFixture]
	public class FileInterfaceTests
	{
		[Test]
		public void Main()
		{
			Assert.Catch(() => FileInterface.readFromFile("."));

			string path = Constants.TEST_INPUT + @"\file_too_short.txt";
			Assert.Catch(() => FileInterface.readFromFile(path));
			
			path = Constants.TEST_INPUT + @"\not_enough_lines.txt";
			Assert.Catch(() => FileInterface.readFromFile(path));

			path = Constants.TEST_INPUT + @"\long_characters.txt";
			Assert.Catch(() => FileInterface.readFromFile(path));
			
			path = Constants.TEST_INPUT + @"\duplicate_chars.txt";
			Assert.Catch(() => FileInterface.readFromFile(path));
			
			path = Constants.TEST_INPUT + @"\incorrect_line.txt";
			Assert.Catch(() => FileInterface.readFromFile(path));
			
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

			long micros = 1294829015890289018;
			Assert.AreEqual(FileInterface.getTimeString(micros),"26:38:10.289018");
			
			Tracker tracker = new Tracker(board);
			Solver solver = new Solver(tracker);
			FileInterface.writeToFile(board, board, solver, 23087);

			List <string> expectedLines = new List<string>
			{
				"2 - 3 1",
				"1 3 - 4",
				"3 1 4 -",
				"- 2 1 3"
			};
			var lines = FileInterface.getBoardFileLines(board);

			for (int i = 0; i < 4; ++i)
			{
				Assert.AreEqual(lines[i], expectedLines[i]);
			}
			
			List <char> lc = new List<char>{'a', 'b'};
			Assert.AreEqual(FileInterface.lineToString(lc), "a b");
		}
	}
}
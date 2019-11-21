using System.Collections.Generic;
using NUnit.Framework;
using SudokuSolver;

namespace UnitTests
{
	[TestFixture]
	public class SingleSquareTests
	{
		[Test]
		public void Main()
		{
			int n = 4;
			CharSet charSet = new CharSet(new List<char>{'0', '1', '2', '3'});
			Board board = new Board(n, charSet);
			board.setCell(0, 0, '-');
			Tracker tracker = new Tracker(board);
			tracker.progress[0][0].erase('0');
			tracker.progress[0][0].erase('1');
			tracker.progress[0][0].erase('2');
			SingleSquare singleSquare = new SingleSquare(0, 0);
			singleSquare.execute(tracker);
			Assert.AreEqual(board.board[0][0], '3');
		}
	}
}
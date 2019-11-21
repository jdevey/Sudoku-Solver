using System.Collections.Generic;
using NUnit.Framework;
using SudokuSolver;

namespace UnitTests
{
	[TestFixture]
	public class SingleRegionTests
	{
		[Test]
		public void Main()
		{
			int n = 4;
			CharSet charSet = new CharSet(new List<char>{'0', '1', '2', '3'});
			Board board = new Board(n, charSet);
			Tracker tracker = new Tracker(board);
			board.setCell(0, 0, '-');
			board.setCell(3, 3, '-');
			board.setCell(1, 2, '-');
			tracker.fillSquare(0, 0, '0');
			tracker.fillSquare(3, 3, '0');
			SingleRegion singleRegion = new SingleRegion(1, Constants.RegionTypes.BLOCK, '0');
			singleRegion.execute(tracker);
			Assert.AreEqual(board.board[1][2], '0');
		}
	}
}
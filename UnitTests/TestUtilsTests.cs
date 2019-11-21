using System.Collections.Generic;
using NUnit.Framework;
using SudokuSolver;

namespace UnitTests
{
	[TestFixture]
	public class TestUtilsTests
	{
		[Test]
		public void Main()
		{
			List<List<char>> a = new List<List<char>>
			{
				new List<char> {'1', '2'},
				new List<char> {'3', '4'}
			};
			List<List<char>> b = new List<List<char>>
			{
				new List<char> {'1', '2'},
				new List<char> {'3', '4'}
			};
			Assert.IsTrue(TestUtils.boardsEqual(a, b));
			a[0][0] = '0';
			Assert.IsFalse(TestUtils.boardsEqual(a, b));

			List<List<char>> c = new List<List<char>>
			{
				new List<char> {'1', '2'}
			};
			Assert.IsFalse(TestUtils.boardsEqual(a, c));

			Board board = null;
			Assert.Catch(() =>
			{
				board = new Board(4, new CharSet(new List<char> {'0', '1', '2'}));
			});
			Assert.IsNull(board);

		List<List<char>> complete = new List<List<char>>
			{
				new List<char> {'2', '4', '3', '1'},
				new List<char> {'1', '3', '2', '4'},
				new List<char> {'3', '1', '4', '2'},
				new List<char> {'4', '2', '1', '3'}
			};
			
			Board board1 = new Board(4, new CharSet(new List<char>{'0', '1', '2', '3'}), complete);
			
			Assert.IsTrue(TestUtils.isCorrectSolution(board1));

			complete[0][0] = '4';
			Assert.IsFalse(TestUtils.isCorrectSolution(board1));
			
			List<List<char>> badCols = new List<List<char>>
			{
				new List<char> {'2', '4', '3', '1'},
				new List<char> {'2', '4', '3', '1'},
				new List<char> {'2', '4', '3', '1'},
				new List<char> {'2', '4', '3', '1'}
			};
			Board board2 = new Board(4, new CharSet(new List<char>{'0', '1', '2', '3'}), badCols);
			Assert.IsFalse(TestUtils.isCorrectSolution(board2));
			
			List<List<char>> badRegions = new List<List<char>>
			{
				new List<char> {'2', '4', '3', '1'},
				new List<char> {'4', '3', '1', '2'},
				new List<char> {'3', '1', '2', '4'},
				new List<char> {'1', '2', '4', '3'}
			};
			Board board3 = new Board(4, new CharSet(new List<char>{'0', '1', '2', '3'}), badRegions);
			Assert.IsFalse(TestUtils.isCorrectSolution(board3));
		}
	}
}
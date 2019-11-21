using System;
using System.Collections.Generic;
using NUnit.Framework;
using SudokuSolver;

namespace UnitTests
{
	[TestFixture]
	public class TrackerTests
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

			Tracker otherTracker = new Tracker(tracker);
			Assert.IsTrue(TestUtils.boardsEqual(
				tracker.board.board, otherTracker.board.board));
			
			otherTracker.eliminatePossibility(1, 1, '0');
			otherTracker.eliminatePossibility(1, 1, '1');
			otherTracker.eliminatePossibility(1, 1, '2');
			otherTracker.eliminatePossibility(1, 1, '3');
			Assert.IsFalse(otherTracker.valid);
			
			tracker.fillSquare(0, 0);
			Assert.AreEqual(board.board[0][0], '3');
			
			tracker.fillSquare(2, 2, '2');
			Assert.AreEqual(board.board[2][2], '2');

			Tuple<int, int> fromBlock = tracker.convertFromBlock(2, 1);
			Assert.AreEqual(new Tuple <int, int>(2, 1), fromBlock);


//			string path = Constants.EASY_INPUT;
//			string[] inputFiles = Directory.GetFiles(path);
//			foreach (string file in inputFiles)
//			{
//				Board board;
//				try
//				{
//					board = FileInterface.readFromFile(file);
//				}
//				catch (Exception e)
//				{
//					Console.WriteLine(e.Message);
//					continue;
//				}
//				
//				Tracker tracker = new Tracker(board);
//				Solver solver = new Solver(tracker);
//				tracker = solver.run();
//				
//				if (!tracker.valid)
//				{
//					Assert.IsFalse(TestUtils.isCorrectSolution(tracker.board));
//				}
//				else
//				{
//					if (tracker.solutionCnt > 1)
//					{
//						Console.WriteLine("WARNING: More than one solution found. One is shown.");
//					}
//					Assert.IsTrue(TestUtils.isCorrectSolution(tracker.board),
//						"ERROR: Could not find correct solution for file " + file + ".");
//				}
//			}
		}	
	}
}
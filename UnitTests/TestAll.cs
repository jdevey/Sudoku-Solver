using System;
using System.IO;

using NUnit.Framework;
using SudokuSolver;

namespace UnitTests
{
	[TestFixture]
	public class TestAll
	{
		[Test]
		public void Main()
		{
			string path = Constants.EASY_INPUT;
			string[] inputFiles = Directory.GetFiles(path);
			foreach (string file in inputFiles)
			{
				Board board;
				try
				{
					board = FileInterface.readFromFile(file);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					continue;
				}
				
				Tracker tracker = new Tracker(board);
				Solver solver = new Solver(tracker);
				tracker = solver.run();
				
				if (!tracker.valid)
				{
					Assert.IsFalse(Utils.isCorrectSolution(tracker.board));
				}
				else
				{
					if (tracker.solutionCnt > 1)
					{
						Console.WriteLine("WARNING: More than one solution found. One is shown.");
					}
					Assert.IsTrue(Utils.isCorrectSolution(tracker.board),
						"ERROR: Could not find correct solution for file " + file + ".");
				}
			}
		}
	}
}
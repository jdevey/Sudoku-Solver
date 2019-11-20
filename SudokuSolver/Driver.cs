using System;
using System.Diagnostics;
using System.IO;

namespace SudokuSolver
{
	internal static class Driver
	{
		private const string helpString = "Sudoku solver arguments: \n" +
			"-h := View this help page\n" +
			"<input_file_name> := Read sudoku puzzle from <input_file_name> and output the result to the console\n" +
			"<input_file_name> <output_file_name> := Read sudoku puzzle from <input_file_name> and output the result to <output_file_name>\n";
		
		static void printHelp()
		{
			Console.Write(helpString);
		}
		
		static void Main(string[] args)
		{
			if (args.Length == 0 || args.Length == 1 && args[0] == "-h")
			{
				printHelp();
				return;
			}

			Board sudokuBoard = null;
			try
			{
				sudokuBoard = FileInterface.readFromFile(args[0]);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			Board initialBoard = new Board(sudokuBoard);
			
			Tracker tracker = new Tracker(sudokuBoard);
			Solver solver = new Solver(tracker);
			
			tracker = solver.run();

			if (!tracker.valid)
			{
				if (tracker.solutionCnt > 1)
				{
					Console.WriteLine("WARNING: More than one solution found. One is shown.");
				}
				else
				{
					Console.WriteLine("ERROR: Invalid puzzle.");
					return;
				}
			}
			
			if (!tracker.valid)
			{
				Console.WriteLine("ERROR: Invalid puzzle.");
				return;
			}
			
			if (tracker.solutionCnt > 1)
			{
				Console.WriteLine("WARNING: More than one solution found. One is shown.");
			}

			if (args.Length > 1)
			{
				using (var writer = new StreamWriter(args[1]))
				{
					Console.SetOut(writer);
					FileInterface.writeToFile(initialBoard, sudokuBoard, solver, solver.totalTimeElapsed);
				}
			}
			else
			{
				FileInterface.writeToFile(initialBoard, sudokuBoard, solver, solver.totalTimeElapsed);
			}
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
		}
	}
}
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
			
			long start = DateTime.Now.Ticks;
			solver.run(); // TODO return value unused
			long end = DateTime.Now.Ticks;
			long totalMicros = (end - start) / 10;
			
			
			// TODO print initial board, solved board, strategies used, and time
			if (args.Length > 1)
			{
				var writer = new StreamWriter(args[1]);
				Console.SetOut(writer);
			}
			FileInterface.writeToFile(initialBoard, sudokuBoard, solver, totalMicros);
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
		}
	}
}
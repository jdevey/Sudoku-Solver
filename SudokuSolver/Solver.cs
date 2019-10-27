using System;

namespace SudokuSolver
{
	internal static class Solver
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

			SudokuBoard sudokuBoard = FileInterface.readFromFile(args[0]);
			
			// TODO solve

			if (args.Length > 1)
			{
				FileInterface.writeToFile(sudokuBoard, args[1]);
			}
			else
			{
				foreach (string line in FileInterface.getSudokuBoardFileLines(sudokuBoard))
				{
					Console.WriteLine(line);
				}
			}
		}
	}
}
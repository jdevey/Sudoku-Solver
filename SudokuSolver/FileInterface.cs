using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SudokuSolver
{
	public static class FileInterface
	{
		public static Board readFromFile(string path)
		{
			if (!File.Exists(path))
			{
				throw new Exception("ERROR: Specified sudoku puzzle file does not exist: " + path);
			}

			string[] lines = File.ReadAllLines(path).Where(line => line.Length > 0).ToArray(); // Ignore empty lines
			if (lines.Length < 3)
			{
				throw new Exception("ERROR: Specified sudoku puzzle file does not have at least three lines: " + path);
			}
			
			int size = Int32.Parse(lines[0]);

			string[] characterStrings = lines[1].Split(null);

			if (lines.Length - 2 != size)
			{
				throw new Exception(
					"ERROR: Specified sudoku puzzle file puzzle size does not match the number of characters provided: " + path);
			}
			
			if (characterStrings.Any(characterString => characterString.Length != 1))
			{
				throw new Exception(
					"ERROR: Specified sudoku puzzle file does not have characters with length equal to one: " + path);
			}

			CharSet validChars = new CharSet();
			foreach (string characterString in characterStrings)
			{
				if (validChars.contains(characterString[0]))
				{
					throw new Exception(
						"ERROR: Specified sudoku puzzle file character header has duplicate characters: " + path);
				}
				validChars.insert(characterString[0]);
			}
			
			Board sudokuBoard = new Board(size, validChars);

			for (int i = 2; i < lines.Length; ++i)
			{
				List<char> row = lines[i].ToCharArray().Where(c => c != ' ').ToList();
				if (row.Count != size)
				{
					throw new Exception(
						"ERROR: Specified sudoku puzzle file contains one or more lines with an incorrect numbers of characters: " + path);
				}

				int j = 0;
				foreach (char cell in row)
				{
					if (!sudokuBoard.isValidChar(cell))
					{
						throw new Exception(
							"ERROR: Specified sudoku file " + path + " contains an invalid character in its board: " + lines[i][j]);
					}

					sudokuBoard.setCell(i - 2, j, cell);
					++j;
				}
			}

			return sudokuBoard;
		}

		public static string getTimeString(long micros)
		{
			long seconds = micros / 1000000;
			long minutes = seconds / 60;
			long hours = minutes / 60;
			micros %= 1000000;
			seconds %= 60;
			minutes %= 60;
			hours %= 100;
			return hours.ToString().PadLeft(2, '0') + ":" +
			       minutes.ToString().PadLeft(2, '0') + ":" +
			       seconds.ToString().PadLeft(2, '0') + "." +
			       micros.ToString().PadLeft(6, '0');
		}

		// It is assumed that we will redirect stdout if we are reading to a file
		public static void writeToFile(Board initialBoard, Board solvedBoard, Solver solver, long totalMillis)
		{
			// Write initial puzzle
			Console.WriteLine(initialBoard.size.ToString());
			Console.WriteLine(lineToString(initialBoard.validCharacters.getList()));
			string[] initFileLines = getBoardFileLines(initialBoard);
			for (int i = 0; i < initialBoard.size; ++i)
			{
				Console.WriteLine(initFileLines[i]);
			}
			Console.WriteLine();
			
			// Write solved puzzle and stats
			Console.WriteLine("Solved");
			string[] solvedFileLines = getBoardFileLines(solvedBoard);
			for (int i = 0; i < initialBoard.size; ++i)
			{
				Console.WriteLine(solvedFileLines[i]);
			}
			Console.WriteLine();

			Console.WriteLine("Total time: " + getTimeString(totalMillis));

			Console.WriteLine("Strategy                        Uses            Time");
			Console.WriteLine("Single Possibility for Square   " +
			                  solver.singleSquareCnt.ToString().PadRight(16, ' ') +
			                  getTimeString(solver.singleSquareElapsed));
			Console.WriteLine("Single Possibility for Region   " +
			                  solver.singleRegionCnt.ToString().PadRight(16, ' ') +
			                  getTimeString(solver.singleRegionElapsed));
			Console.WriteLine("Guessing strategy               " +
			                  solver.guessCnt.ToString().PadRight(16, ' ') +
			                  getTimeString(solver.guessTimeElapsed));
		}

		public static string[] getBoardFileLines(Board sudokuBoard)
		{
			string [] fileLines = new string[sudokuBoard.size];
			for (int i = 0; i < sudokuBoard.size; ++i)
			{
				fileLines[i] = string.Join(" ", sudokuBoard.board[i]);
			}

			return fileLines;
		}

		public static string lineToString(List<char> l)
		{
			return string.Join(" ", l.ToArray());
		}
	}
}
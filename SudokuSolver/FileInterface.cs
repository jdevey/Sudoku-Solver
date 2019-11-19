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
			//List <List <char>> board = new List<List<char>>();

			for (int i = 2; i < lines.Length; ++i)
			{
				List<char> row = lines[i].ToCharArray().Where(c => c != ' ').ToList();
				//board.Add(row);
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

		public static void writeToFile(Board sudokuBoard, string path)
		{
			string[] fileLines = getSudokuBoardFileLines(sudokuBoard);
			File.WriteAllLines(path, fileLines);
		}

		public static string[] getSudokuBoardFileLines(Board sudokuBoard)
		{
			string [] fileLines = new string[sudokuBoard.size + 2];
			fileLines[0] = sudokuBoard.size.ToString();
			fileLines[1] = string.Join(" ", sudokuBoard.validCharacters);
			for (int i = 0; i < sudokuBoard.size; ++i)
			{
				fileLines[i + 2] = string.Join(" ", sudokuBoard.board[i]);
			}

			return fileLines;
		}
	}
}
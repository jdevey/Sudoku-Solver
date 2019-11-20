using System;
using System.Collections.Generic;
using SudokuSolver;

namespace UnitTests
{
	public static class SudokuVerifier
	{
		public static bool isSudokuCorrect(Board sudokuBoard)
		{
			List<List<char>> board = sudokuBoard.board;
			
			// Each cell should be filled and one of the valid characters
			foreach (List <char> row in board)
			{
				foreach (char cell in row)
				{
					if (!sudokuBoard.validCharacters.contains(cell))
					{
						return false;
					}
				}
			}

			List<char> charList = Utils.charSetToList(sudokuBoard.validCharacters);

			// Verify rows
			foreach (List<char> row in board)
			{
				if (!Utils.areListsPermutations(row, charList))
				{
					return false;
				}
			}

			// Verify columns
			for (int i = 0; i < sudokuBoard.size; ++i)
			{
				List<char> column = new List<char>();
				for (int j = 0; j < sudokuBoard.size; ++j)
				{
					column.Add(board[j][i]);
				}

				if (!Utils.areListsPermutations(column, charList))
				{
					return false;
				}
			}

			// Verify blocks
			int blockSize = (int)Math.Sqrt(sudokuBoard.size);
			for (int i = 0; i < blockSize; ++i)
			{
				for (int j = 0; j < blockSize; ++j)
				{
					List<char> block = new List<char>();
					for (int k = 0; k < blockSize; ++k)
					{
						for (int l = 0; l < blockSize; ++l)
						{
							block.Add(board[3 * i + k][3 * j + l]);
						}
					}

					if (!Utils.areListsPermutations(block, charList))
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}
using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public class Board
	{
		public int size { get; }
		public List<List <char>> board { get; }
		public CharSet validCharacters { get; }
		
		public Board(int size, CharSet validCharacters)
		{
			if (size != validCharacters.size())
			{
				throw new Exception("ERROR: Specified sudoku board size does not equal the number of characters listed.");
			}
			this.size = size;
			this.validCharacters = validCharacters;
			board = new List<List<char>>();
			for (int i = 0; i < size; ++i)
			{
				board.Add(new List<char>());
				for (int j = 0; j < size; ++j)
				{
					board[i].Add('\0');
				}
			}
		}

		public Board(Board other)
		{
			size = other.size;
			CharSet newValidCharacters = new CharSet(other.validCharacters);

			List<List<char>> newBoard = new List<List<char>>();
			foreach (List<char> row in other.board)
			{
				newBoard.Add(Utils.listCopy(row));
			}

			board = newBoard;
		}

		public void setCell(int y, int x, char c)
		{
			if (!Utils.isValidCoord(size, y, x))
			{
				throw new Exception("ERROR: Attempted to set out-of-bounds cell in row " + y + " column " + x);
			}

			if (!isValidChar(c))
			{
				throw new Exception("ERROR: Attempted to set cell to invalid character: " + c);
			}

			board[y][x] = c;
		}

		public bool isValidChar(char c)
		{
			return c == '-' || validCharacters.contains(c);
		}

		public void printBoard()
		{
			foreach (List<char> row in board)
			{
				foreach (char cell in row)
				{
					Console.Write(cell + " ");
				}
				Console.WriteLine();
			}
		}
	}
}
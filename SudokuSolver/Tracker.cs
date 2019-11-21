using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public class Tracker
	{
		// State of the board that only includes empty cells and completely filled in (complete) cells
		public Board board { get; }
		
		// The possibilities for each cell of the board thus far
		public List<List<CharSet>> progress { get; } = new List<List<CharSet>>();
		
		public int filledCnt { get; private set; }

		public bool valid { get; private set; } = true;

		public int solutionCnt { get; set; }

		public Tracker(Board sudokuBoard)
		{
			board = sudokuBoard;
			for (int i = 0; i < sudokuBoard.size; ++i)
			{
				progress.Add(new List<CharSet>());
				for (int j = 0; j < sudokuBoard.size; ++j)
				{
					progress[i].Add(new CharSet(sudokuBoard.validCharacters));
				}
			}
		}

		public Tracker(Tracker other)
		{
			board = new Board(other.board);
			valid = other.valid;
			List<List<CharSet>> newProgress = new List<List<CharSet>>();
			for (int i = 0; i < board.size; ++i)
			{
				newProgress.Add(new List<CharSet>());
				for (int j = 0; j < board.size; ++j)
				{
					newProgress[i].Add(new CharSet(other.progress[i][j]));
				}
			}

			progress = newProgress;
		}

		// Eliminate a possibility for a cell and do nothing else
		public void eliminatePossibility(int row, int col, char value)
		{
			progress[row][col].erase(value);
			if (progress[row][col].size() == 0)
			{
				valid = false;
			}
		}

		// Fills square, updates possibilities for its row, column, and block variables, and eliminates possibilities for
		// row, column, and block variables for the progress variable
		public void fillSquare(int row, int col)
		{
			char c = progress[row][col].getList()[0];
			board.setCell(row, col, c);
			++filledCnt;

			int sqt = Utils.getIntSqrt(board.size);
			int baseRow = row - row % sqt;
			int baseCol = col - col % sqt;
			for (int i = 0; i < board.size; ++i)
			{
				if (i != col) eliminatePossibility(row, i, c);
				if (i != row) eliminatePossibility(i, col, c);
				if (!(baseRow + i / sqt == row && baseCol + i % sqt == col))
					eliminatePossibility(baseRow + i / sqt, baseCol + i % sqt, c);
			}
		}
		
		public void fillSquare(int row, int col, char c)
		{
			board.setCell(row, col, c);
			++filledCnt;

			int sqt = Utils.getIntSqrt(board.size);
			int baseRow = row - row % sqt;
			int baseCol = col - col % sqt;
			for (int i = 0; i < board.size; ++i)
			{
				if (i != col) eliminatePossibility(row, i, c);
				if (i != row) eliminatePossibility(i, col, c);
				if (!(baseRow + i / sqt == row && baseCol + i % sqt == col))
					eliminatePossibility(baseRow + i / sqt, baseCol + i % sqt, c);
			}
		}

		public Tuple<int, int> convertFromBlock(int blockInd, int ind)
		{
			int sqt = Utils.getIntSqrt(board.size);
			int row = blockInd / sqt * sqt + ind / sqt;
			int col = blockInd % sqt * sqt + ind % sqt;
			return new Tuple<int, int>(row, col);
		}
	}
}
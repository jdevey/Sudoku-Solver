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
		
		public int solvedCnt { get; set; }

		public bool valid { get; set; } = true;

		public int solutionCnt { get; set; }

		// The possibilities for each row, column, and block respectively
//		public List<CharSet> rows { get; } = new List<CharSet>();
//		public List<CharSet> columns { get; } = new List<CharSet>();
//		public List<CharSet>	blocks { get; } = new List<CharSet>();

		// TODO improve performance
		// Create a dynamic data structure that can keep track of, for each group, the
		// characters in that group that have the fewest possible cells in which they can fit
		
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
//
//			for (int i = 0; i < sudokuBoard.size; ++i)
//			{
//				rows.Add(new CharSet(sudokuBoard.validCharacters));
//				columns.Add(new CharSet(sudokuBoard.validCharacters));
//				blocks.Add(new CharSet(sudokuBoard.validCharacters));
//			}
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
			++solvedCnt;

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
			++solvedCnt;

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

		// Converts a coordinate into which block it is
		public int convertToBlock(int row, int col)
		{
			int sqt = Utils.getIntSqrt(board.size);
			return row / sqt * sqt + col / sqt;
		}

		public Tuple<int, int> convertFromBlock(int blockInd, int ind)
		{
			int sqt = Utils.getIntSqrt(board.size);
			int row = blockInd / sqt * sqt + ind / sqt;
			int col = blockInd % sqt * sqt + ind % sqt; // TODO test
			return new Tuple<int, int>(row, col);
		}
	}
}
using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public class Tracker
	{
		// State of the board that only includes empty cells and completely filled in (complete) cells
		public Board board { get; }
		
		// The possibilities for each cell of the board thus far
		private List<List<CharSet>> progress = new List<List<CharSet>>();

		// The possibilities for each row, column, and region respectively
		private List<CharSet> rows = new List<CharSet>();
		private List<CharSet> columns = new List<CharSet>();
		private List<CharSet>	regions = new List<CharSet>();
		
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

			for (int i = 0; i < sudokuBoard.size; ++i)
			{
				rows.Add(new CharSet(sudokuBoard.validCharacters));
				columns.Add(new CharSet(sudokuBoard.validCharacters));
				regions.Add(new CharSet(sudokuBoard.validCharacters));
			}
		}

		// TODO finish copy constructor
		public Tracker(Tracker other)
		{
			board = new Board(other.board);
		}

		public void eliminatePossibility(int row, int col, char value)
		{
			CharSet cellProgress = progress[row][col];
			cellProgress.erase(value);
//			if (cellProgress.size() == 1)
//			{
//				fillSquare(row, col);
//			}
		}

		public void fillSquare(int row, int col)
		{
			char c = progress[row][col].findSingle();
			board.setCell(row, col, c);
			int reg = convertToRegion(row, col);
			rows[row].erase(c);
			columns[col].erase(c);
			regions[reg].erase(c);
			
			int sqt = Utils.getIntSqrt(board.size);
			int baseRow = row - row % sqt;
			int baseCol = col - col % sqt;
			for (int i = 0; i < board.size; ++i)
			{
				eliminatePossibility(row, i, c);
				eliminatePossibility(i, col, c);
				eliminatePossibility(baseRow + i / sqt, baseCol + i % sqt, c);
			}
		}

		private int convertToRegion(int row, int col)
		{
			int sqt = Utils.getIntSqrt(board.size);
			return row / sqt * sqt + col / sqt;
		}
	}
}
using System;
using System.Collections.ObjectModel;

namespace SudokuSolver
{
	// Class to execute the single square strat
	public class SingleSquare : IStrategy
	{
		public int row { get; }
		public int col { get; }
		
		public SingleSquare(int row, int col)
		{
			this.row = row;
			this.col = col;
		}

		// Attempt to fill in a square if doing so is valid; return true if successful
		public bool execute(Tracker tracker)
		{
			if (tracker.progress[row][col].size() == 1)
			{
				tracker.fillSquare(row, col);
				return true;
			}

			return false;
		}

		public Tuple<int, int> getTuple()
		{
			return new Tuple<int, int>(row, col);
		}
	}
}
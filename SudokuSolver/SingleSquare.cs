namespace SudokuSolver
{
	// Class to execute the single square strat
	public class SingleSquare : IStrategy
	{
		private int row { get; }
		private int col { get; }
		
		public SingleSquare(int row, int col)
		{
			this.row = row;
			this.col = col;
		}

		// Attempt to fill in a square if doing so is valid; return true if successful
		public bool execute(Tracker tracker)
		{
			if (tracker.board.board[row][col] == '-' && tracker.progress[row][col].size() == 1)
			{
				tracker.fillSquare(row, col);
				return true;
			}

			return false;
		}
	}
}
using System;

namespace SudokuSolver
{
	public class SingleRegion : IStrategy
	{
		private readonly int index;
		private readonly Constants.RegionTypes regionType;
		private readonly char check;
		
		public SingleRegion(int index, Constants.RegionTypes regionType, char check)
		{
			this.index = index;
			this.regionType = regionType;
			this.check = check;
		}
		
		// Attempt to fill in a square if doing so is valid; return true if successful
		public bool execute(Tracker tracker)
		{
			int size = tracker.board.size, charCnt = 0;
			
			switch (regionType)
			{
				case Constants.RegionTypes.ROW:
					int f1 = -1;
					for (int i = 0; i < size; ++i)
					{
						if (tracker.progress[index][i].contains(check))
						{
							++charCnt;
							f1 = i;
						}
					}

					if (charCnt == 1)
					{
						if (tracker.board.board[index][f1] == '-')
							tracker.fillSquare(index, f1, check);
						return true;
					}

					return false;
				
				case Constants.RegionTypes.COLUMN:
					int f2 = -1;
					for (int i = 0; i < size; ++i)
					{
						if (tracker.progress[i][index].contains(check))
						{
							++charCnt;
							f2 = i;
						}
					}

					if (charCnt == 1)
					{
						if (tracker.board.board[f2][index] == '-')
							tracker.fillSquare(f2, index, check);
						return true;
					}

					return false;
				
				case Constants.RegionTypes.BLOCK:
					int frow = -1;
					int fcol = -1;
					for (int i = 0; i < size; ++i)
					{
						Tuple<int, int> t = tracker.convertFromBlock(index, i);
						int row = t.Item1;
						int col = t.Item2;
						if (tracker.progress[row][col].contains(check))
						{
							++charCnt;
							frow = row;
							fcol = col;
						}
					}

					if (charCnt == 1)
					{
						if (tracker.board.board[frow][fcol] == '-')
							tracker.fillSquare(frow, fcol, check);
						return true;
					}

					break;
			}
			return false;
		}
	}
}
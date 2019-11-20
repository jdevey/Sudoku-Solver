using System;

namespace SudokuSolver
{
	public class SingleRegion : IStrategy
	{
		private int index;
		private Utils.RegionTypes rt;
		private char check;
		
		public SingleRegion(int index, Utils.RegionTypes rt, char check)
		{
			this.index = index;
			this.rt = rt;
			this.check = check;
		}
		
		// Attempt to fill in a square if doing so is valid; return true if successful
		public bool execute(Tracker tracker)
		{
			int size = tracker.board.size, charCnt = 0;
			
			switch (rt)
			{
				case Utils.RegionTypes.ROW:
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
						tracker.fillSquare(index, f1);
						return true;
					}

					return false;
				
				case Utils.RegionTypes.COLUMN:
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
						tracker.fillSquare(f2, index);
						return true;
					}

					return false;
				
				case Utils.RegionTypes.BLOCK:
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
						tracker.fillSquare(frow, fcol);
						return true;
					}

					return false;
			}
			return false;
		}
	}
}
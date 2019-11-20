using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public class Solver
	{
		private Tracker tracker;
		private readonly int size;
		private Queue<IStrategy> qStrats;
		private HashSet<Tuple<int, int>> singleSquareVis = new HashSet<Tuple<int, int>>();
		private HashSet<Tuple<int, string, char>> singleRegionVis = new HashSet<Tuple<int, string, char>>();
		
		public Solver(Tracker tracker)
		{
			this.tracker = tracker;
			this.size = tracker.board.size;
			qStrats = new Queue<IStrategy>();
		}

		private void prepareBoard()
		{
			// Put initial values to use
			for (int i = 0; i < size; ++i)
			{
				for (int j = 0; j < size; ++j)
				{
					char c = tracker.board.board[i][j];
					if (c != '-')
					{
						tracker.progress[i][j].clear();
						tracker.progress[i][j].insert(c);
						
						// leave it here so the singlesquare strat can pick it up
//						tracker.rows[i].erase(c);
//						tracker.columns[j].erase(c);
//						int reg = tracker.convertToBlock(i, j);
//						tracker.blocks[reg].erase(c);
					}
				}
			}
			
			// Queue up strats
			for (int i = 0; i < size; ++i)
			{
				for (int j = 0; j < size; ++j)
				{
					qStrats.Enqueue(new SingleSquare(i, j));
				}
			}
			
			foreach (Utils.RegionTypes e in Enum.GetValues(typeof(Utils.RegionTypes)))
			{
				for (int i = 0; i < size; ++i)
				{
					foreach (char c in tracker.board.validCharacters.getList())
					{
						qStrats.Enqueue(new SingleRegion(i, e, c));
					}
				}
			}
		}



		public Board run()
		{
			prepareBoard();
			while (qStrats.Count != 0)
			{
				IStrategy top = qStrats.Dequeue();
				bool success = top.execute(tracker);
				if (!success)
				{
					continue;
				}
				if (top.GetType() == typeof(SingleSquare))
				{
					SingleSquare ss = (SingleSquare) top;
					int row = ss.row;
					int col = ss.col;
					char filledChar = tracker.board.board[row][col];
					int sqt = Utils.getIntSqrt(size);
					int baseRow = row - row % sqt;
					int baseCol = col - col % sqt;
					// Add single square strats
					for (int i = 0; i < size; ++i)
					{
						if (!singleSquareVis.Contains(new Tuple<int, int>(row, i)))
						{
							qStrats.Enqueue(new SingleSquare(row, i));
						}
						if (!singleSquareVis.Contains(new Tuple<int, int>(i, col)))
						{
							qStrats.Enqueue(new SingleSquare(i, col));
						}
						int rr = baseRow + i / sqt;
						int rc = baseCol + i % sqt;
						if (!singleSquareVis.Contains(new Tuple<int, int>(rr, rc)))
						{
							qStrats.Enqueue(new SingleSquare(rr, rc));
						}
					}

					for (int i = 0; i < size; ++i)
					{
						foreach (char c in tracker.rows[row].getList())
						{
							
						}
					}
				}
			}
			
			return tracker.board;
		}
	}
}
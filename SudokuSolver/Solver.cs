using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public class Solver
	{
		private readonly int MAX_TRIES;
		private Tracker tracker;
		private readonly int size;
		private readonly Queue<IStrategy> stratQueue;
		
		public long singleSquareElapsed { get; private set; }
		public long singleRegionElapsed { get; private set; }
		public long guessTimeElapsed { get; private set; }
		public long totalTimeElapsed { get; private set; }
		
		public long singleSquareCnt { get; private set; }
		public long singleRegionCnt { get; private set; }
		public long guessCnt { get; private set; }

		public Solver(Tracker tracker)
		{
			this.tracker = tracker;
			size = tracker.board.size;
			MAX_TRIES = size * size + 3 * size * size;
			stratQueue = new Queue<IStrategy>();
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
						tracker.fillSquare(i, j, c);
					}
				}
			}
			queueStrats();
		}

		private void queueStrats()
		{
			for (int i = 0; i < size; ++i)
			{
				for (int j = 0; j < size; ++j)
				{
					if (tracker.board.board[i][j] == '-')
					{
						stratQueue.Enqueue(new SingleSquare(i, j));
					}
				}
			}
			
			foreach (Constants.RegionTypes e in Enum.GetValues(typeof(Constants.RegionTypes)))
			{
				for (int i = 0; i < size; ++i)
				{
					foreach (char c in tracker.board.validCharacters.getList())
					{
						stratQueue.Enqueue(new SingleRegion(i, e, c));
					}
				}
			}
		}

		private void guessAll()
		{
			long guessStart = DateTime.Now.Ticks;
			for (int k = 2; k < 4; ++k)
			{
				for (int i = 0; i < size; ++i)
				{
					for (int j = 0; j < size; ++j)
					{
						if (tracker.board.board[i][j] != '-')
						{
							continue;
						}
						CharSet possib = tracker.progress[i][j];
						if (possib.size() == k)
						{
							for (int l = 0; l < k; ++l)
							{
								Tracker newTracker = new Tracker(tracker);
								newTracker.board.setCell(i, j, possib.getList()[l]);
								Solver newSolver = new Solver(newTracker);
								Tracker retTracker = newSolver.run();
								if (retTracker.valid)
								{
									singleSquareElapsed += newSolver.singleSquareElapsed;
									singleRegionElapsed += newSolver.singleRegionElapsed;
									guessCnt += newSolver.guessCnt + 1;
									++tracker.solutionCnt;
									if (tracker.solutionCnt == 2 || retTracker.solutionCnt == 2)
									{
										tracker = retTracker;
										tracker.solutionCnt = 2;
										goto breakout;
									}
								}
							}
						}
					}
				}
			}

			breakout:
			
			long guessEnd = DateTime.Now.Ticks;
			long timeMicros = (guessEnd - guessStart) / 10;
			guessTimeElapsed += timeMicros;
		}

		public Tracker run()
		{
			prepareBoard();
			long totalStart = DateTime.Now.Ticks;
			int fails = 0;
			while (tracker.filledCnt < size * size)
			{
				IStrategy top = stratQueue.Dequeue();
				long start = DateTime.Now.Ticks;
				bool success = top.execute(tracker);
				long end = DateTime.Now.Ticks;
				if (success)
				{
					if (!tracker.valid)
					{
						break;
					}
				}
				fails = success ? 0 : fails + 1;
				long micros = (end - start) / 10;
				if (top.GetType() == typeof(SingleSquare))
				{
					singleSquareElapsed += micros;
					if (success)
					{
						++singleSquareCnt;
					}
				}
				else if (top.GetType() == typeof(SingleRegion))
				{
					singleRegionElapsed += micros;
					if (success)
					{
						++singleRegionCnt;
					}
				}
				if (!success)
				{
					stratQueue.Enqueue(top);
				}

				if (fails >= MAX_TRIES/* && tracker.solutionCnt < 2*/)
				{
					guessAll();
					break;
				}
			}
			long totalEnd = DateTime.Now.Ticks;
			totalTimeElapsed = (totalEnd - totalStart) / 10;
			
			return tracker;
		}
	}
}
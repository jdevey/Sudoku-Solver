using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SudokuSolver
{
	public class Solver
	{
		private readonly int MAX_TRIES;
		private Tracker tracker;
		private readonly int size;
		private Queue<IStrategy> qStrats;
		//private HashSet<Tuple<int, int>> singleSquareVis = new HashSet<Tuple<int, int>>();
		//private HashSet<Tuple<int, string, char>> singleRegionVis = new HashSet<Tuple<int, string, char>>();
		
		public long SingleSquareElapsed { get; set; }
		public long SingleRegionElapsed { get; set; }
		public long guessTimeElapsed { get; set; }
		public long totalTimeElapsed { get; set; }
		
		public long SingleSquareCnt { get; set; }
		public long SingleRegionCnt { get; set; }
		public long GuessCnt { get; set; }
		
		public Solver(Tracker tracker)
		{
			this.tracker = tracker;
			size = tracker.board.size;
			MAX_TRIES = size * size + 3 * size * size;
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
						tracker.fillSquare(i, j, c);
						
						// leave it here so the singlesquare strat can pick it up
//						tracker.rows[i].erase(c);
//						tracker.columns[j].erase(c);
//						int reg = tracker.convertToBlock(i, j);
//						tracker.blocks[reg].erase(c);
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
//					if (!singleSquareVis.Contains(new Tuple<int, int>(i, j)))
//					{
						qStrats.Enqueue(new SingleSquare(i, j));
//					}
				}
			}
			
			foreach (Utils.RegionTypes e in Enum.GetValues(typeof(Utils.RegionTypes)))
			{
				for (int i = 0; i < size; ++i)
				{
					foreach (char c in tracker.board.validCharacters.getList())
					{
//						if (!singleRegionVis.Contains(new Tuple<int, string, char>(i, e.ToString(), c)))
//						{
							qStrats.Enqueue(new SingleRegion(i, e, c));
//						}
					}
				}
			}
		}

		private int getMinGuessSize()
		{
			int mn = tracker.board.size;
			for (int i = 0; i < size; ++i)
			{
				for (int j = 0; j < size; ++j)
				{
					mn = Math.Min(mn, tracker.progress[i][j].size());
				}
			}

			return mn;
		}
		
		public void guessAll()
		{
			long guessStart = DateTime.Now.Ticks;
			for (int k = 2; k < size; ++k) // TODO can this start at 1?
			{
				for (int i = 0; i < size; ++i)
				{
					for (int j = 0; j < size; ++j)
					{
						CharSet possib = tracker.progress[i][j];
						if (possib.size() == k)
						{
							for (int l = 0; l < k; ++l)
							{
								Tracker newTracker = new Tracker(tracker);
								newTracker.fillSquare(i, j, possib.getList()[l]);
								Solver newSolver = new Solver(newTracker);
								newSolver.run();
								if (newTracker.valid)
								{
									SingleSquareElapsed += newSolver.SingleSquareElapsed;
									SingleRegionElapsed += newSolver.SingleRegionElapsed;
									GuessCnt = GuessCnt + newSolver.GuessCnt + 1;
									tracker = newTracker;
									goto breakout;
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

		public Board run()
		{
			prepareBoard();
			long totalStart = DateTime.Now.Ticks;
			int tries = 0;
			//Tuple <int, int> lastSingleSquare = new Tuple<int, int>(-1, -1);
			while (tracker.board.solvedCnt < size * size)
			{
				IStrategy top = qStrats.Dequeue();
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
				tries = success ? 0 : tries + 1;
				long micros = (end - start) / 10;
				if (top.GetType() == typeof(SingleSquare))
				{
					SingleSquareElapsed += micros;
					if (success)
					{
						++SingleSquareCnt;
					//SingleSquare tp = (SingleSquare) top;
					}
					//if (success) lastSingleSquare = tp.getTuple();
				}
				else if (top.GetType() == typeof(SingleRegion))
				{
					SingleRegionElapsed += micros;
					if (success)
					{
						++SingleRegionCnt;
					}
				}
				if (!success)
				{
					qStrats.Enqueue(top);
				}

//				if (tries >= MAX_TRIES)
//				{
//					Console.WriteLine("Can't solve board without guessing.");
//					guessAll();
//					break;
//				}
			}
			long totalEnd = DateTime.Now.Ticks;
			totalTimeElapsed = (totalEnd - totalStart) / 10;
			
			return tracker.board;
		}
	}
}
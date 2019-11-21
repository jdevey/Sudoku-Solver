using System.Collections.Generic;
using System.Linq;
using SudokuSolver;

namespace UnitTests
{
	public static class TestUtils
	{
		public static bool boardsEqual(List<List<char>> a, List<List<char>> b)
		{
			if (a.Count != b.Count)
			{
				return false;
			}

			for (int i = 0; i < a.Count; ++i)
			{
				if (!listsEqual(a[i], b[i]))
				{
					return false;
				}
			}

			return true;
		}

		public static bool listsEqual<T>(List<T> a, List<T> b)
		{
			var firstNotSecond = a.Except(b).ToList();
			var secondNotFirst = b.Except(a).ToList();
			return !firstNotSecond.Any() && !secondNotFirst.Any();
		}
		
		public static bool isCorrectSolution(Board board)
		{
			int size = board.size;

			for (int i = 0; i < size; ++i)
			{
				for (int j = 0; j < size; ++j)
				{
					if (board.board[i][j] == '-')
					{
						return false;
					}
				}
			}

			for (int i = 0; i < size; ++i)
			{
				int[] cs = new int[Constants.MAX_CHAR];
				for (int j = 0; j < size; ++j)
				{
					++cs[board.board[i][j]];
				}

				for (int j = 0; j < Constants.MAX_CHAR; ++j)
				{
					if (cs[j] > 1)
					{
						return false;
					}
				}
			}
			
			for (int i = 0; i < size; ++i)
			{
				int[] cs = new int[Constants.MAX_CHAR];
				for (int j = 0; j < size; ++j)
				{
					++cs[board.board[j][i]];
				}

				for (int j = 0; j < Constants.MAX_CHAR; ++j)
				{
					if (cs[j] > 1)
					{
						return false;
					}
				}
			}

			int sqt = Utils.getIntSqrt(size);
			for (int h = 0; h < sqt; ++h)
			{
				for (int i = 0; i < sqt; ++i)
				{
					int[] cs = new int[Constants.MAX_CHAR]; 
					for (int j = 0; j < sqt; ++j)
					{
						for (int k = 0; k < sqt; ++k)
						{
							++cs[board.board[h * sqt + j][i * sqt + k]];
						}
					}

					for (int j = 0; j < Constants.MAX_CHAR; ++j)
					{
						if (cs[j] > 1)
						{
							return false;
						}
					}
				}
			}

			return true;
		}
	}
}
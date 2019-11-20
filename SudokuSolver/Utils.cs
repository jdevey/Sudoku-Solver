using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public static class Utils
	{
		public const int MAX_CHAR = 128;
		
		public enum RegionTypes
		{
			ROW,
			COLUMN,
			BLOCK
		};
		
		public static bool isValidCoord(int size, int y, int x)
		{
			return y < size && x < size;
		}
		
		public static List<T> listCopy <T>(List<T> l)
		{
			return l.ConvertAll(elem => elem); // TODO test
		}

		public static List<char> charSetToList (CharSet charSet)
		{
			List<char> charList = new List<char>();
			for (int i = 0; i < Utils.MAX_CHAR; ++i)
			{
				if (charSet.mem[i])
				{
					charList.Add((char)i);
				}
			}

			return charList;
		}
		
		public static bool areListsPermutations <T > (List<T> expected, List<T> actual) where T : IComparable<T>
		{
			List<T> actualCopy = Utils.listCopy(actual);
			actualCopy.Sort();
			for (int i = 0; i < expected.Count; ++i)
			{
				if (expected[i].CompareTo(actualCopy[i]) != 0) // The elements are not equal
				{
					return false;
				}
			}

			return expected.Count == actual.Count;
		}

		public static int getIntSqrt(int n)
		{
			return (int)Math.Sqrt(n);
		}

		public static bool isCorrectSolution(Board board)
		{
			int size = board.size;
			if (board.board.Count != size || board.board.Count != size)
			{
				return false;
			}

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
				int[] cs = new int[Utils.MAX_CHAR];
				for (int j = 0; j < size; ++j)
				{
					++cs[board.board[i][j]];
				}

				for (int j = 0; j < Utils.MAX_CHAR; ++j)
				{
					if (cs[j] > 1)
					{
						return false;
					}
				}
			}
			
			for (int i = 0; i < size; ++i)
			{
				int[] cs = new int[Utils.MAX_CHAR];
				for (int j = 0; j < size; ++j)
				{
					++cs[board.board[j][i]];
				}

				for (int j = 0; j < Utils.MAX_CHAR; ++j)
				{
					if (cs[j] > 1)
					{
						return false;
					}
				}
			}

			int sqt = getIntSqrt(size);
			for (int h = 0; h < sqt; ++h)
			{
				for (int i = 0; i < sqt; ++i)
				{
					int[] cs = new int[Utils.MAX_CHAR]; 
					for (int j = 0; j < sqt; ++j)
					{
						for (int k = 0; k < sqt; ++k)
						{
							++cs[board.board[h * sqt + j][i * sqt + k]];
						}
					}

					for (int j = 0; j < Utils.MAX_CHAR; ++j)
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
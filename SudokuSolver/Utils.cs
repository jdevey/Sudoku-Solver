using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public static class Utils
	{
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
			for (int i = 0; i < 128; ++i)
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
	}
}
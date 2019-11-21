using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public static class Utils
	{
		public static bool isValidCoord(int size, int y, int x)
		{
			return y < size && x < size && y > -1 && x > -1;
		}
		
		public static List<T> listCopy <T>(List<T> l)
		{
			return l.ConvertAll(elem => elem);
		}
		
		public static int getIntSqrt(int n)
		{
			return (int)Math.Sqrt(n);
		}
	}
}
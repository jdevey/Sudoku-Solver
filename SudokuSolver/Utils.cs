namespace SudokuSolver
{
	public static class Utils
	{
		public static bool isValidCoord(int size, int y, int x)
		{
			return y < size && x < size;
		}
	}
}
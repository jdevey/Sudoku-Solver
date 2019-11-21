namespace SudokuSolver
{
	public static class Constants
	{
		public const int MAX_CHAR = 128;

		public const string EASY_INPUT = @"..\..\..\..\SudokuSolver\SamplePuzzles\BasicInput";
		public const string TEST_INPUT = @"..\..\..\..\SudokuSolver\SamplePuzzles\TestInput";
		
		public enum RegionTypes
		{
			ROW,
			COLUMN,
			BLOCK
		};
	}
}
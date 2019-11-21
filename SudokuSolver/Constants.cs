namespace SudokuSolver
{
	public class Constants
	{
		public const int MAX_CHAR = 128;

		public const string ORIGINAL_INPUT = @"..\..\..\..\SudokuSolver\SamplePuzzles\OriginalInput";
		public const string NORMAL_INPUT = @"..\..\..\..\SudokuSolver\SamplePuzzles\Input";
		public const string EASY_INPUT = @"..\..\..\..\SudokuSolver\SamplePuzzles\BasicInput";
		public const string TEST_INPUT = @"..\..\..\..\SudokuSolver\SamplePuzzles\TestInput";
		public const string DEFAULT_OUTPUT = @"..\..\..\..\SudokuSolver\SamplePuzzles\DefaultOutput";
		
		public enum RegionTypes
		{
			ROW,
			COLUMN,
			BLOCK
		};
	}
}
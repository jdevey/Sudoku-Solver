using System.Collections.Generic;
using NUnit.Framework;
using SudokuSolver;

namespace UnitTests
{
	[TestFixture]
	public class UtilsTests
	{
		[Test]
		public void Main()
		{
			Assert.IsTrue(Utils.isValidCoord(10, 0, 9));
			Assert.IsTrue(Utils.isValidCoord(10, 9, 0));
			Assert.IsFalse(Utils.isValidCoord(10, 10, 0));
			Assert.IsFalse(Utils.isValidCoord(10, 9, -1));
			Assert.IsFalse(Utils.isValidCoord(10, -1, 0));
			Assert.IsFalse(Utils.isValidCoord(10, 9, 10));
			
			List <int> l1 = new List<int>{1, 2, 3};
			List<int> l2 = Utils.listCopy(l1);
			l2[0] = 5;
			Assert.AreNotEqual(l1[0], l2[0]);
			
			Assert.AreEqual(3, Utils.getIntSqrt(9));
			Assert.AreEqual(3, Utils.getIntSqrt(10));
			Assert.AreEqual(2, Utils.getIntSqrt(8));
		}
	}
}
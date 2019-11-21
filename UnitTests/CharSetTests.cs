using System.Collections.Generic;
using NUnit.Framework;
using SudokuSolver;

namespace UnitTests
{
	[TestFixture]
	public class CharSetTests
	{
		[Test]
		public void Main()
		{
			CharSet c = new CharSet();
			List<char> lab = new List<char> {'a', 'b'};
			CharSet lc = new CharSet(lab);
			Assert.True(TestUtils.listsEqual(lab, lc.getList()));
			
			CharSet lcCopy = new CharSet(lc);
			Assert.True(TestUtils.listsEqual(lcCopy.getList(), lc.getList()));
			
			c.insert('f');
			Assert.IsTrue(c.contains('f'));
			Assert.IsFalse(c.contains('e'));
			Assert.AreEqual(1, c.size());
			c.erase('f');
			Assert.IsFalse(c.contains('f'));
			Assert.AreEqual(0, c.size());
			c.insert('g');
			c.insert('g');
			c.insert('h');
			Assert.AreEqual(2, c.size());
			c.clear();
			Assert.AreEqual(0, c.size());
		}
	}
}
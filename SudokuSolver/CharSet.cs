using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public class CharSet
	{
		public List<bool> mem { get; }
		private int numAlloc;

		public CharSet()
		{
			mem = new List<bool>(new bool[128]);
		}

		public CharSet(IEnumerable<char> init)
		{
			mem = new List<bool>(new bool[128]);
			foreach (char c in init)
			{
				numAlloc += Convert.ToInt32(!mem[c]);
				mem[c] = true;
			}
		}

		public CharSet(CharSet other)
		{
			mem = Utils.listCopy(other.mem);
			numAlloc = other.size();
		}

		public bool contains(char c)
		{
			return mem[c];
		}

		public void insert(char c)
		{
			numAlloc += Convert.ToInt32(!mem[c]);
			mem[c] = true;
		}

		public void erase(char c)
		{
			numAlloc -= Convert.ToInt32(mem[c]);
			mem[c] = false;
		}

		public int size()
		{
			return numAlloc;
		}

		// TODO improve performance
		public char findSingle()
		{
			for (int i = 0; i < 128; ++i)
			{
				if (mem[i])
				{
					return (char) i;
				}
			}
			return (char) 0;
		}
	}
}
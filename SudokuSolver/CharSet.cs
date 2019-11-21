using System;
using System.Collections.Generic;

namespace SudokuSolver
{
	public class CharSet
	{
		private List<bool> mem { get; }
		private List<char> set { get; } = new List<char>();
		private int numAlloc;

		public CharSet()
		{
			mem = new List<bool>(new bool[Constants.MAX_CHAR]);
		}

		public CharSet(IEnumerable<char> init)
		{
			mem = new List<bool>(new bool[Constants.MAX_CHAR]);
			foreach (char c in init)
			{
				numAlloc += Convert.ToInt32(!mem[c]);
				mem[c] = true;
				set.Add(c);
			}
		}

		public CharSet(CharSet other)
		{
			mem = Utils.listCopy(other.mem);
			numAlloc = other.size();
			foreach (char c in other.set)
			{
				set.Add(c);
			}
		}

		public bool contains(char c)
		{
			return mem[c];
		}

		public void insert(char c)
		{
			numAlloc += Convert.ToInt32(!mem[c]);
			mem[c] = true;
			set.Add(c);
		}

		public void erase(char c)
		{
			numAlloc -= Convert.ToInt32(mem[c]);
			mem[c] = false;
			set.Remove(c);
		}

		public int size()
		{
			return numAlloc;
		}

		public List<char> getList()
		{
			return set;
		}

		public void clear()
		{
			set.Clear();
			numAlloc = 0;
			for (int i = 0; i < Constants.MAX_CHAR; ++i)
			{
				mem[i] = false;
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
	public class Solver
	{
		public static string[] Solve(string[] input) => new Solver(input).Solve();

		static readonly int[] Range9 = Enumerable.Range(0, 9).ToArray();

		int[,] field = new int[9, 9];
		List<(int, int)> blanks = new List<(int, int)>();

		bool[][] rows = Array.ConvertAll(Range9, _ => new bool[10]);
		bool[][] columns = Array.ConvertAll(Range9, _ => new bool[10]);
		bool[,][] sections = new bool[3, 3][];

		Solver(string[] input)
		{
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					sections[i, j] = new bool[10];

			for (int i = 0; i < 9; i++)
				for (int j = 0; j < 9; j++)
					if (input[i][j] == '.')
						blanks.Add((i, j));
					else
						Set(i, j, input[i][j] - '0', true);
		}

		void Set(int i, int j, int x, bool on)
		{
			field[i, j] = on ? x : 0;
			rows[i][x] = columns[j][x] = sections[i / 3, j / 3][x] = on;
		}

		string[] Solve()
		{
			var ok = false;
			Dfs(0);
			if (!ok) throw new InvalidOperationException("The input has no solution.");
			return Array.ConvertAll(Range9, i => string.Join("", Range9.Select(j => field[i, j])));

			void Dfs(int k)
			{
				if (k == blanks.Count) { ok = true; return; }

				var (i, j) = blanks[k];
				for (int x = 1; x <= 9; x++)
				{
					if (rows[i][x] || columns[j][x] || sections[i / 3, j / 3][x]) continue;

					Set(i, j, x, true);
					Dfs(k + 1);
					if (ok) return;
					Set(i, j, x, false);
				}
			}
		}
	}
}

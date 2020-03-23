using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SudokuSolver
{
	class Program
	{
		const string Input = "Input";
		const string Output = "Output";

		static void Main(string[] args)
		{
			if (!Directory.Exists(Input)) return;
			Directory.CreateDirectory(Output);

			foreach (var file in Directory.EnumerateFiles(Input))
			{
				var input = File.ReadAllLines(file);

				var sw = Stopwatch.StartNew();
				var solution = Solver.Solve(input);
				sw.Stop();
				Console.WriteLine(sw.Elapsed);

				File.WriteAllLines(Path.Combine(Output, Path.GetFileName(file)), solution, Encoding.UTF8);
			}
		}
	}
}

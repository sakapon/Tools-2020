using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
	class Program
	{
		const string Input = "Input";
		const string Output = "Output";

		static readonly Encoding UTF8N = new UTF8Encoding();

		static void Main(string[] args)
		{
			if (!Directory.Exists(Input)) return;
			Directory.CreateDirectory(Output);

			foreach (var file in Directory.EnumerateFiles(Input))
			{
				try
				{
					var fileName = Path.GetFileName(file);
					Console.WriteLine(fileName);

					var input = File.ReadAllLines(file, UTF8N);

					var sw = Stopwatch.StartNew();
					var solution = Solver.Solve(input);
					sw.Stop();
					Console.WriteLine(sw.Elapsed);

					var content = string.Join("", solution.Select(x => x + "\n"));
					File.WriteAllText(Path.Combine(Output, fileName), content, UTF8N);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}

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

		static void Main(string[] args)
		{
			if (!Directory.Exists(Input)) return;
			Directory.CreateDirectory(Output);

			foreach (var filePath in Directory.EnumerateFiles(Input))
			{
				try
				{
					var fileName = Path.GetFileName(filePath);
					Console.WriteLine(fileName);

					var encoding = DetectEncoding(filePath);
					var input = File.ReadAllLines(filePath, encoding);

					var sw = Stopwatch.StartNew();
					var solution = Solver.Solve(input);
					sw.Stop();
					Console.WriteLine(sw.Elapsed);

					var content = string.Join("", solution.Select(x => x + "\n"));
					File.WriteAllText(Path.Combine(Output, fileName), content, encoding);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		static readonly Encoding UTF8N = new UTF8Encoding();

		static Encoding DetectEncoding(string filePath)
		{
			var preamble = Encoding.UTF8.GetPreamble();
			var headBytes = new byte[preamble.Length];

			using (var stream = File.OpenRead(filePath))
			{
				stream.Read(headBytes, 0, headBytes.Length);
			}
			return headBytes.SequenceEqual(preamble) ? Encoding.UTF8 : UTF8N;
		}
	}
}

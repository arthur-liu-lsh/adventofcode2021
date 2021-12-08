using System;
using System.Linq;
using System.Diagnostics;

namespace D8
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = lines.Length;
            int m = lines[0].Split(new char[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries).Length;

            string[][] inputs = lines.Select(x => x.Split(new char[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries)).ToArray();


            // Part 1
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int count = 0;

            foreach (string[] line in inputs) {
                for (int i = 10; i < m; i++) {
                    if (line[i].Length == 2 || line[i].Length == 3 || line[i].Length == 4 || line[i].Length == 7) {
                        count++;
                    }
                }
            }

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Occurences of 1, 4, 7, and 8: {count}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");


            // Part 2
            sw.Restart();

            int sum = 0;

            for (int i = 0; i < n; i++) {
                DigitDecoder decoder = new DigitDecoder(inputs[i].Select(x => x).Take(10).Select(x => new Digit(x)).ToArray());
                decoder.Solve();
                string s = "";
                for(int j = 10; j < 14; j++) {
                    s += decoder.DecodeToString(inputs[i][j]);
                }
                sum += int.Parse(s);
            }

            sw.Stop();

            Console.WriteLine("\nPart 2:");
            Console.WriteLine($"Sum of all outputs: {sum}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
        }
    }
}

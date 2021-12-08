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
        }
    }
}

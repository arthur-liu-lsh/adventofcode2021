using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace D14
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = lines.Length-2;


            var rules = new Dictionary<string, string>();
            for (int i = 0; i < n; i++) {
                string[] line = lines[i+2].Split(new char[] {' ', '-', '>'}, StringSplitOptions.RemoveEmptyEntries);
                rules.Add(line[0], line[1]);
            }


            // Part 1
            var sw = new Stopwatch();
            sw.Start();

            const int iter1 = 10;

            string sequence = lines[0];

            for (int i = 0; i < iter1; i++) {
                int m = sequence.Length;
                string[] inserts = new string[m];
                for (int j = 0; j < m; j++) {
                    inserts[j] = "";
                }
                for (int j = 0; j < m-1; j++) {
                    string slice = sequence.Substring(j,2);
                    if (rules.ContainsKey(slice)) {
                        inserts[j] = rules[slice];
                    }
                }

                string newSequence = "";
                for (int j = 0; j < m; j++) {
                    newSequence += sequence[j] + inserts[j];
                }
                sequence = newSequence;
            }

            int[] counts1 = sequence.GroupBy(c => c).Select(x => x.Count()).OrderByDescending(x => x).ToArray();

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Quantity of most common character: {counts1[0]}");
            Console.WriteLine($"Quantity of least common character: {counts1[counts1.Length-1]}");
            Console.WriteLine($"Answer (most common - least common): {counts1[0] - counts1[counts1.Length-1]}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

            // Part 2
            // We need a much more memory efficient way of counting in order to complete this part as the size of our string would grow exponentially with more steps
            sw.Restart();

            var polymer = new Polymer(lines[0]);

            for (int i = 0; i < n; i++) {
                string[] line = lines[i+2].Split(new char[] {' ', '-', '>'}, StringSplitOptions.RemoveEmptyEntries);
                polymer.AddRule(line);
            }

            const int iter2 = 40;

            for (int i = 0; i < iter2; i++) {
                polymer.Iterate();
            }

            ulong[] counts2 = polymer.GetCounts().Select(x => x.Value).Where(x => x > 0ul).OrderByDescending(x => x).ToArray();

            Console.WriteLine("\nPart 2:");
            Console.WriteLine($"Quantity of most common character: {counts2[0]}");
            Console.WriteLine($"Quantity of least common character: {counts2[counts2.Length-1]}");
            Console.WriteLine($"Answer (most common - least common): {counts2[0] - counts2[counts2.Length-1]}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
        }
    }
}

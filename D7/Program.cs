using System;
using System.Linq;
using System.Diagnostics;

namespace D7
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            string[] words = lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
            
            // This linq request parses the array and sorts it
            int[] positions = words.Select(int.Parse).OrderBy(x => x).ToArray();
            int n = positions.Length/2;

            Stopwatch sw = new Stopwatch();

            // Part 1
            sw.Start();
            // The median m minimizes sum(abs(x-m)) which is what we need
            int median = positions[n];

            // This linq request calculates sum(abs(x-m))
            int fuel = positions.Sum(x => Math.Abs(x - median));

            sw.Stop();
            Console.WriteLine("Part 1:");
            Console.WriteLine($"Count: {fuel}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");



            // Part 2
            sw.Restart();
            int max = positions.Max();

            // This linq request calculates the minimum fuel consumption which is sum(abs(x-y)*(abs(x-y)+1)/2)
            // y is the final position
            // The max fuel consumption is near int overflow, using long for safety and scalability
            long fuel2 = Enumerable.Range(0,max+1).Min(x => positions.Sum(y => (long)Math.Abs(x - y)*(Math.Abs(x - y) + 1)/2));
            sw.Stop();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Count: {fuel2}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
        }
    }
}

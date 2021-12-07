using System;
using System.Linq;

namespace D7
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            string[] words = lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
            int[] positions = words.Select(int.Parse).OrderBy(x => x).ToArray();
            int n = positions.Length/2;

            int median = positions[n];

            int fuel = positions.Sum(x => Math.Abs(x - median));

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Count: {fuel}");
        }
    }
}

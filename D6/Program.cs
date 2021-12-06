using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace D6
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            string[] words = lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);

            const int iter = 80;

            // Part 1
            List<int> pop = words.Select(int.Parse).ToList();
            for (int i = 0; i < iter; i++) {
                int n = pop.Count;
                for (int j = 0; j < n; j++) {
                    if(--pop[j] < 0) {
                        pop[j] = 6;
                        pop.Add(8);
                    } 
                }
            }

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Count: {pop.Count}");



            // Part 2
            // The previous method is quite naive and will consume too much memory after a lot of iterations
            const int iter2 = 256;

            long[] count = new long[9];
            for (int i = 0; i < words.Length; i++) {
                count[int.Parse(words[i])]++;
            }

            for (int i = 0; i < iter2; i++) {
                long[] newCount = new long[9];
                for(int j = 0; j < 9; j++) {
                    if (j == 0) {
                        newCount[8] += count[0];
                        newCount[6] += count[0];
                    }
                    else {
                        newCount[j-1] += count[j];
                    }
                }
                Array.Copy(newCount, count, 9);
            }

            long sum = 0;

            foreach (long elem in count) {
                sum += elem;
            }

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Count: " + sum);
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace D24
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalSw = new Stopwatch();
            globalSw.Start();

            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            string[][] instructions = lines.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

            int n = 14;

            int[] a = new int[n];
            int[] b = new int[n];
            for (int i = 0; i < n; i++) {
                a[i] = int.Parse(instructions[5+18*i][2]);
                b[i] = int.Parse(instructions[15+18*i][2]);
            }

            int[,] relations = new int[7,3];

            Stack<(int,int)> stack = new Stack<(int,int)>();

            int k = 0;

            for (int i = 0; i < n; i++) {
                if (a[i] > 0) {
                    stack.Push((b[i],i));
                }
                else {
                    (int,int) pop = stack.Pop();
                    
                    relations[k,0] = i;
                    relations[k,1] = pop.Item2;

                    int diff = pop.Item1 + a[i];

                    relations[k,2] = diff;

                    k++;
                }
            }
            // Part 1

            var sw = new Stopwatch();
            sw.Start();
            
            int[] maxArray = new int[14];

            for (int i = 0; i < 7; i++) {
                if (relations[i,2] >= 0) {
                    maxArray[relations[i,0]] = 9;
                    maxArray[relations[i,1]] = 9 - relations[i,2];
                }
                else {
                    maxArray[relations[i,1]] = 9;
                    maxArray[relations[i,0]] = 9 + relations[i,2];
                }
            }

            long max = ArrayToLong(maxArray);

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Max model number: {max}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

            globalSw.Stop();
            Console.WriteLine($"\nTotal execution time: {globalSw.ElapsedMilliseconds} ms");
        }

        static long ArrayToLong(int[] array) {
            long res = 0L;
            foreach(int elem in array) {
                res = (long)(res * 10 + (long)elem);
            }
            return res;
        }
    }
}

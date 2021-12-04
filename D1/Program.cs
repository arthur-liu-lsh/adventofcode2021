using System;
using System.Collections.Generic;
using System.Collections;

namespace D1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int[] depths = Array.ConvertAll(lines, int.Parse); 
            
            int n = depths.Length;
            int count = 0;

            for (int i = 0; i<n-1; i++) {
                if (depths[i] < depths[i+1]) {
                    count++;
                }
            }

            Console.WriteLine("Single count: " + count.ToString());

            int count2 = 0;

            for (int i = 1; i<n-2; i++) {
                if ((depths[i-1] + depths[i] + depths[i+1]) < (depths[i] + depths[i+1] + depths[i+2])) {
                    count2++;
                }
            }

            Console.WriteLine("Three-measurement sliding window: " + count2.ToString());
        }
    }
}

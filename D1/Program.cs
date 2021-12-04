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

            Console.WriteLine(count);
        }
    }
}

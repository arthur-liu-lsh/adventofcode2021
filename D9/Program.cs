using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace D9
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = lines.Length;
            int m = lines[0].Length;

            int[,] table = new int[n,m];
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    table[i,j] = (int)Char.GetNumericValue(lines[i][j]);
                }
            }

            // Part 1
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int sum = 0;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    int[] neighbours = Neighbours(table, i, j);
                    int value = table[i,j];
                    if (IsLocalMinimum(value,neighbours)) {
                        sum += value+1;
                    }
                }
            }

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Sum of risk levels: {sum}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

            // Part 2
            sw.Restart();

            List<int> sums = new List<int>();
            bool[,] isChecked = new bool[n,m];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    int value = table[i,j];
                    if (value != 9 && !isChecked[i,j]) {
                        sums.Add(BasinSize(table,isChecked,i,j));
                    }
                }
            }

            List<int> biggestBasins = sums.OrderByDescending(x => x).Take(3).ToList();

            int product = 1;

            foreach (int elem in biggestBasins) {
                product *= elem;
            }

            sw.Stop();

            Console.WriteLine("\nPart 2:");
            Console.WriteLine($"Product of the size of the three biggest basins: {product}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

        }

        static int[] Neighbours(int[,] table, int i, int j){
            
            int[] neighbours = new int[] {-1, -1, -1, -1};

            if (i > 0) {
                neighbours[0] = table[i-1,j];
            }
            if (i < table.GetLength(0)-1) {
                neighbours[1] = table[i+1,j];
            }
            if (j > 0) {
                neighbours[2] = table[i,j-1];
            }
            if (j < table.GetLength(1)-1) {
                neighbours[3] = table[i,j+1];
            }
            return neighbours;
        }

        static bool IsLocalMinimum(int number, int[] neighbours) {
            for (int k = 0; k < neighbours.Length; k ++) {
                if (neighbours[k] != -1 && neighbours[k] <= number) {
                    return false;
                }
            }
            return true;
        }

        static int BasinSize(int[,] table, bool[,] isChecked, int i, int j) {
            int value = table[i,j];
            int[] neighbours = Neighbours(table, i, j);
            int size = 1;
            isChecked[i,j] = true;
            if (i > 0) {
                if (table[i-1,j] != 9 && !isChecked[i-1,j]) {
                    size += BasinSize(table, isChecked, i-1, j);
                    isChecked[i-1,j] = true;
                }
            }
            if (i < table.GetLength(0)-1) {
                if (table[i+1,j] != 9 && !isChecked[i+1,j]) {
                    size += BasinSize(table, isChecked, i+1, j);
                    isChecked[i+1,j] = true;
                }
            }
            if (j > 0) {
                if (table[i,j-1] != 9 && !isChecked[i,j-1]) {
                    size += BasinSize(table, isChecked, i, j-1);
                    isChecked[i,j-1] = true;
                }
            }
            if (j < table.GetLength(1)-1) {
                if (table[i,j+1] != 9 && !isChecked[i,j+1]) {
                    size += BasinSize(table, isChecked, i, j+1);
                    isChecked[i,j+1] = true;
                }
            }
            return size;
        }
    }
}

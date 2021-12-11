using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace D11
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

            int days = 100;

            int flashes = 0;

            for (int k = 0; k < days; k++) {
                bool[,] isChecked = new bool[n,m];
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < m; j++) {
                        table[i,j] += 1;
                    }
                }
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < m; j++) {
                        Propagate(table,isChecked,i,j);
                    }
                }

                flashes += table.Cast<int>().Sum(x => x == 0 ? 1 : 0);
            }

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Sum of risk levels: {flashes}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
        }

        static void Propagate(int[,] table, bool[,] isChecked, int i, int j) {
            int value = table[i,j];

            if (table[i,j] < 10) {
                return;
            }

            isChecked[i,j] = true;
            table[i,j] = 0;

            if (i > 0 && j > 0 && !isChecked[i-1,j-1]) {
                table[i-1,j-1] += 1;
                Propagate(table, isChecked, i-1, j-1);
            }
            if (i > 0 && !isChecked[i-1,j]) {
                table[i-1,j] += 1;
                Propagate(table, isChecked, i-1, j);
            }
            if (i > 0 && j < table.GetLength(1)-1 && !isChecked[i-1,j+1]) {
                table[i-1,j+1] += 1;
                Propagate(table, isChecked, i-1, j+1);
            }
            if (j > 0 && !isChecked[i,j-1]) {
                table[i,j-1] += 1;
                Propagate(table, isChecked, i, j-1);
            }
            if (j < table.GetLength(1)-1 && !isChecked[i,j+1]) {
                table[i,j+1] += 1;
                Propagate(table, isChecked, i, j+1);
            }
            if (i < table.GetLength(0)-1 && j > 0 && !isChecked[i+1,j-1]) {
                table[i+1,j-1] += 1;
                Propagate(table, isChecked, i+1, j-1);
            }
            if (i < table.GetLength(0)-1 && !isChecked[i+1,j]) {
                table[i+1,j] += 1;
                Propagate(table, isChecked, i+1, j);
            }
            if (i < table.GetLength(0)-1 && j < table.GetLength(1)-1 && !isChecked[i+1,j+1]) {
                table[i+1,j+1] += 1;
                Propagate(table, isChecked, i+1, j+1);
            }
            return;
        }
    }
}

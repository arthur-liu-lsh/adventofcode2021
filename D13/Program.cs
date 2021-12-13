using System;
using System.Linq;
using System.Diagnostics;

namespace D13
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = 0;

            for (int i = 0; i < lines.Length; i++) {
                if (lines[i] == "") {
                    n = i;
                    break;
                }
            }

            int m = lines.Length - n - 1;

            // Generate array of coordinates and fill it
            int[][] coordinates = new int[n][];
            for(int i = 0; i < n; i++) {
                string[] line = lines[i].Split(',',StringSplitOptions.RemoveEmptyEntries);
                coordinates[i] = line.Select(int.Parse).ToArray();
            }

            int maxX = coordinates.Max(x => x[0])+1;
            int maxY = coordinates.Max(x => x[1])+1;

            // Generate array of instructions and fill it
            string[][] instructions = new string[m][];
            for(int i = 0; i < m; i++) {
                string[] line = lines[n+i+1].Split(new char[] {' ', '='},StringSplitOptions.RemoveEmptyEntries);
                instructions[i] = line;
            }

            bool[][] paper = new bool[maxY][];
            for (int i = 0; i < maxY; i++) {
                paper[i] = new bool[maxX];
            }
            foreach (int[] coordinate in coordinates) {
                paper[coordinate[1]][coordinate[0]] = true;
            }

            // Part 1
            var sw = new Stopwatch();
            sw.Start();
 
            int dots = Fold(paper,instructions[0][2]).Sum(y => y.Count(x => x == true));

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Number of dots: {dots}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

            // Part 2
            sw.Restart();

            foreach(string[] instruction in instructions) {
                paper = Fold(paper, instruction[2]);
            }

            Console.WriteLine("\nPart 2:");
            Print(paper);
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

        }

        static void Print(bool[][] table) {
            int n = table.Length;
            int m = table[0].Length;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    Console.Write(table[i][j] ? "█" : " ");
                }
                Console.WriteLine();
            }
        }

        static bool[][] Fold(bool[][] table, string axis) {
            int n = table.Length;
            int m = table[0].Length;
            bool[][] newTable = new bool[1][];
            if (axis == "y") {
                newTable = new bool[n/2][];
                for (int i = 0; i < n/2; i++) {
                    newTable[i] = new bool[m];
                }
                for (int i = 0; i < n/2; i++) {
                    for (int j = 0; j < m; j++) {
                        if (table[i][j] || table[n-i-1][j]) {
                            newTable[i][j] = true;
                        }
                    }
                }
            }
            if (axis == "x") {
                newTable = new bool[n][];
                for (int i = 0; i < n; i++) {
                    newTable[i] = new bool[m/2];
                }
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < m/2; j++) {
                        if (table[i][j] || table[i][m-j-1]) {
                            newTable[i][j] = true;
                        }
                    }
                }
            }
            return newTable;
        }
    }
}

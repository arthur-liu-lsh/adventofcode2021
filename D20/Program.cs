using System;
using System.Diagnostics;

namespace D20
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalSw = new Stopwatch();
            globalSw.Start();
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = lines.Length-2;
            int m = lines[2].Length;
            
            bool[,] inputTable = new bool[n,m];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    if (lines[i+2][j] == '#') {
                        inputTable[i,j] = true;
                    }
                }
            }

            bool[] rule = GetAlgorithm(lines[0]);

            const int iter1 = 2;

            var sw = new Stopwatch();
            sw.Start();

            bool[,] table = (bool[,])inputTable.Clone();

            for (int k = 0; k < iter1; k++) {

                bool invert = k%2==1;

                table = Padding(inputTable, invert);
                n = table.GetLength(0);
                m = table.GetLength(1);
                bool[,] newTable = new bool[n,m];

                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < m; j++) {
                        bool[] code = GetCode(table,i,j, invert);
                        int index = BoolArrayToInt(code);
                        newTable[i,j] = LookupAlgorithm(rule, index);
                    }
                }
                table = newTable;
            }

            int count1 = Count(table);
            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Number of lit pixels after 2 steps: {count1}");
            Console.WriteLine($"Calculations execution time: {sw.ElapsedMilliseconds} ms");

            sw.Start();

            table = (bool[,])inputTable.Clone();


            const int iter2 = 50;

            for (int k = 0; k < iter2; k++) {

                bool invert = k%2==1;

                table = Padding(table, invert);
                n = table.GetLength(0);
                m = table.GetLength(1);
                bool[,] newTable = new bool[n,m];

                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < m; j++) {
                        bool[] code = GetCode(table,i,j, invert);
                        int index = BoolArrayToInt(code);
                        newTable[i,j] = LookupAlgorithm(rule, index);
                    }
                }
                table = newTable;
            }

            int count2 = Count(table);
            sw.Stop();

            Console.WriteLine("\nPart 2:");
            Console.WriteLine($"Number of lit pixels after 2 steps: {count2}");
            Console.WriteLine($"Calculations execution time: {sw.ElapsedMilliseconds} ms");
            
            globalSw.Stop();
            Console.WriteLine($"\nTotal execution time: {globalSw.ElapsedMilliseconds} ms");

        }

        static bool[] GetCode(bool[,] table, int i, int j, bool cycle) {
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            bool[] code = new bool[9];

            if (cycle) {
                for (int k = 0; k < 9; k++) {
                    code[k] = true;
                }
            }

            if (i > 0 && j > 0) {
                code[0] = table[i-1,j-1];
            }
            if (i > 0) {
                code[1] = table[i-1,j];
            }
            if (i > 0 && j < m-1) {
                code[2] = table[i-1,j+1];
            }
            if (j > 0) {
                code[3] = table[i,j-1];
            }
            code[4] = table[i,j];
            if (j < m-1) {
                code[5] = table[i,j+1];
            }
            if (i < n-1 && j > 0) {
                code[6] = table[i+1,j-1];
            }
            if (i < n-1) {
                code[7] = table[i+1,j];
            }
            if (i < n-1 && j < m-1) {
                code[8] = table[i+1,j+1];
            }
            return code;
        }

        static int BoolArrayToInt(bool[] array) {
            int res = 0;
            foreach (bool bit in array) {
                res = res << 1;
                res += bit ? 1 : 0;
            }
            return res;
        }

        static bool[] GetAlgorithm(string str) {
            int n = str.Length;
            bool[] rule = new bool[n];
            for (int i = 0; i < n; i++) {
                if (str[i] == '#') {
                    rule[i] = true;
                }
            }
            return rule;
        }

        static bool LookupAlgorithm(bool[] rule, int index) {
            return rule[index];
        }

        static bool[,] Padding(bool[,] table, bool cycle) {
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            bool[,] newTable = new bool[n+2,m+2];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    if (table[i,j]) {
                        newTable[i+1,j+1] = true;
                    }
                }
            }

            if (cycle) {
                for (int i = 0; i < n+2; i++) {
                    newTable[i,0] = true;
                    newTable[i,m+1] = true;
                }
                for (int j = 0; j < m+2; j++) {
                    newTable[0,j] = true;
                    newTable[n+1,j] = true;
                }
            }

            return newTable;
        }

        static int Count(bool[,] table) {
            int n = table.GetLength(0);
            int m = table.GetLength(1);

            int count = 0;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    if (table[i,j]) {
                        count++;
                    }
                }
            }
            return count;
        }
        static void Print(bool[,] table) {
            int n = table.GetLength(0);
            int m = table.GetLength(1);

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    Console.Write(table[i,j] ? '█' : ' ');
                }
                Console.WriteLine();
            }
        }
    }
}

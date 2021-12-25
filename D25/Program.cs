using System;
using System.Diagnostics;

namespace D25
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalSw = new Stopwatch();
            globalSw.Start();

            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = lines.Length;
            int m = lines[0].Length;

            char[,] state = new char[n,m];

            for (int i = 0; i < n; i ++) {
                for (int j = 0; j < m; j++) {
                    if (lines[i][j] != '.') {
                        state[i,j] = lines[i][j];
                    }
                }
            }

            var sw = new Stopwatch();
            sw.Start();

            bool changed1 = true;
            bool changed2 = true;
            int iter = 0;

            while (changed1 || changed2) {
                (state,changed1) = Iterate(state,false);
                (state,changed2) = Iterate(state,true);
                iter++;
            }

            sw.Stop();

            Console.WriteLine($"Number of iterations: {iter}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
            
            globalSw.Stop();
            Console.WriteLine($"\nTotal execution time: {globalSw.ElapsedMilliseconds} ms");
        }

        static (char[,],bool) Iterate(char[,] state, bool cycle) {
            int n = state.GetLength(0);
            int m = state.GetLength(1);

            char[,] newState = new char[n,m];
            bool changed = false;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    if (!cycle) {
                        if (state[i,j] == '>' && state[i,(j+1)%m] == '\0') {
                            newState[i,(j+1)%m] = '>';
                            changed = true;
                        }
                        else {
                            if (newState[i,j] == '\0') {
                                newState[i,j] = state[i,j];
                            }
                        }
                    }
                    else {
                        if (state[i,j] == 'v' && state[(i+1)%n,j] == '\0') {
                            newState[(i+1)%n,j] = 'v';
                            changed = true;
                        }
                        else {
                            if (newState[i,j] == '\0') {
                                newState[i,j] = state[i,j];
                            }
                        }
                    }
                }
            }
            return (newState,changed);
        }

        static void Print(char[,] state) {
            int n = state.GetLength(0);
            int m = state.GetLength(1);

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    if (state[i,j] != '\0') {
                        Console.Write(state[i,j]);
                    }
                    else {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

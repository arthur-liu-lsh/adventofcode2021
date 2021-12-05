using System;
using System.Collections.Generic;

namespace D4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            // string[] test = {"1 2 3", "4 5 6", "7 8 9"};
            // Bingo testGrid = new Bingo(test);

            int[] drawnNumbers = Array.ConvertAll(lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries), int.Parse); 
            int m = drawnNumbers.Length;

            // Number of grids
            int n = (lines.Length-1)/6;

            Bingo[] grids = new Bingo[n];

            string[] values = new string[5];
            for(int i = 0; i < n; i++) {
                Array.Copy(lines,2+6*i,values,0,5);
                grids[i] = new Bingo(values);
            }

            // Part 1
            int unmarked = -1;
            int winningNumber = -1;
            bool exit = false;
            for (int j = 0; j < m && !exit; j++) {
                for (int i = 0; i < n && !exit; i++) {
                    if (grids[i].Play(drawnNumbers[j])) {
                        winningNumber = drawnNumbers[j];
                        unmarked = grids[i].Evaluate();
                        exit = true;
                    }
                }
            }

            Console.WriteLine("Part 1:");
            Console.WriteLine("Sum: " + unmarked.ToString());
            Console.WriteLine("Winning number: " + winningNumber.ToString());
            Console.WriteLine("Answer: " + (unmarked*winningNumber).ToString());

            // Part 2
            unmarked = -1;
            winningNumber = -1;
            for (int j = 0; j < m; j++) {
                for (int i = 0; i < n; i++) {
                    if (grids[i].Play(drawnNumbers[j])) {
                        winningNumber = drawnNumbers[j];
                        unmarked = grids[i].Evaluate();
                    }
                }
            }

            Console.WriteLine("\nPart 2:");
            Console.WriteLine("Sum: " + unmarked.ToString());
            Console.WriteLine("Winning number: " + winningNumber.ToString());
            Console.WriteLine("Answer: " + (unmarked*winningNumber).ToString());
        }
    }
}

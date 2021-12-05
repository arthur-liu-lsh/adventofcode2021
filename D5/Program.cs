using System;
using System.Linq;

namespace D5
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = lines.Length;

            int[] x1 = new int[n];
            int[] y1 = new int[n];
            int[] x2 = new int[n];
            int[] y2 = new int[n];

            char[] separators = {' ', ',', '>', '-'};

            for (int i = 0; i < n; i++) {
                string[] words = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                x1[i] = Int32.Parse(words[0]);
                y1[i] = Int32.Parse(words[1]);
                x2[i] = Int32.Parse(words[2]);
                y2[i] = Int32.Parse(words[3]);
            }

            int[] xMax = {x1.Max(),x2.Max()};
            int[] yMax = {y1.Max(),y2.Max()};
            int xSize = xMax.Max()+1;
            int ySize = yMax.Max()+1;

            // Part 1
            int[,] grid = new int[xSize,ySize];

            for (int i = 0; i < n; i++) {
                if (x1[i] == x2[i] || y1[i] == y2[i]) {
                    int xLow = Math.Min(x1[i],x2[i]);
                    int xHigh = Math.Max(x1[i],x2[i]);
                    int yLow = Math.Min(y1[i],y2[i]);
                    int yHigh = Math.Max(y1[i],y2[i]);
                    for (int x = xLow; x < xHigh+1; x++) {
                        for (int y = yLow; y < yHigh+1; y++) {
                            grid[x,y]++;
                        }
                    }
                }
            }
            
            int count = 0;
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    if (grid[x,y] > 1) {
                        count++;
                    }
                }
            }

            Console.WriteLine("Part 1:");
            Console.WriteLine("Count: " + count.ToString());

            // Part 2
            grid = new int[xSize,ySize];

            for (int i = 0; i < n; i++) {
                int k = 0;
                int iter = Math.Max(Math.Abs(x1[i]-x2[i]),Math.Abs(y1[i]-y2[i]));
                
                int xMult = x1[i]<x2[i] ? 1 :-1;
                int yMult = y1[i]<y2[i] ? 1 :-1;
                xMult = x1[i]==x2[i] ? 0 : xMult;
                yMult = y1[i]==y2[i] ? 0 : yMult;
                while (k < iter+1) {
                    grid[x1[i]+xMult*k,y1[i]+yMult*k]++;
                    k++;
                }
            }
            
            count = 0;
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    if (grid[x,y] > 1) {
                        count++;
                    }
                }
            }

            Console.WriteLine("Part 2:");
            Console.WriteLine("Count: " + count.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace D15
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
                    table[i,j] = (int)(lines[i][j] - '0');
                }
            }

            // Part 1
            var sw = new Stopwatch();
            sw.Start();

            int[,] distances = Dijkstra(table, 0,0);

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Shortest distance: {distances[n-1,m-1]}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

            const int upscaleRatio = 5;

            int[,] bigTable = Upscale(table, upscaleRatio);

            sw.Restart();

            int[,] bigDistances = Dijkstra(bigTable, 0,0);

            sw.Stop();

            Console.WriteLine("\nPart 2:");
            Console.WriteLine($"Shortest distance: {bigDistances[upscaleRatio*n-1,upscaleRatio*m-1]}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");


        }

        static void Print(int[,] table) {
            int n = table.GetLength(0);
            int m = table.GetLength(1);

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    Console.Write(table[i,j]);
                }
                Console.WriteLine();
            }
        }

        static int[,] Upscale(int[,] table, int upscaleRatio) {
            int n = table.GetLength(0);
            int m = table.GetLength(1);
            int[,] bigTable = new int[upscaleRatio*n,upscaleRatio*m];
            for (int ii = 0; ii < upscaleRatio; ii++) {
                for (int jj = 0; jj < upscaleRatio; jj++) {
                    for (int i = 0; i < n; i++) {
                        for (int j = 0; j < m; j++) {
                            bigTable[n*ii + i, m*jj + j] = (table[i,j]+ii+jj-1)%9+1;
                        }
                    }
                }
            }
            return bigTable;
        }

        static int[,] Dijkstra(int[,] table, int beginY, int beginX) {
            int n = table.GetLength(0);
            int m = table.GetLength(1);

            int[,] distances = new int[n,m];
            
            var modified = new HashSet<Tuple<int, int>>();
            modified.Add(Tuple.Create<int,int>(0,0));


            bool[,] vertices = new bool[n,m];
            int count = 0;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    distances[i,j] = 10*n*m;
                }
            }
            distances[beginY,beginX] = 0;

            while (count < n*m) {
                int[] v = MinVertex(distances, modified);

                modified.Remove(new Tuple<int, int>(v[0],v[1]));
                count++;
                
                if (v[0] > 0) {
                    distances[v[0]-1,v[1]] = UpdateDistance(distances,table,new Tuple<int,int>(v[0],v[1]), new Tuple<int,int>(v[0]-1,v[1]), modified);
                }
                if (v[0] < n-1) {
                    distances[v[0]+1,v[1]] = UpdateDistance(distances,table,new Tuple<int,int>(v[0],v[1]), new Tuple<int,int>(v[0]+1,v[1]), modified);
                }
                if (v[1] > 0) {
                    distances[v[0],v[1]-1] = UpdateDistance(distances,table,new Tuple<int,int>(v[0],v[1]), new Tuple<int,int>(v[0],v[1]-1), modified);
                }
                if (v[1] < m-1) {
                    distances[v[0],v[1]+1] = UpdateDistance(distances,table,new Tuple<int,int>(v[0],v[1]), new Tuple<int,int>(v[0],v[1]+1), modified);
                }
            }
            return distances;
        }

        static int[] MinVertex(int[,] distances, HashSet<Tuple<int,int>> modified) {
            int n = distances.GetLength(0);
            int m = distances.GetLength(1);
            int min = 10*n*m;
            int vY = 0;
            int vX = 0;
            foreach (var elem in modified) {
                if (distances[elem.Item1,elem.Item2] < min) {
                    min = distances[elem.Item1,elem.Item2];
                    vY = elem.Item1;
                    vX = elem.Item2;
                }
            }
            return new int[] {vY,vX};
        }

        static int UpdateDistance(int[,] distances, int[,] table, Tuple<int,int> v1, Tuple<int,int> v2, HashSet<Tuple<int,int>> modified) {
            if (distances[v2.Item1,v2.Item2] > distances[v1.Item1,v1.Item2] + table[v2.Item1,v2.Item2]) {
                modified.Add(v2);
                return distances[v1.Item1,v1.Item2] + table[v2.Item1,v2.Item2];
            }
            return distances[v2.Item1,v2.Item2];
        }
    }
}

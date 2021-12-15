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

            int[,] bigTable = new int[5*n,5*m];
            for (int ii = 0; ii < 5; ii++) {
                for (int jj = 0; jj < 5; jj++) {
                    for (int i = 0; i < n; i++) {
                        for (int j = 0; j < m; j++) {
                            bigTable[5*ii + i, 5*ii + j] = (table[i,j]+ii+jj-1)%9+1;
                        }
                    }
                }
            }

            // int[,] bigDistances = Dijkstra(bigTable, 0,0);
            // Console.WriteLine(bigDistances[5*n-1,5*m-1]);
        }

        static int[,] Dijkstra(int[,] table, int beginY, int beginX) {
            int n = table.GetLength(0);
            int m = table.GetLength(1);

            int[,] distances = new int[n,m];
            var vertices = new HashSet<Tuple<int, int>>();

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    vertices.Add(Tuple.Create<int,int>(i,j));
                }
            }

            // bool[,] vertices = new bool[n,m];
            // int count = 0;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    distances[i,j] = 10*n*m;
                }
            }
            distances[beginY,beginX] = 0;

            while (vertices.Count > 0) {

                int min = 10*n*m;
                int vY = 0;
                int vX = 0;
                // for (int i = 0; i < n; i++) {
                //     for (int j = 0; j < m; j++) {
                //         if (!vertices[i,j] && distances[i,j] < min) {
                //             min = distances[i,j];
                //             vY = i;
                //             vX = j;
                //         }
                //     }
                // }
                foreach (var vertex in vertices) {
                    if (distances[vertex.Item1,vertex.Item2] < min) {
                        min = distances[vertex.Item1,vertex.Item2];
                        vY = vertex.Item1;
                        vX = vertex.Item2;
                    }
                }

                vertices.Remove(Tuple.Create<int,int>(vY,vX));

                // if (vY > 0) {
                //     distances[vY-1,vX] = UpdateDistance(distances[vY,vX],distances[vY-1,vX],table[vY-1,vX]);
                // }
                if (vY < n-1) {
                    distances[vY+1,vX] = UpdateDistance(distances[vY,vX],distances[vY+1,vX],table[vY+1,vX]);
                }
                // if (vX > 0) {
                //     distances[vY,vX-1] = UpdateDistance(distances[vY,vX],distances[vY,vX-1],table[vY,vX-1]);
                // }
                if (vX < m-1) {
                    distances[vY,vX+1] = UpdateDistance(distances[vY,vX],distances[vY,vX+1],table[vY,vX+1]);
                }
            }
            return distances;
        }

        static int UpdateDistance(int d1, int d2, int v2) {
            if (d2 > d1 + v2) {
                return d1 + v2;
            }
            return d2;
        }
    }
}

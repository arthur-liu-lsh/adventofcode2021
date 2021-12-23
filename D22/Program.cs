using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace D22
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

            string[] separators = new string[] {" ", "x", "y", "z", "=", "..", ","};

            int[][] coords = new int[n][];
            bool[] states = new bool[n];

            for (int i = 0; i < n; i++) {
                string[] words = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                states[i] = words[0] == "on" ? true : false;
                coords[i] = new int[words.Length-1];
                for (int j = 0; j < words.Length-1; j++) {
                    coords[i][j] = int.Parse(words[j+1]);
                }
            }

            // Part 1

            HashSet<(int x, int y, int z)> cubes = new HashSet<(int x, int y, int z)>();

            for (int i = 0; i < 20; i++) {
                SwitchCubes(cubes, coords[i][0], coords[i][1], coords[i][2], coords[i][3], coords[i][4], coords[i][5], states[i]);
            }

            var sw = new Stopwatch();
            sw.Start();
            
            long count50 = Count(cubes, -50, 50, -50, 50, -50, 50);

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Answer: {count50}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

            // Part 2

            sw.Restart();

            List<(int x1, int x2, int y1, int y2, int z1, int z2, int sign)> cubes2
            = new List<(int x1, int x2, int y1, int y2, int z1, int z2, int sign)>();

            for (int i = 0; i < n; i++) {
                (int x1, int x2, int y1, int y2, int z1, int z2, int state) newCube
                = (coords[i][0], coords[i][1], coords[i][2], coords[i][3], coords[i][4], coords[i][5], states[i] ? 1 : -1);

                FindOverlap(cubes2,newCube);
            }

            long countAll = Volume(cubes2);

            sw.Stop();

            Console.WriteLine("\nPart 2:");
            Console.WriteLine($"Answer: {countAll}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

            globalSw.Stop();
            Console.WriteLine($"\nTotal execution time: {globalSw.ElapsedMilliseconds} ms");
        }



        static void FindOverlap (
                List<(int x1, int x2, int y1, int y2, int z1, int z2, int sign)> cubes,
                (int x1, int x2, int y1, int y2, int z1, int z2, int sign) c1)
        {
            var toAdd = new List<(int x1, int x2, int y1, int y2, int z1, int z2, int sign)>();
            foreach ((int x1, int x2, int y1, int y2, int z1, int z2, int sign) c2 in cubes) {
                int x1 = Math.Max(c1.x1, c2.x1);
                int x2 = Math.Min(c1.x2, c2.x2);
                int y1 = Math.Max(c1.y1, c2.y1);
                int y2 = Math.Min(c1.y2, c2.y2);
                int z1 = Math.Max(c1.z1, c2.z1);
                int z2 = Math.Min(c1.z2, c2.z2);
                if (x1 <= x2 && y1 <= y2 && z1 <= z2) {
                    toAdd.Add((x1,x2,y1,y2,z1,z2,-c2.sign));
                }
            }
            foreach((int x1, int x2, int y1, int y2, int z1, int z2, int state) cube in toAdd) {
                cubes.Add(cube);
            }
            if (c1.sign > 0) {
                cubes.Add(c1);
            }
        }

        static long Volume(List<(int x1, int x2, int y1, int y2, int z1, int z2, int sign)> cubes) {
            long sum = 0L;
            foreach ((int x1, int x2, int y1, int y2, int z1, int z2, int sign) cube in cubes) {
                int x = cube.x2-cube.x1+1;
                int y = cube.y2-cube.y1+1;
                int z = cube.z2-cube.z1+1;
                sum += (long)cube.sign*x*y*z;
            }
            return sum;
        }

        static void SwitchCubes(HashSet<(int x,int y,int z)> cubes, int minX, int maxX, int minY, int maxY, int minZ, int maxZ, bool state) {
            for (int x = minX; x <= maxX; x++) {
                for (int y = minY; y <= maxY; y++) {
                    for (int z = minZ; z <= maxZ; z++) {
                        if (state) {
                            cubes.Add((x,y,z));
                        }
                        else {
                            cubes.Remove((x,y,z));
                        }
                    }
                }
            }
        }

        static long Count(HashSet<(int x,int y,int z)> cubes) {
            return cubes.Count();
        }

        static long Count(HashSet<(int x,int y,int z)> cubes, int minX, int maxX, int minY, int maxY, int minZ, int maxZ) {
            long count = 0;
            for (int x = minX; x <= maxX; x++) {
                for (int y = minY; y <= maxY; y++) {
                    for (int z = minZ; z <= maxZ; z++) {
                        if (cubes.Contains((x,y,z))) {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}

using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.Diagnostics;

namespace D19
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalSw = new Stopwatch();
            globalSw.Start();
            string path = @"input.txt";
            string text = System.IO.File.ReadAllText(path);

            string[] scannersData = text.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);

            string[][] scannersCoordinates = scannersData.Select(x => x.Split("\n", StringSplitOptions.RemoveEmptyEntries)).ToArray();
            int nScanners = scannersData.Length;

            var scanners = new Scanner[nScanners];

            for (int i = 0; i < nScanners; i++) {
                int nCoordinates = scannersCoordinates[i].Length;
                string[] coordinates = new String[nCoordinates-1];
                Array.Copy(scannersCoordinates[i],1,coordinates,0,nCoordinates-1);
                scanners[i] = new Scanner(coordinates);
            }

            Solver solver = new Solver(scanners[0]);

            for (int i = 1; i < nScanners; i++) {
                solver.AddScanner(scanners[i]);
            }

            var sw = new Stopwatch();
            sw.Start();

            solver.Solve();

            sw.Stop();

            Console.WriteLine("Part 1 and 2:");
            Console.WriteLine($"Number of sensors: {solver.GetBeaconCount()}");
            Console.WriteLine($"Biggest distance between sensors: {solver.GetMaxManhattanDistance()}");
            Console.WriteLine($"Calculations execution time: {sw.ElapsedMilliseconds} ms");

            globalSw.Stop();
            Console.WriteLine($"\nTotal execution time: {globalSw.ElapsedMilliseconds} ms");

        }

        static Vector3 StringToVector(string s) {
            return new Vector3(0,0,0);
        }
    }
}

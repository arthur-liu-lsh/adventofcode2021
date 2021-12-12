using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace D12
{
    class Program
    {



        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            
            string[][] paths = lines.Select(x => x.Split('-',StringSplitOptions.RemoveEmptyEntries)).ToArray();

            Graph graph = new Graph();

            foreach (string[] line in paths) {
                graph.AddPath(line[0],line[1]);
            }

            // Part 1
            var sw = new Stopwatch();
            sw.Start();
            
            int paths1 = graph.CountPaths("start", "end", new HashSet<string> { "start" });

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Number of paths: {paths1}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");
        }
    }
}

using System;
using System.Linq;
using System.Diagnostics;

namespace D8
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = lines.Length;
            int m = lines[0].Split(new char[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries).Length;

            string[][] inputs = lines.Select(x => x.Split(new char[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries)).ToArray();

            
        }
    }
}

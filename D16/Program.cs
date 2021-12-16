using System;

namespace D16
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            int n = lines[0].Length;
            string messageHex = lines[0];
        }
    }
}

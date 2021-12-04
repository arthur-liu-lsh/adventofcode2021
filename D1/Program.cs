using System;
using System.Collections.Generic;
using System.Collections;

namespace D1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            foreach(string s in lines) {
                Console.WriteLine(s);
            }
        }
    }
}

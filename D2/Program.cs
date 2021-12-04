using System;

namespace D2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int position = 0;
            int depth = 0;
            int n = lines.Length;

            // Part 1

            for (int i = 0; i<n; i++) {
                string[] words = lines[i].Split(' ');
                
                switch(words[0]) {
                    case "forward" :
                        position += Int32.Parse(words[1]);
                        break;
                    case "up" :
                        depth -= Int32.Parse(words[1]);
                        break;
                    case "down" :
                        depth += Int32.Parse(words[1]);
                        break;
                }
            }

            Console.WriteLine("Part 1:");
            Console.WriteLine("Position: " + position.ToString());
            Console.WriteLine("Depth: " + depth.ToString());
            Console.WriteLine("Position*Depth: " + (position*depth).ToString());

            // Part 2

            position = 0;
            depth = 0;
            int aim = 0;

            for (int i = 0; i<n; i++) {
                string[] words = lines[i].Split(' ');
                
                switch(words[0]) {
                    case "forward" :
                        position += Int32.Parse(words[1]);
                        depth += Int32.Parse(words[1])*aim;
                        break;
                    case "up" :
                        aim -= Int32.Parse(words[1]);
                        break;
                    case "down" :
                        aim += Int32.Parse(words[1]);
                        break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine("Position: " + position.ToString());
            Console.WriteLine("Depth: " + depth.ToString());
            Console.WriteLine("Position*Depth: " + (position*depth).ToString());
        }
    }
}

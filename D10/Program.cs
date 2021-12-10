using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace D10
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = lines.Length;

            // Part 1
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int sum = 0;
            for (int i = 0; i < n; i++) {
                Stack<char> stack = new Stack<char>();
                foreach (char ch in lines[i]) {
                    int points = ReadCharacter(stack, ch);
                    if (points != 0) {
                        sum += points;
                        break;
                    }
                    
                }
            }

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Syntax error score: {sum}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

            // Part 2
            sw.Restart();
            
            long[] scores = new long[n];

            for (int i = 0; i < n; i++) {
                bool skip = false;
                Stack<char> stack = new Stack<char>();
                foreach (char ch in lines[i]) {
                    int points = ReadCharacter(stack, ch);
                    if (points != 0) {
                        skip = true;
                        break;
                    }
                }
                while (stack.Count > 0 && !skip) {
                        scores[i] *= 5;
                        scores[i] += CharToPoints(stack.Pop());
                }
            }

            long[] sortedScores = scores.Where(x => x > 0).OrderBy(x => x).ToArray();
            long median = sortedScores[sortedScores.Length/2];

            sw.Stop();

            Console.WriteLine("\nPart 2:");
            Console.WriteLine($"Syntax error score: {median}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

        }

        static int ReadCharacter(Stack<Char> stack, char ch) {
            if (IsOpeningCharacter(ch)) {
                stack.Push(ch);
            }
            else {
                char check = CheckCharacter(stack, ch);
                if (check != '0') {
                    return CharToPoints(ch);
                }
            }
            return 0;
        }

        static char CheckCharacter(Stack<Char> stack, char ch) {
            char compare = stack.Pop();
            switch(ch) {
                case ')':
                    if (compare != '(') {
                        return ')';
                    }
                    break;
                case ']':
                    if (compare != '[') {
                        return ']';
                    }
                    break;
                case '}':
                    if (compare != '{') {
                        return '}';
                    }
                    break;
                case '>':
                    if (compare != '<') {
                        return '>';
                    }
                    break;
            }
            return '0';
        }

        static bool IsOpeningCharacter(char ch) {
            if (ch == '(' || ch == '[' || ch == '{' || ch == '<') {
                return true;
            }
            return false;
        }

        static int CharToPoints(char ch) {
            switch(ch) {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                case '(':
                    return 1;
                case '[':
                    return 2;
                case '{':
                    return 3;
                case '<':
                    return 4;
            }
            return 0;
        }
    }
}

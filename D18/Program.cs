using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace D18
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

            var sw = new Stopwatch();
            sw.Start();

            List<(int value,int depth)> numbers = GenerateLine(lines[0]);

            for (int i = 1; i < n; i++) {
                numbers = Add(numbers, GenerateLine(lines[i]));

                Reduce(numbers);
            }
            int magnitude = Magnitude(numbers);

            sw.Stop();
            globalSw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Magnitude: {magnitude}");
            Console.WriteLine($"Calculations execution time: {sw.ElapsedMilliseconds} ms");

            globalSw.Start();
            sw.Restart();

            var inputs = new List<List<(int value,int depth)>>();
            for(int i = 0; i < n; i++) {
                inputs.Add(GenerateLine(lines[i]));
            }

            List<int> magnitudes = new List<int>();

            for(int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (i != j) {
                        var temp = Add(inputs[i],inputs[j]);
                        Reduce(temp);
                        magnitudes.Add(Magnitude(temp));
                    }
                }
            }

            int max = magnitudes.Max();

            sw.Stop();
            globalSw.Stop();

            Console.WriteLine("\nPart 2:");
            Console.WriteLine($"Max magnitude: {max}");
            Console.WriteLine($"Calculations execution time: {sw.ElapsedMilliseconds} ms");

            Console.WriteLine($"\nTotal execution time: {globalSw.ElapsedMilliseconds} ms");
        }

        static List<(int value,int depth)> GenerateLine(string input) {
            var res = new List<(int value, int depth)>();
            int depth = 0;
            foreach (char c in input) {
                if (Char.IsDigit(c)) {
                    res.Add(((int)(c-'0'),depth));
                }
                else if (c == '[') {
                    depth += 1;
                }
                else if (c == ']') {
                    depth -=1;
                }
            }
            return res;
        }

        static List<(int value,int depth)> Add(List<(int value,int depth)> list1, List<(int value,int depth)> list2) {
            var res = new List<(int value,int depth)>();
            foreach (var elem in list1) {
                res.Add((elem.value,elem.depth+1));
            }
            foreach (var elem in list2) {
                res.Add((elem.value,elem.depth+1));
            }
            return res;
        }

        static bool Explode(List<(int value,int depth)> list) {
            int n = list.Count;
            bool changed = false;

            for (int i = 0; i < n-1; i++) {
                if (list[i].depth > 4 && list[i].depth == list[i+1].depth) {
                    if (i > 0) {
                        list[i-1] = (list[i-1].value+list[i].value, list[i-1].depth);
                    }
                    if (i < n-2) {
                        list[i+2] = (list[i+2].value+list[i+1].value, list[i+2].depth);
                    }
                    list[i+1] = (0, list[i+1].depth -1);
                    list.RemoveAt(i);
                    changed = true;
                    break;
                }
            }
            return changed;
        }

        static bool Split(List<(int value,int depth)> list) {
            int n = list.Count;
            bool changed = false;

            for (int i = 0; i < n; i++) {
                if (list[i].value >= 10) {
                    int splitValue1;
                    int splitValue2;
                    if (list[i].value % 2 == 0) {
                        splitValue1 = list[i].value/2;
                        splitValue2 = list[i].value/2;
                    }
                    else {
                        splitValue1 = list[i].value/2;
                        splitValue2 = list[i].value/2+1;
                    }
                    list.Insert(i+1,(splitValue2,list[i].depth+1));
                    changed = true;
                    list[i] = (splitValue1, list[i].depth+1);
                    break;
                }
            }
            return changed;
        }

        static void Reduce(List<(int value,int depth)> list) {
            bool changed1 = true;
            bool changed2 = true;

            while (changed1 || changed2) {
                changed1 = Explode(list);
                if (changed1) {
                    continue;
                }
                changed2 = Split(list);
            }
        }

        static int Magnitude(List<(int value,int depth)> list) {
            var copy = new List<(int value,int depth)>(list);
            while (copy.Count > 1) {
                int maxDepth = copy.Max(x => x.depth);
                for(int i = 0; i < copy.Count; i++) {
                    if (copy[i].depth == maxDepth) {
                        int magnitude = 3*copy[i].value + 2*copy[i+1].value;
                        copy[i+1] = (magnitude, copy[i+1].depth -1);
                        copy.RemoveAt(i);
                        break;
                    }
                }
            }
            return copy[0].value;
        }

        // static string ToString(List<(int value,int depth)> list) {
        //     minDepth = 
        //     return $"[{ToString(list1)},{ToString(list2)}]";
        // }
    }
}

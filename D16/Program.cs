using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace D16
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalSw = new Stopwatch();
            globalSw.Start();
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            string hex = lines[0];

            string bin = HexToBin(hex);
            int n = bin.Length;

            int pos = 0;
            int version = 0;
            var values = new List<long>();

            var sw = new Stopwatch();
            sw.Start();

            var res = ReadPacket(bin, n, pos, values, true);
            pos = res.pos;
            version += res.version;

            sw.Stop();
            globalSw.Stop();

            Console.WriteLine("Part 1 and 2:");
            Console.WriteLine($"Version sum: {version}");
            Console.WriteLine($"Result: {values[0]}");
            Console.WriteLine($"Packet read execution time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Total execution time: {globalSw.ElapsedMilliseconds} ms");
        }

        static string HexToBin(string message) {
            return String.Join("", message.Select(x => Convert.ToString(Convert.ToInt32(x+"", 16), 2).PadLeft(4,'0')));
        }

        static int BinToInt(string message) {
            return Convert.ToInt32(message, 2);
        }
        static long BinToLong(string message) {
            return Convert.ToInt64(message, 2);
        }

        static (int version, int type, int pos) ReadPacket(string message, int messageLength, int pos, List<long> values, bool skipZeros) {
            int version = BinToInt(message.Substring(pos, 3));
            pos += 3;
            int type = BinToInt(message.Substring(pos, 3));
            pos += 3;
            if (type == 4) {
                bool stop = false;
                string value = "";
                while (!stop) {
                    if (message[pos] == '0') {
                        stop = true;
                    }
                    pos += 1;
                    value += message.Substring(pos, 4);
                    pos += 4;
                }
                values.Add(BinToLong(value));
                if (skipZeros && pos % 4 != 0) {
                    pos += 4-pos%4;
                }
                return (version, type, pos);
            }
            else {
                var subValues = new List<long>();
                if (message[pos] == '0') {
                    pos += 1;
                    int subLength = BinToInt(message.Substring(pos, 15));
                    pos += 15;
                    string substring = message.Substring(pos, subLength);
                    int subPos = 0;
                    while (subPos < subLength) {
                        var res = ReadPacket(substring, subLength, subPos, subValues, false);
                        subPos = res.pos;
                        version += res.version;
                    }
                    pos += subLength;
                }
                else if (message[pos] == '1') {
                    pos += 1;
                    int subPacket = BinToInt(message.Substring(pos, 11));
                    pos += 11;
                    for(int i = 0; i < subPacket; i++) {
                        var res = ReadPacket(message, messageLength, pos, subValues, false);
                        pos = res.pos;
                        version += res.version;
                    }
                }
                values.Add(Operation(type, subValues));
                if (skipZeros && pos % 4 != 0) {
                    pos += 4-pos%4;
                }
                return (version, type, pos);
            }
        }

        static long Operation(int type, List<long> values) {
            long res = 0L;
            switch(type) {
                case 0: // Sum
                    res = values.Sum();
                    break;
                case 1: // Product
                    res = values.Aggregate(1L, (acc, val) => acc * val);
                    break;
                case 2: // Minimum
                    res = values.Min();
                    break;
                case 3: // Maximum
                    res = values.Max();
                    break;
                case 5: // Greater than
                    res = (values[0] > values[1]) ? 1 : 0;
                    break;
                case 6: // Less than
                    res = (values[0] < values[1]) ? 1 : 0;
                    break;
                case 7: // Equal to
                    res = (values[0] == values[1]) ? 1 : 0;
                    break;
            }
            return res;
        }
    }
}

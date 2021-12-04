using System;

namespace D3
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            int n = lines.Length;
            int m = lines[0].Length;

            int gamma = 0;
            int epsilon = 0;

            int count = 0;

            for(int j = 0; j<m; j++) {
                count = 0;
                for(int i = 0; i<n; i++) {
                    if (lines[i][j] == '1') {
                        count++;
                    }
                }
                // if majority of ones in column j
                if (2*count > n) { //if (count > n - count)
                    gamma = (gamma << 1) + 1;
                    epsilon = epsilon << 1;
                }
                else {
                    gamma = gamma << 1;
                    epsilon = (epsilon << 1) + 1;
                }
            }

            Console.WriteLine("Gamma: " + gamma.ToString());
            Console.WriteLine("Epsilon: " + epsilon.ToString());
            Console.WriteLine("Gamma*Epsilon: " + (gamma*epsilon).ToString());
        }
    }
}

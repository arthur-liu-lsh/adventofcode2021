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

            Console.WriteLine("Part 1:");
            Console.WriteLine("Gamma: " + gamma.ToString());
            Console.WriteLine("Epsilon: " + epsilon.ToString());
            Console.WriteLine("Gamma*Epsilon: " + (gamma*epsilon).ToString());

            bool go = true;

            bool[] skip = new bool[n];
            bool[] ones = new bool[n];
            bool[] zeros = new bool[n];

            int count0 = -1;
            int count1 = -1;

            // Oxygen
            int oxygenIndex = -1;
            int k = 0;
            while (go && k < m) {
                count0 = 0;
                count1 = 0;
                for(int i = 0; i<n; i++) {
                    if (!skip[i] && (lines[i][k] == '1')) {
                        ones[i] = true;
                        count1++;
                    }
                    else if (!skip[i] && (lines[i][k] == '0')){
                        zeros[i] = true;
                        count0++;
                    }
                }
                if (count1 >= count0) {
                    Array.Copy(zeros, skip, n);
                    if (count1 == 1) {
                        go = false;
                        oxygenIndex = argFalse(skip);
                    }
                    Array.Copy(zeros, ones, n);
                }
                else {
                    Array.Copy(ones, skip, n);
                    if (count0 == 1) {
                        go = false;
                        oxygenIndex = argFalse(skip);
                    }
                    Array.Copy(ones, zeros, n);
                }
                k++;
            }

            go = true;
            skip = new bool[n];
            ones = new bool[n];
            zeros = new bool[n];

            //CO2
            int co2Index = -1;
            k = 0;
            while (go && k < m) {
                count0 = 0;
                count1 = 0;
                for(int i = 0; i<n; i++) {
                    if (!skip[i] && (lines[i][k] == '1')) {
                        ones[i] = true;
                        count1++;
                    }
                    else if (!skip[i] && (lines[i][k] == '0')){
                        zeros[i] = true;
                        count0++;
                    }
                }
                if (count1 < count0) {
                    Array.Copy(zeros, skip, n);
                    if (count1 == 1) {
                        go = false;
                        co2Index = argFalse(skip);
                    }
                    Array.Copy(zeros, ones, n);
                }
                else {
                    Array.Copy(ones, skip, n);
                    if (count0 == 1) {
                        go = false;
                        co2Index = argFalse(skip);
                    }
                    Array.Copy(ones, zeros, n);
                }
                k++;
            }
    
            int oxygen = Convert.ToInt32(lines[oxygenIndex],2);
            int co2 = Convert.ToInt32(lines[co2Index],2);


            Console.WriteLine("\nPart 2:");
            Console.WriteLine("Oxygen: " + oxygen.ToString());
            Console.WriteLine("C02: " + co2.ToString());
            Console.WriteLine("Oxygen*CO2: " + (oxygen*co2).ToString());
        }

        static int argFalse(bool[] arr) {
            for (int i = 0; i<arr.Length; i++) {
                if (!arr[i]) {
                    return i;
                }
            }
            return 0;
        }

    }
}

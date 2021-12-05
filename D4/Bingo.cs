using System;

namespace D4
{
    class Bingo
    {
        int[][] numbers;
        bool[][] isMarked;
        int n,m;
        bool isEnabled;

        public Bingo(string[] lines) {
            numbers = new int[lines.Length][];
            for(int i = 0; i<lines.Length; i++) {
                string[] words = lines[i].Split(' ',StringSplitOptions.RemoveEmptyEntries);
                numbers[i] = new int[words.Length];
                for(int j = 0; j<words.Length;j++) {
                    numbers[i][j] = Int32.Parse(words[j]);
                }
            }
            n = numbers.Length;
            m = numbers[0].Length;
            isMarked = new bool[n][];
            for (int i = 0; i<n; i++) {
                isMarked[i] = new bool[m];
            }
            isEnabled = true;
        }

        public void Print() {
            for (int i = 0; i<n; i++) {
                for (int j = 0; j<m; j++) {
                    Console.Write(numbers[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public void PrintMarks() {
            for (int i = 0; i<n; i++) {
                for (int j = 0; j<m; j++) {
                    Console.Write(isMarked[i][j] ? "1" : "0");
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }


        public bool CheckVictory() {
            // Horizontal check
            for (int i = 0; i<n; i++) {
                bool aligned = true;
                for (int j = 0; j<m; j++) {
                    if (!isMarked[i][j]) {
                        aligned = false;
                    }
                }
                if (aligned) {
                    return true;
                }
            }

            // Vertical check
            for (int j = 0; j<m; j++) {
                bool aligned = true;
                for (int i = 0; i<n; i++) {
                    if (!isMarked[i][j]) {
                        aligned = false;
                    }
                }
                if (aligned) {
                    return true;
                }
            }
            return false;
        }

        public int Evaluate() {
            int sum = 0;
            for (int i = 0; i<n; i++) {
                for (int j = 0; j<m; j++) {
                    if (!isMarked[i][j]) {
                        sum += numbers[i][j];
                    }
                }
            }
            return sum;
        }

        public bool Play(int number) {
            if (!isEnabled) {
                return false;
            }
            for (int i = 0; i<n; i++) {
                for (int j = 0; j<m; j++) {
                    if (numbers[i][j] == number) {
                        isMarked[i][j] = true;
                        if (CheckVictory()) {
                            isEnabled = false;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace D21
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalSw = new Stopwatch();
            globalSw.Start();

            string path = @"input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            int start1 = int.Parse(lines[0].Split("Player 1 starting position: ", StringSplitOptions.RemoveEmptyEntries)[0]);
            int start2 = int.Parse(lines[1].Split("Player 2 starting position: ", StringSplitOptions.RemoveEmptyEntries)[0]);

            var sw = new Stopwatch();
            sw.Start();

            // Part 1
            int p1 = start1;
            int p2 = start2;

            int s1 = 0;
            int s2 = 0;

            int dice = 0;
            int rolls = 0;
            int winnerP1 = 0;
            int loserScore = 0;

            while (s1 < 1000 && s2 < 1000) {
                dice = RollDice(dice);
                p1 = Move(p1, dice);
                dice = RollDice(dice);
                p1 = Move(p1, dice);
                dice = RollDice(dice);
                p1 = Move(p1, dice);
                s1 += p1;
                rolls += 3;
                if (s1 >= 1000) {
                    winnerP1 = 1;
                    loserScore = s2;
                    break;
                }
                dice = RollDice(dice);
                p2 = Move(p2, dice);
                dice = RollDice(dice);
                p2 = Move(p2, dice);
                dice = RollDice(dice);
                p2 = Move(p2, dice);
                s2 += p2;
                rolls += 3;
                if (s2 >= 1000) {
                    winnerP1 = 2;
                    loserScore = s1;
                    break;
                }
            }

            sw.Stop();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Winner: Player {winnerP1}");
            Console.WriteLine($"Loser score: {loserScore}");
            Console.WriteLine($"Answer: {loserScore*rolls}");
            Console.WriteLine($"Calculations execution time: {sw.ElapsedMilliseconds} ms");



            // Part 2
            sw.Restart();

            // states[turn, score1, score2, pos1, pos2]
            long[,,,,] states = new long[2,21,21,10,10];

            states[0,0,0,start1-1,start2-1] = 1L;
            long scores1 = 0L;
            long scores2 = 0L;

            var diracRolls = new Dictionary<int,int>();
            diracRolls.Add(3,1);
            diracRolls.Add(4,3);
            diracRolls.Add(5,6);
            diracRolls.Add(6,7);
            diracRolls.Add(7,6);
            diracRolls.Add(8,3);
            diracRolls.Add(9,1);

            while(!CheckEmpty(states)) {
                states = PlayDirac(states, diracRolls, ref scores1, ref scores2);
            }

            int winnerP2 = (scores1 > scores2) ? 1 : 2;

            sw.Stop();

            Console.WriteLine("\nPart 2:");
            Console.WriteLine($"Winner: Player {winnerP2}");
            Console.WriteLine($"Player 1 score: {scores1}");
            Console.WriteLine($"Player 2 score: {scores2}");
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms");

            globalSw.Stop();
            Console.WriteLine($"\nTotal execution time: {globalSw.ElapsedMilliseconds} ms");
        }

        static int RollDice(int oldValue) {
            return oldValue % 100 + 1;
        }

        static bool CheckEmpty(long[,,,,] table) {
            for (int i = 0; i < table.GetLength(0); i++) {
                for (int j = 0; j < table.GetLength(1); j++) {
                    for (int k = 0; k < table.GetLength(2); k++) {
                        for (int l = 0; l < table.GetLength(3); l++) {
                            for (int m = 0; m < table.GetLength(4); m++) {
                                if (table[i,j,k,l,m] != 0L) {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        static long Count(long[,] table) {
            long count = 0;
            for (int i = 0; i < table.GetLength(0); i++) {
                for (int j = 0; j < table.GetLength(1); j++) {
                    count += table[i,j];
                }
            }
            return count;
        }

        static long[,,,,] PlayDirac(long[,,,,] states, Dictionary<int,int> rolls, ref long score1, ref long score2) {
            // states[turn, score1, score2, pos1, pos2]

            int winScore = states.GetLength(1);
            int nPositions = states.GetLength(3);
            long[,,,,] newStates = new long[2, winScore, winScore, nPositions, nPositions];

            for (int s1 = 0; s1 < winScore; s1++) {
                for (int s2 = 0; s2 < winScore; s2++) {
                    for (int p1 = 0; p1 < nPositions; p1++) {
                        for (int p2 = 0; p2 < nPositions; p2++) {
                            if (states[0,s1,s2,p1,p2] == 0 && states[1,s1,s2,p1,p2] == 0) {
                                continue;
                            }
                            foreach (KeyValuePair<int,int> elem in rolls) {
                                int newPos1 = (p1 + elem.Key) % 10;
                                int newPos2 = (p2 + elem.Key) % 10;
                                int newScore1 = s1 + newPos1 + 1;
                                int newScore2 = s2 + newPos2 + 1;
                                if (newScore1 >= 21) {
                                    score1 += elem.Value*states[0, s1, s2, p1, p2];
                                }
                                else {
                                    newStates[1, newScore1, s2, newPos1, p2] += elem.Value*states[0, s1, s2, p1, p2];
                                }
                                if (newScore2 >= 21) {
                                    score2 += elem.Value*states[1, s1, s2, p1, p2];
                                }
                                else {
                                    newStates[0, s1, newScore2, p1, newPos2] += elem.Value*states[1, s1, s2, p1, p2];
                                }
                            }
                        }
                    }
                }
            }
            return newStates;
        }

        static int Move(int oldPos, int increment) {
            return (oldPos + increment - 1) % 10 + 1;
        }
    }
}

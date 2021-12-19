using System;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;

namespace D19
{
    class Solver
    {
        // Position of scanners and beacons
        HashSet<Vector3> scanners;
        HashSet<Vector3> beacons;

        Scanner [] scannerArray;

        private Solver() {
            scanners = new HashSet<Vector3>();
            beacons = new HashSet<Vector3>();
        }

        public Solver(Scanner[] newScannerArray) : this() {
            scannerArray = newScannerArray;
            AddBeacons(scannerArray[0].GetBeacons());
        }

        public void AddBeacons(HashSet<Vector3> newBeacons) {
            foreach (Vector3 beacon in newBeacons) {
                beacons.Add(beacon);
            }
        }

        public void AddScanner(Vector3 scanner) {
            scanners.Add(scanner);
        }

        /// <summary>
        /// Function used in main Solve function, will iterate over unsolved scanners
        /// and correct their orientation and translation, then add the scanner and beacon
        /// positions to the scanners and beacons fields.
        /// Uses memoization to avoid iterating again over invalid orientations and
        /// translations.
        /// </summary>
        public void SolveStep(bool[] solved, bool[,] skip) {
            int n = scannerArray.Length;
            HashSet<Vector3> solvedBeacons = new HashSet<Vector3>();
            Vector3 scannerPosition = new Vector3();

            for (int i = 0; i < n; i++) { // Unsolved scanners
                if (solved[i]) { // Only check unsolved
                    continue;
                }
                for (int j = 0; j < n; j++) { // Solved scanners
                    if (!solved[j] || skip[i,j]) { // Only check solved, don't check incompatible combinations
                        continue;
                    }

                    // Either we will find a suitable rotation and translation for this combination or we won't.
                    // If we don't, we don't want to check that combination of scanners again.
                    skip[i,j] = true;

                    for (int k = 0; k < 24; k++) { // Rotations
                        

                        // It is faster to compute the absolute distances of the beacons in both scanners
                        // than to find a valid translation
                        // It is useful to rule out scanners that don't overlap
                        if (scannerArray[i].GetTransform(k).CountSimilarities(scannerArray[j]) < 66) {
                            continue;
                        }

                        HashSet<Vector3> unsolvedBeacons = scannerArray[i].GetTransform(k).GetBeacons();
                        HashSet<Vector3> beaconsToMatch = scannerArray[j].GetBeacons();

                        // Translations
                        foreach (Vector3 b1 in beaconsToMatch) {
                            foreach (Vector3 b2 in unsolvedBeacons) {
                                Vector3 delta = b1-b2;
                                var translatedBeacons = new HashSet<Vector3>(unsolvedBeacons.Select(x => x+delta));
                                
                                // Check if overlap
                                if (CountSimilarities(beaconsToMatch, translatedBeacons) >= 12) {
                                    scannerPosition = delta;
                                    solvedBeacons = new HashSet<Vector3>(translatedBeacons);

                                    // Replace scanner with correct
                                    scannerArray[i] = new Scanner(solvedBeacons);

                                    solved[i] = true; // Mark as solved

                                    AddBeacons(solvedBeacons);
                                    AddScanner(scannerPosition);
                                    return;
                                }
                            }
                        }
                    }
                }
            }

        }



        /// <summary>
        /// Main solve function, calls SolveStep until it corrects all the scanners' correct
        /// rotations and translations.
        /// Uses memoization to avoid iterating again over invalid orientations and
        /// translations.
        /// </summary>
        public void Solve() {
            int n = scannerArray.Length;

            bool[] solved = new bool[n]; // Skip solved scanners
            bool[,] skip = new bool[n,n]; // Skip incompatible scanner combinations
            
            // Scanner 0 is defined as origin, and is solved by definition
            solved[0] = true;

            // There are n-1 unsolved scanners
            for (int i = 0; i < n-1; i++) {
                SolveStep(solved,skip);
            }
        }

        public int CountSimilarities(HashSet<Vector3> b1, HashSet<Vector3> b2) {
            int count = 0;
            foreach(var elem in b2) {
                if (b1.Contains(elem)) {
                    count++;
                }
            }
            return count;
        }

        public int GetMaxManhattanDistance() {
            int max = 0;
            foreach (Vector3 s1 in scanners) {
                foreach (Vector3 s2 in scanners) {
                    Vector3 abs = Vector3.Abs(s1-s2);
                    int manhattanDistance = (int)Vector3.Dot(abs, Vector3.One);
                    if (manhattanDistance > max) {
                        max = manhattanDistance;
                    }
                }
            }
            return max;
        }

        public int GetBeaconCount() {
            return beacons.Count;
        }

    }
}
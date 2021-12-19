using System;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;

namespace D19
{
    class Solver
    {
        HashSet<Vector3> scanners;
        HashSet<Vector3> beacons;

        List<Scanner> solvedScannerList;
        List<Scanner> unsolvedScannerList;

        public Solver() {
            scanners = new HashSet<Vector3>();
            scanners.Add(new Vector3(0f,0f,0f));
            beacons = new HashSet<Vector3>();
            solvedScannerList = new List<Scanner>();
            unsolvedScannerList = new List<Scanner>();
        }

        public Solver(Scanner origin) : this() {
            foreach (Vector3 beacon in origin.GetBeacons()) {
                beacons.Add(beacon);
            }
            solvedScannerList.Add(origin);
        }

        public Solver(Scanner origin, List<Scanner> otherScanners) : this(origin) {
            unsolvedScannerList = otherScanners;
        }

        public void AddScanner(Scanner sc) {
            unsolvedScannerList.Add(sc);
        }

        public List<Scanner> Transform(int transform) {
            var transformList = new List<Scanner>();
            foreach (Scanner sc in transformList) {
                transformList.Add(sc.GetTransform(transform));
            }
            return transformList;
        }

        public void AddBeacons(HashSet<Vector3> newBeacons) {
            foreach (Vector3 beacon in newBeacons) {
                beacons.Add(beacon);
            }
        }

        public void AddScanner(Vector3 scanner) {
            scanners.Add(scanner);
        }

        public void Solve() {
            int n = unsolvedScannerList.Count;
            bool[] solved = new bool[n];
            bool[,] skip = new bool[n+1,n];
            
            int iter = 0;
            while (iter < n) {
                bool stop = false;


                int unsolvedScannerIndex = 0;
                int solvedScannerIndex = 0;
                int scannerRotation = 0;

                HashSet<Vector3> solvedBeacons = new HashSet<Vector3>();
                Vector3 scannerPosition = new Vector3();

                for (int i = 0; i < unsolvedScannerList.Count; i++) {
                    if (solved[i]) {
                        continue;
                    }
                    for (int k = 0; k < solvedScannerList.Count; k++) {
                        if (skip[i,k]) {
                            continue;
                        }
                        for (int j = 0; j < 24; j++) {
                            
                            if (unsolvedScannerList[i].GetTransform(j).CountSimilarities(solvedScannerList[k]) < 66) {
                                skip[i,k] = true;
                                continue;
                            }
                            HashSet<Vector3> unsolvedBeacons = unsolvedScannerList[i].GetTransform(j).GetBeacons();
                            HashSet<Vector3> beaconsToMatch = solvedScannerList[k].GetBeacons();


                            foreach (Vector3 b1 in beaconsToMatch) {
                                foreach (Vector3 b2 in unsolvedBeacons) {
                                    Vector3 delta = b1-b2;
                                    var translatedBeacons = new HashSet<Vector3>(unsolvedBeacons.Select(x => x+delta));
                                    if (CountSimilarities(beaconsToMatch, translatedBeacons) >= 12) {
                                        stop = true;
                                        scannerPosition = delta;
                                        solvedBeacons = new HashSet<Vector3>(translatedBeacons);
                                        unsolvedScannerIndex = i;
                                        scannerRotation = j;
                                        solvedScannerIndex = k;
                                        solved[i] = true;
                                        break;
                                    }
                                }
                                if (stop) {
                                    break;
                                }
                            }
                            if (stop) {
                                break;
                            }
                        }
                        if (stop) {
                            break;
                        }
                    }
                    if (stop) {
                        break;
                    }
                }

                solvedScannerList.Add(new Scanner(solvedBeacons));
                AddBeacons(solvedBeacons);
                AddScanner(scannerPosition);

                // unsolvedScannerList.RemoveAt(unsolvedScannerIndex);
                iter++;

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
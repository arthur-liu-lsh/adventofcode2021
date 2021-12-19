using System;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;

namespace D19
{
    class Scanner
    {
        private HashSet<Vector3> beacons;
        private HashSet<Vector3> distances;

        public Scanner(string[] lines) {
            beacons = new HashSet<Vector3>();
            foreach(string line in lines) {
                string[] words = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int[] coordinates = words.Select(int.Parse).ToArray();
                beacons.Add(new Vector3(coordinates[0], coordinates[1], coordinates[2]));
            }
            CalculateDistances();
        }

        public Scanner(HashSet<Vector3> newBeacons) {
            beacons = newBeacons;
            CalculateDistances();
        }

        public HashSet<Vector3> GetBeacons() {
            return beacons;
        }

        public void CalculateDistances() {
            distances = new HashSet<Vector3>();
            foreach(var v1 in beacons) {
                foreach (var v2 in beacons) {
                    if (v1 != v2) {
                        distances.Add(Vector3.Abs(v1-v2));
                    }
                }
            }
        }

        public Scanner[] GetTransforms() {
            Scanner[] scanners = new Scanner[24];
            for (int i = 0; i < 24; i++) {
                scanners[i] = new Scanner(Transform(i));
            }
            return scanners;
        }

        public Scanner GetTransform(int transform) {
            return new Scanner(Transform(transform));
        }

        HashSet<Vector3> Transform(int transform) {
            var newSet = new HashSet<Vector3>();

            foreach (var elem in beacons) {
                float x = elem.X;
                float y = elem.Y;
                float z = elem.Z;
                switch (transform) {
                    case 0:
                        newSet.Add(new Vector3(x, y, z));
                        break;
                    case 1:
                        newSet.Add(new Vector3(x, z, -y));
                        break;
                    case 2:
                        newSet.Add(new Vector3(x, -y, -z));
                        break;
                    case 3:
                        newSet.Add(new Vector3(x, -z, y));
                        break;
                    case 4:
                        newSet.Add(new Vector3(-x, y, -z));
                        break;
                    case 5:
                        newSet.Add(new Vector3(-x, z, y));
                        break;
                    case 6:
                        newSet.Add(new Vector3(-x, -y, z));
                        break;
                    case 7:
                        newSet.Add(new Vector3(-x, -z, -y));
                        break;
                    case 8:
                        newSet.Add(new Vector3(y, z, x));
                        break;
                    case 9:
                        newSet.Add(new Vector3(y, -x, z));
                        break;
                    case 10:
                        newSet.Add(new Vector3(y, -z, -x));
                        break;
                    case 11:
                        newSet.Add(new Vector3(y, x, -z));
                        break;
                    case 12:
                        newSet.Add(new Vector3(-y, z, -x));
                        break;
                    case 13:
                        newSet.Add(new Vector3(-y, -x, -z));
                        break;
                    case 14:
                        newSet.Add(new Vector3(-y, -z, x));
                        break;
                    case 15:
                        newSet.Add(new Vector3(-y, x, z));
                        break;
                    case 16:
                        newSet.Add(new Vector3(z, x, y));
                        break;
                    case 17:
                        newSet.Add(new Vector3(z, y, -x));
                        break;
                    case 18:
                        newSet.Add(new Vector3(z, -x, -y));
                        break;
                    case 19:
                        newSet.Add(new Vector3(z, -y, x));
                        break;
                    case 20:
                        newSet.Add(new Vector3(-z, x, -y));
                        break;
                    case 21:
                        newSet.Add(new Vector3(-z, y, x));
                        break;
                    case 22:
                        newSet.Add(new Vector3(-z, -x, y));
                        break;
                    case 23:
                        newSet.Add(new Vector3(-z, -y, -x));
                        break;
                    default:
                        break;
                }
            }
            return newSet;
        }

        public int CountSimilarities(Scanner o) {
            int count = 0;
            foreach(var elem in distances) {
                if (o.distances.Contains(elem)) {
                    count++;
                }
            }
            return count;
        }

        public HashSet<Vector3> GetDistances() {
            return distances;
        }

        public void Print() {
            foreach (Vector3 coordinate in beacons) {
                Console.Write($"{coordinate} ");
            }
        }
    }
}

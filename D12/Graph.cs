using System;
using System.Collections.Generic;
using System.Linq;

namespace D12
{
    class Graph
    {
        Dictionary<string, List<string>> vertices;
        
        public Graph() {
            vertices = new Dictionary<string, List<string>>();
        }

        public void AddPath(string src, string dest) {
            if (!vertices.ContainsKey(src)) {
                AddVertex(src);
            }
            if (!vertices.ContainsKey(dest)) {
                AddVertex(dest);
            }
            vertices[src].Add(dest);
            vertices[dest].Add(src);
        }

        void AddVertex(string name) {
            vertices.Add(name, new List<string>());
        }

        public void Print() {
            Console.WriteLine(string.Join("\n", vertices.Select(kvp => $"{kvp.Key}: {string.Join(",", kvp.Value)}")));
        }

        public int CountPaths(string src, string dest, HashSet<string> seen) {
            if (src == dest) {
                return 1;
            }
            else {
                int sum = 0;
                foreach (string neighbour in vertices[src].Where(vertex => !seen.Contains(vertex))) {
                    var newSeen = new HashSet<string>(seen);
                    if (neighbour.All(char.IsLower)) {
                        newSeen.Add(neighbour);
                    }
                    sum += CountPaths(neighbour,dest,newSeen);
                }
                return sum;
            }
        }

    }
}
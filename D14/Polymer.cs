using System;
using System.Collections.Generic;

namespace D14
{
    class Polymer {
        Dictionary<string, ulong> pairs = new Dictionary<string, ulong>();
        Dictionary<char, ulong> counts = new Dictionary<char, ulong>();
        Dictionary<string, Tuple<string,string,char>> rules = new Dictionary<string, Tuple<string,string,char>>();

        public Dictionary<char, ulong> GetCounts() {
            return counts;
        }

        public void AddRule(string[] rule) {
            string pair1 = rule[0][0] + rule[1];
            string pair2 = rule[1] + rule[0][1];
            InitPair(pair1);
            InitPair(pair2);
            InitCount(rule[1][0]);
            rules.Add(rule[0],Tuple.Create<string,string,char>(pair1,pair2,rule[1][0]));
        }

        void InitCount(char key) {
            if (!counts.ContainsKey(key)) {
                counts.Add(key,0ul);
            }
        }

        void InitPair(string key) {
            if (!pairs.ContainsKey(key)) {
                pairs.Add(key,0ul);
            }
        }

        public void IncrementCount(char key, ulong value) {
            if (!counts.ContainsKey(key)) {
                counts.Add(key,0ul);
            }
            counts[key] += value;
        }

        public void IncrementPair(string key, ulong value) {
            if (!pairs.ContainsKey(key)) {
                pairs.Add(key,0ul);
            }
            pairs[key] += value;
        }

        public void ErasePairs() {
            foreach (KeyValuePair<string, ulong> entry in pairs) {
                pairs[entry.Key] = 0ul;
            }
        }

        public void AddInitialSequence(string initialSequence) {
            foreach (char c in initialSequence) {
                IncrementCount(c,1ul);
            }
            for (int i = 0; i < initialSequence.Length-1; i++) {
                IncrementPair(initialSequence.Substring(i,2),1ul);
            }
        }

        public Polymer(string initialSequence) {
            AddInitialSequence(initialSequence);
        }

        public void Print() {
            foreach(KeyValuePair<char, ulong> entry in counts) {
                Console.Write($"{entry.Key}:{entry.Value} ");
            }
            Console.WriteLine();
        }

        public void Iterate() {
            var oldPairs = new Dictionary<string,ulong>(pairs);
            var oldCounts = new Dictionary<char, ulong>(counts);
            ErasePairs();
            foreach (KeyValuePair<string, ulong> entry in oldPairs) {
                IncrementPair(rules[entry.Key].Item1, oldPairs[entry.Key]);
                IncrementPair(rules[entry.Key].Item2, oldPairs[entry.Key]);
                IncrementCount(rules[entry.Key].Item3, oldPairs[entry.Key]);
            }
        }
    }
}
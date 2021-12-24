using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace D24
{
    class ALU
    {
        List<int> input;
        
        Dictionary<string,int> vars;

        public ALU() {
            input = new List<int>();
            vars = new Dictionary<string, int>();
            vars.Add("w",0);
            vars.Add("x",0);
            vars.Add("y",0);
            vars.Add("z",0);
        }

        public ALU(List<int> newInput) : this() {
            input = newInput;
        }

        void ReadInstruction(string[] parameters) {
            switch (parameters[0]) {
                case "inp":
                    break;
                case "add":
                    break;
                case "mul":
                    break;
                case "div":
                    break;
                case "mod":
                    break;
                case "eql":
                    break;
                default:
                    break;
            }
        }

        void ReadInstructions(string[][] instructions) {
            foreach (string[] parameters in instructions) {
                ReadInstruction(parameters);
            }
        }

        void Reset() {
            foreach (string key in vars.Keys) {
                vars[key] = 0;
            }
            input = new List<int>();
        }

        void Reset(List<int> newInput) {
            Reset();
            input = newInput;
        }

        void Inp(string[] parameters) {
            vars[parameters[1]] = input[input.Count-1];
            input.RemoveAt(input.Count - 1);
        }

        void Add(string[] parameters) {
            vars[parameters[1]] = vars[parameters[1]] + vars[parameters[2]];
        }

        void Mul(string[] parameters) {
            vars[parameters[1]] = vars[parameters[1]] * vars[parameters[2]];
        }

        void Div(string[] parameters) {
            vars[parameters[1]] = vars[parameters[1]] / vars[parameters[2]];
        }

        void Mod(string[] parameters) {
            vars[parameters[1]] = vars[parameters[1]] % vars[parameters[2]];
        }
        
        void Eql(string[] parameters) {
            vars[parameters[1]] = (vars[parameters[1]] == vars[parameters[2]]) ? 1 : 0;
        }
    }
}
using System;

namespace D8
{
    class Digit
    {

        private bool[] segments = new bool[7];

        public Digit(string code) {
            Decode(code);
        }

        public Digit(bool[] newSegments) {
            Array.Copy(newSegments, segments, 7);
        }

        void Decode(string code) {
            foreach(char c in code) {
                switch(c) {
                    case 'a':
                        segments[0] = true;
                        break;
                    case 'b':
                        segments[1] = true;
                        break;
                    case 'c':
                        segments[2] = true;
                        break;
                    case 'd':
                        segments[3] = true;
                        break;
                    case 'e':
                        segments[4] = true;
                        break;
                    case 'f':
                        segments[5] = true;
                        break;
                    case 'g':
                        segments[6] = true;
                        break;
                    default:
                        break;
                }
            }
        }

        public int Count() {
            int count = 0;
            foreach (bool segment in segments) {
                if (segment) {
                    count++;
                }
            }
            return count;
        }

        public override bool Equals(object o) {
            for(int i = 0; i < 7; i++) {
                if (segments[i] != ((Digit)o).segments[i]) {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()  
            {  
                return this.ToString().GetHashCode();  
            } 

        public static bool operator ==(Digit a, Digit b) {
            for(int i = 0; i < 7; i++) {
                if (a.segments[i] != b.segments[i]) {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(Digit a, Digit b) {
            return !(a==b);
        }

        public static Digit operator |(Digit a, Digit b) {
            bool[] newSegments = new bool[7];
            for (int i = 0; i < 7; i++) {
                newSegments[i] = a.segments[i] || b.segments[i];
            }
            return new Digit(newSegments);
        }

    }
}
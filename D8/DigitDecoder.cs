using System;

namespace D8
{
    class DigitDecoder
    {
        Digit[] digits = new Digit[10];

        public DigitDecoder(Digit[] newDigits) {
            Array.Copy(newDigits,digits,10);
        }

        public void Solve() {
            Digit[] newDigits = new Digit[10];

            // Find 1, 4, 7, and 8
            for (int i = 0; i < 10; i++) {
                switch(digits[i].Count()) {
                    case 2:
                        newDigits[1] = digits[i];
                        break;
                    case 3:
                        newDigits[7] = digits[i];
                        break;
                    case 4:
                        newDigits[4] = digits[i];
                        break;
                    case 7:
                        newDigits[8] = digits[i];
                        break;
                    default:
                        break;
                }
            }

            // Find 0, 6, and 9
            for (int i = 0; i < 10; i++) {
                if (digits[i].Count() == 6) {
                    if ((digits[i] | newDigits[1]) != digits[i]) {
                        newDigits[6] = digits[i];
                    }
                    else if ((digits[i] | newDigits[4]) != digits[i]) {
                        newDigits[0] = digits[i];
                    }
                    else {
                        newDigits[9] = digits[i];
                    }
                }
            }

            // Find 2, 3, and 5
            for (int i = 0; i < 10; i++) {
                if (digits[i].Count() == 5) {
                    if ((digits[i] | newDigits[7]) == digits[i]) {
                        newDigits[3] = digits[i];
                    }
                    else if ((digits[i] | newDigits[9]) != newDigits[9]) {
                        newDigits[2] = digits[i];
                    }
                    else {
                        newDigits[5] = digits[i];
                    }
                }
            }
            digits = newDigits;

        }

        public int Decode(string code) {
            Digit digit = new Digit(code);
            for (int i = 0; i < 10; i++) {
                if (digit == digits[i]) {
                    return i;
                }
            }
            return -1;
        }

        public string DecodeToString(string code) {
            Digit digit = new Digit(code);
            for (int i = 0; i < 10; i++) {
                if (digit == digits[i]) {
                    return i.ToString();
                }
            }
            return "x";
        }

    }
}
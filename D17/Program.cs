using System;
using System.Diagnostics;

namespace D17
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalSw = new Stopwatch();
            globalSw.Start();
            (int x1, int x2, int y1, int y2) target = (150, 171, -129, -70);
            // (int x1, int x2, int y1, int y2) target = (20, 30, -10, -5);

            var sw = new Stopwatch();

            // Part 1 and 2
            sw.Start();
            
            int count = 0;
            int maxY = 0;
            for (int i = -500; i < 500; i++) {
                for(int j = 0; j < 500; j++) {

                (bool reached, int maxY) res = Test(j,i, target);
                if (res.reached) {
                    count++;
                    if (res.maxY > maxY) {
                        maxY = res.maxY;
                    }
                }
                }
            }
            
            globalSw.Stop();

            Console.WriteLine("Part 1 and 2:");
            Console.WriteLine($"Max y position: {maxY}");
            Console.WriteLine($"Possible velocity values: {count}");
            Console.WriteLine($"Total execution time: {globalSw.ElapsedMilliseconds} ms");
        }

        static (bool reached, int maxY) Test(int vx, int vy, (int x1, int x2, int y1, int y2) target) {
            bool stop = false;
            int x = 0;
            int y = 0;
            bool reached = false;

            int maxY = 0;

            while (!stop) {
                var res = NextStep(x,y,vx,vy);
                x = res.x;
                y = res.y;
                vx = res.vx;
                vy = res.vy;
                if (y > maxY) {
                    maxY = y;
                }
                if (target.y1 <= y && y <= target.y2 && target.x1 <= x && x <= target.x2) {
                    reached = true;
                }
                if (y < target.y1 || x > target.x2) {
                    stop = true;
                }
            }

            return (reached,maxY);
        }

        static (int x, int y, int vx, int vy) NextStep(int x, int y, int vx, int vy) {
            x += vx;
            y += vy;

            if (vx > 0) {
                vx -= 1;
            }
            else if (vx < 0) {
                vx += 1;
            }

            vy -= 1;

            return (x,y,vx,vy);
        }
    }
}

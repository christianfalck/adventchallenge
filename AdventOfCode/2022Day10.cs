using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day10_2022
    {
        readonly string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2022day10.txt").ToArray();

        public void calculate()
        {
            int valueToAdd = 0;
            bool calculating = false;
            int cycle = 0;
            int X = 1;
            int row = 0;
            int answerPart1 = 0;
            while (cycle < 240)
            {
                // Calculation of the positions
                cycle++;
                if (calculating)
                {
                    calculating = false; // one cycle to start, one cycle to stop
                }
                else
                {
                    // Add the value after calculation is done
                    X += valueToAdd;
                    if (lines[row].StartsWith("noop"))
                    {
                        valueToAdd = 0;
                    }
                    else if (lines[row].StartsWith("addx"))
                    {
                        calculating = true;
                        valueToAdd = int.Parse(lines[row][lines[row].IndexOf(" ")..]);
                    }
                    row++;
                }

                // Part 1: Calculating for cycle 20, 60, 100, 140, 180 and 220
                if ((cycle + 20) % 40 == 0)
                {
                    // cycle 20, 60, 100, 140 etc.
                    answerPart1 += cycle * X;
                }

                // Part 2: Printing each pixel, new line after 40 pixels
                if ((cycle - 1) % 40 == X - 1 || (cycle - 1) % 40 == X || (cycle - 1) % 40 == X + 1)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
                if (cycle % 40 == 0)
                {
                    Console.WriteLine();
                }
                
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Answer: " + answerPart1);

        }
    }
}

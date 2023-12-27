using System;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day15_2023
    {
        public static void calculate()
        {
            BigInteger answer = 0;
            BigInteger answer2 = 0;
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day15.txt").ToArray();

            string[] commands = lines[0].Split(',');

            foreach (string command in commands)
            {
                int currentValue = 0;
                foreach (char c in command)
                {
                    currentValue += c;
                    currentValue *= 17;
                    currentValue %= 256;
                }
                answer += currentValue;
            }
            System.Console.WriteLine("Answer part 1: " + answer + " and part 2: " + answer2);
        }
    }
}
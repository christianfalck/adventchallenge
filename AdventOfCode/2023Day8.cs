using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day8_2023
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day8.txt").ToArray();
            string navigation = lines[0];

            // Removing the first two rows
            string[] mapRows = new string[lines.Length - 2];
            Array.Copy(lines, 2, mapRows, 0, mapRows.Length);

            // Storing each step as a dictionary entry
            Dictionary<string, (string, string)> steps = new Dictionary<string, (string, string)>();

            //Storing where we are right now
            string here = "";

            int answer1 = 0;

            foreach (string line in mapRows)
            {
                int equalsIndex = line.IndexOf('=');
                here = line.Substring(0, equalsIndex).Trim();

                // Fetching the text between the parenthesises
                string tupleValues = line.Substring(line.IndexOf('(') + 1, line.IndexOf(')') - line.IndexOf('(') - 1).Trim();

                string[] stringsInTuple = tupleValues.Split(',');
                steps[here] = (stringsInTuple[0].Trim(), stringsInTuple[1].Trim());
            }
            // Let's start at the start
            here = "AAA";
            while (here != "ZZZ")
            {
                // take next step
                if (navigation[answer1 % navigation.Length] == 'L')
                {
                    here = steps[here].Item1;
                }
                else
                {
                    here = steps[here].Item2;
                }
                answer1++;
            }

            System.Console.WriteLine("Answer part 1: " + answer1);

            //Part 2
            BigInteger answer2 = 0;

            // THere are 6 nodes that ends with A and 6 that ends with Z
            // Todo: programmatically find the ones ending with A instead
            string[] heres = { "JSA", "AAA", "RLA", "QLA", "QFA", "RXA" };
            int[] steps2 = { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i <= 5; i++)
            {
                while (heres[i][2] != 'Z')
                {
                    // take next step
                    if (navigation[steps2[i] % navigation.Length] == 'L')
                    {
                        heres[i] = steps[heres[i]].Item1;
                    }
                    else
                    {
                        heres[i] = steps[heres[i]].Item2;
                    }
                    steps2[i]++;
                }
            }

            answer2 = LCM(steps2);

            //while (!(heres[0][2] == 'Z' && heres[1][2] == 'Z' && heres[2][2] == 'Z' && heres[3][2] == 'Z' && heres[4][2] == 'Z' && heres[5][2] == 'Z'))
            //{
            //    // take next step
            //    if (navigation[answer2 % navigation.Length] == 'L')
            //    {
            //        heres[0] = steps[heres[0]].Item1;
            //        heres[1] = steps[heres[1]].Item1;
            //        heres[2] = steps[heres[2]].Item1;
            //        heres[3] = steps[heres[3]].Item1;
            //        heres[4] = steps[heres[4]].Item1;
            //        heres[5] = steps[heres[5]].Item1;
            //    }
            //    else
            //    {
            //        heres[0] = steps[heres[0]].Item2;
            //        heres[1] = steps[heres[1]].Item2;
            //        heres[2] = steps[heres[2]].Item2;
            //        heres[3] = steps[heres[3]].Item2;
            //        heres[4] = steps[heres[4]].Item2;
            //        heres[5] = steps[heres[5]].Item2;
            //    }
            //    answer2++;
            //}

            System.Console.WriteLine("Answer part 2: " + answer2);
        }

        static BigInteger LCM(params int[] numbers)
        {
            BigInteger lcm = 1;

            foreach (int number in numbers)
            {
                lcm = LeastCommonMultiple(lcm, number);
            }

            return lcm;
        }

        static BigInteger LeastCommonMultiple(BigInteger a, BigInteger b)
        {
            return BigInteger.Abs(a * b) / BigInteger.GreatestCommonDivisor(a, b);
        }
    }
    class twoDestinations
    {
        string R { get; set; }
        string L { get; set; }
    }
}

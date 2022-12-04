using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day4_2022
    {
        public static void calculate()
        {
            // In how many assignment pairs does one range fully contain the other?
            int numberOfOverlapsPart1 = 0;

            //In how many assignment pairs do the ranges overlap?
            int numberOfOverlapsPart2 = 0;

            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2022day4.txt"))
            {
                // Extract the numbers
                string firstPair = line.Substring(0, line.IndexOf(','));
                int firstPairStart = int.Parse(firstPair.Substring(0, firstPair.IndexOf('-')));
                int firstPairEnd = int.Parse(firstPair.Substring(firstPair.IndexOf('-') + 1));

                string secondPair = line.Substring(line.IndexOf(',') + 1);
                int secondPairStart = int.Parse(secondPair.Substring(0, secondPair.IndexOf('-')));
                int secondPairEnd = int.Parse(secondPair.Substring(secondPair.IndexOf('-') + 1));

                // Part 1
                if (firstPairStart <= secondPairStart && secondPairEnd <= firstPairEnd)
                {
                    //.2345678.  2-8
                    //..34567..  3-7
                    numberOfOverlapsPart1++;
                }
                else if (firstPairStart >= secondPairStart && secondPairEnd >= firstPairEnd)
                {
                    //...45678.  3-7
                    //.2345678.  2-8
                    numberOfOverlapsPart1++;
                }

                // Part 2
                if (firstPairEnd < secondPairStart || firstPairStart > secondPairEnd)
                {
                    // no overlap
                }
                else
                {
                    numberOfOverlapsPart2++;
                }
            }

            System.Console.WriteLine("Answer: " + numberOfOverlapsPart1 + " and: " + numberOfOverlapsPart2);

        }
    }
}

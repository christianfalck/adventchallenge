using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day6_2023
    {
        public static void calculate()
        {
            int answer = 1, answer2 = 0;
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day6.txt").ToArray();

            // Parse the input
            MatchCollection timeMatches = Regex.Matches(lines[0], @"\d+");
            List<int> times = new List<int>();
            foreach (Match timeMatch in timeMatches)
                times.Add(Int32.Parse(timeMatch.Value));
            MatchCollection distanceMatches = Regex.Matches(lines[1], @"\d+");
            List<int> distance = new List<int>();
            foreach (Match distanceMatch in distanceMatches)
                distance.Add(Int32.Parse(distanceMatch.Value));

            int tmp = 0;
            for (int k = 0; k < times.Count; k++)
            {
                for (int i = 0; i <= times[k]; i++)
                {
                    // i = speed, times[k] - i = moving time
                    int thisDistance = i * (times[k] - i);
                    if (thisDistance > distance[k])
                    {
                        // winning one more time
                        tmp++;
                    }
                }
                answer *= tmp;
                tmp = 0;
            }

            //Parse the input for part 2
            string timePart2 = "";
            foreach (Match timeMatch in timeMatches)
                timePart2 += timeMatch.Value;
            string distancePart2 = "";
            foreach (Match distanceMatch in distanceMatches)
                distancePart2 += distanceMatch.Value;

            BigInteger timePart2Value = BigInteger.Parse(timePart2);
            BigInteger distancePart2Value = BigInteger.Parse(distancePart2);

            for (int i = 0; i <= timePart2Value; i++)
            {
                // same calculation as part 1
                BigInteger thisDistance = i * (timePart2Value - i);
                if (thisDistance > distancePart2Value)
                {
                    // winning one more time
                    answer2++;
                }
            }

            System.Console.WriteLine("Answer part 1: " + answer + " and part 2: " + answer2);
        }
    }
}
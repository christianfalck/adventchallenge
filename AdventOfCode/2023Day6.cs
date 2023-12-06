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

            System.Console.WriteLine("Note the time it takes to calculate each part");

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
            System.Console.WriteLine("Answer part 1: " + answer);
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
            System.Console.WriteLine("Answer part 2: " + answer2);
            // this can be optimized by simply finding the first winning time,
            // and then remove the numbers before * 2 since the calculation is symmetrical. 
            // Let's take the first number as example: 7 seconds, 9 distance
            // 1 * 6 = 6
            // 2 * 5 = 10
            // 3 * 4 = 12
            // 4 * 3 = 12
            // 5 * 2 = 10
            // 6 * 1 = 6
            // We win already in the second race, then we just take 6 - 1*2 = 4.
            // We used 6 since 7-1=6, 7 seconds but if we hold 7 seconds we have 0 time
            int t = 0;
            while(t * (timePart2Value - t) < distancePart2Value)
            {
                t++;
            }
            // Answer = time - 1 - (winning race -1) * 2
            BigInteger bonusAnswer2 = timePart2Value - 1 - (t-1) * 2;

            System.Console.WriteLine("Answer part 2 again to show the optimization: " + bonusAnswer2);
        }
    }
}
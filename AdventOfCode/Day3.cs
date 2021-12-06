using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day3
    {
        public static void calculate()
        {

            //Part 1
            int numberOfLines = 0;
            int[] number = new int[12];
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/day3.txt"))
            {
                numberOfLines++;
                var intArray = line.Select(c => c - '0').ToArray();
                int counter = 0;
                foreach(int i in intArray)
                {
                    number[counter] += i;
                    counter++;
                }
            }
            int gamma = 0;
            int epsilon = 0;
            int multiplier = 1;
            for (int i = number.Length-1; i>=0; i--)
            {
                if(number[i] > numberOfLines - number[i])
                {
                    gamma += multiplier; 
                }
                else
                {
                    epsilon += multiplier;
                }
                multiplier *= 2;
            }

            //Part 2
            //I now know that there are 1000 lines with 12 digits each
            List<int[]> allNumbers = new List<int[]>();
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/day3.txt"))
            {
                allNumbers.Add(line.Select(c => c - '0').ToArray());
            }
            List<int[]> gammaPart2 = new List<int[]>(allNumbers);
            List<int[]> epsilonPart2 = new List<int[]>(allNumbers);
            for (int i = 0; i < 12; i++)
            {
                //gamma
                int numberOfOnes = 0;
                int totalnumber = 0;
                foreach (int[] row in gammaPart2)
                {
                    totalnumber++;
                    numberOfOnes += row[i];
                }
                if(gammaPart2.Count>1)
                {
                    bool keepOnes = numberOfOnes >= totalnumber - numberOfOnes; // if equal, keep
                    if (keepOnes)
                        gammaPart2.RemoveAll(x => x[i] == 0);
                    else
                        gammaPart2.RemoveAll(x => x[i] == 1);
                }
                //epsilon
                numberOfOnes = 0;
                totalnumber = 0;
                foreach (int[] row in epsilonPart2)
                {
                    totalnumber++;
                    numberOfOnes += row[i];
                }
                if (epsilonPart2.Count > 1)
                {
                    bool keepOnes = numberOfOnes < totalnumber - numberOfOnes; // if equal, don't keep
                    if (keepOnes)
                        epsilonPart2.RemoveAll(x => x[i] == 0);
                    else
                        epsilonPart2.RemoveAll(x => x[i] == 1);
                }
            }
            int part2gamma = 0;
            int part2epsilon = 0;
            multiplier = 1;
            for (int i = 11; i >= 0; i--)
            {
                part2gamma += multiplier * gammaPart2[0][i];
                part2epsilon += multiplier * epsilonPart2[0][i];
                multiplier *= 2;
            }

            System.Console.WriteLine("Answer: " + (gamma*epsilon) + ", and " + (part2gamma*part2epsilon));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day11_2021
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2021day11.txt").ToArray();
            int[][] octopuses = new int[lines.Length][];
            int answerPart1 = 0;
            int answerPart2 = 0;
            for (int i = 0; i <= lines.Length - 1; i++)
            {
                octopuses[i] = lines[i].Select(c => c - '0').ToArray();
            }

            int numberOfIterations = 10000;
            int flashes = 0;
            for (int iteration = 0; iteration < numberOfIterations; iteration++)
            {
                // Add a value to all octopuses
                for (int i = 0; i <= octopuses.Length - 1; i++)
                {
                    for (int j = 0; j <= octopuses[0].Length - 1; j++)
                    {
                        octopuses[i][j]++;
                    }
                }
            // One octopus at a time: If it's > 9, increase all neighbours with 1. 0 indicates that it has already been handled. 
            doItAgain:
                for (int i = 0; i <= octopuses.Length - 1; i++)
                {
                    for (int j = 0; j <= octopuses[0].Length - 1; j++)
                    {
                        if (octopuses[i][j] > 9)
                        {
                            // Add 1 on all neighbours larger than 0 (since 0 has already been handled)
                            if (i > 0 && j > 0 && octopuses[i - 1][j - 1] > 0)
                            {
                                octopuses[i - 1][j - 1]++;
                            }
                            if (i > 0 && octopuses[i - 1][j] > 0)
                            {
                                octopuses[i - 1][j]++;
                            }
                            if (i > 0 && j < octopuses[0].Length - 1 && octopuses[i - 1][j + 1] > 0)
                            {
                                octopuses[i - 1][j + 1]++;
                            }
                            if (j > 0 && octopuses[i][j - 1] > 0)
                            {
                                octopuses[i][j - 1]++;
                            }
                            if (j > 0 && i < octopuses.Length - 1 && octopuses[i + 1][j - 1] > 0)
                            {
                                octopuses[i + 1][j - 1]++;
                            }
                            if (i < octopuses.Length - 1 && octopuses[i + 1][j] > 0)
                            {
                                octopuses[i + 1][j]++;
                            }
                            if (j < octopuses[0].Length - 1 && octopuses[i][j + 1] > 0)
                            {
                                octopuses[i][j + 1]++;
                            }
                            if (i < octopuses.Length - 1 && j < octopuses[0].Length - 1 && octopuses[i + 1][j + 1] > 0)
                            {
                                octopuses[i + 1][j + 1]++;
                            }
                            octopuses[i][j] = 0;
                            goto doItAgain;
                        }
                    }
                }
                // Count flashes
                int checkpart2 = 100;
                for (int i = 0; i <= octopuses.Length - 1; i++)
                {
                    for (int j = 0; j <= octopuses[0].Length - 1; j++)
                    {
                        if (octopuses[i][j] == 0)
                        {
                            flashes++;
                            checkpart2--;
                        } 
                    }
                }
                if (iteration == 99) // index 99 means we've stepped 100
                    answerPart1 = flashes; // This was added to be able to calculate part 1 and then continue for part 2
                if (checkpart2 == 0 && answerPart2 == 0)
                    answerPart2 = iteration + 1; // since iteration is an index, we have to add 1 for the answer
            }

            System.Console.WriteLine("Answer part 1: " + answerPart1 + " and part 2: "+answerPart2);
        }
    }
}

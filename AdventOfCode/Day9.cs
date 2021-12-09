using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day9
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/day9.txt").ToArray();
            int width = lines[0].Length;
            int answerPart1 = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (i == 0 || lines[j][i] < lines[j][i - 1])
                        if (j == 0 || lines[j][i] < lines[j - 1][i])
                            if (i == width - 1 || lines[j][i] < lines[j][i + 1])
                                if (j == lines.Length - 1 || lines[j][i] < lines[j + 1][i])
                                {
                                    answerPart1 += int.Parse(lines[j][i].ToString()) + 1;
                                }
                }
            }
            System.Console.WriteLine("Answer part 1: " + answerPart1);

            // Recursively look through all areas to find the size of the basin

            // This is used to mark all areas that are either 9 or already handled
            bool[][] unavailable = new bool[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
                unavailable[i] = new bool[width];

            // mark all 9
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    unavailable[j][i] = (lines[j][i] == '9');
                }
            }

            int[] answers = { 0, 0, 0 }; // the three largest basins' size, sorted in size with smallest first

            // look through all fields
            lookAgain:
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (!unavailable[j][i])
                    {
                        int size = returnNumberOfNeighbours(j, i, lines, unavailable);
                        if (size > answers[2]) // largest basin found so far
                        {
                            answers[0] = answers[1];
                            answers[1] = answers[2];
                            answers[2] = size;
                        }
                        else if(size > answers[1]) //second largest so far
                        {
                            answers[0] = answers[1];
                            answers[1] = size;
                        }
                        else if (size > answers[0]) //third largest so far
                        {
                            answers[0] = size;
                        }
                        goto lookAgain; // we'll start from the beginning
                    }
                       
                }
            }

            System.Console.WriteLine("Answer part 2: " + (answers[0] * answers[1] * answers[2]) + " (" + answers[0] +", "+ answers[1] + ", " + answers[2]+")");
        }
        public static int returnNumberOfNeighbours(int x, int y, string[] lines, bool[][] unavailable)
        {
            //if (x < 0 || y < 0)
            //    return 0;
            unavailable[x][y] = true;
            int sum = 1;
            if (x > 0 && !unavailable[x-1][y])
                sum += returnNumberOfNeighbours(x - 1, y, lines, unavailable);
            if (y > 0 && !unavailable[x][y - 1])
                sum += returnNumberOfNeighbours(x, y - 1, lines, unavailable);
            if (x < lines[0].Length-1 && !unavailable[x + 1][y])
                sum += returnNumberOfNeighbours(x + 1, y, lines, unavailable);
            if (y < lines.Length-1 && !unavailable[x][y + 1])
                sum += returnNumberOfNeighbours(x, y + 1, lines, unavailable);
            return sum; 
        }
    }
}

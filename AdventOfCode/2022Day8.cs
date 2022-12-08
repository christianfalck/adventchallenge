using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace AdventOfCode
{
    class Day8_2022
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2022day8.txt").ToArray();

            int height = lines.Length;
            int width = lines[0].Length;

            int[][] allNumbers = new int[height][];
            bool[][] isVisible = new bool[height][];

            int numberOfVisible = 0;

            // Go through all numbers from left to right
            for (int y = 0; y < height; y++)
            {
                allNumbers[y] = new int[width];
                isVisible[y] = new bool[width];
                int highestTree = -1;
                for (int x = 0; x < width; x++)
                {
                    allNumbers[y][x] = int.Parse(lines[y][x].ToString());
                    if (allNumbers[y][x] > highestTree)
                    {
                        highestTree = allNumbers[y][x];
                        if (!isVisible[y][x])
                        {
                            isVisible[y][x] = true;
                            numberOfVisible++;
                        }
                    }
                }
            }

            // Go through all numbers from right to left
            for (int y = 0; y < height; y++)
            {
                int highestTree = -1;
                for (int x = width - 1; x >= 0; x--)
                {
                    if (allNumbers[y][x] > highestTree)
                    {
                        highestTree = allNumbers[y][x];
                        if (!isVisible[y][x])
                        {
                            isVisible[y][x] = true;
                            numberOfVisible++;
                        }
                    }
                }
            }

            // Go through all numbers from top to bottom
            for (int x = 0; x < width; x++)
            {
                int highestTree = -1;
                for (int y = 0; y < height; y++)
                {
                    if (allNumbers[y][x] > highestTree)
                    {
                        highestTree = allNumbers[y][x];
                        if (!isVisible[y][x])
                        {
                            isVisible[y][x] = true;
                            numberOfVisible++;
                        }
                    }
                }
            }

            // Go through all numbers from bottom to top
            for (int x = 0; x < width; x++)
            {
                int highestTree = -1;
                for (int y = width - 1; y >= 0; y--)
                {
                    if (allNumbers[y][x] > highestTree)
                    {
                        highestTree = allNumbers[y][x];
                        if (!isVisible[y][x])
                        {
                            isVisible[y][x] = true;
                            numberOfVisible++;
                        }
                    }
                }
            }

            // count number of visible trees
            int answerPart1 = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (isVisible[y][x])
                    {
                        answerPart1++;
                    }
                }
            }

            int answerPart2 = 0;
            //Loop through all trees, for each: go as far as possible in each direction and multiply those distances. If the value is larger -> answerPart2
            for (int y = 1; y < height - 1; y++) // Borders will always have score 0
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int score = 1;
                    int treeHeight = allNumbers[y][x];

                    // Same logic for all of these: One of three things can happen when walking a step in any direction: 
                    // Reach end of board = stop looking. Don't go another step. 
                    // Reach a tree with similar height or higher. Take one final step. 
                    // Reach a smaller tree: Continue exploring. 

                    //Look up
                    int y_2 = y - 1;
                    int distance = 0;
                    bool found = false;
                    while (!found)
                    {
                        if (y_2 < 0)
                        {
                            // reached end of board
                            found = true;

                        }
                        else if (allNumbers[y_2][x] >= treeHeight)
                        {
                            // reached a high tree
                            distance++;
                            found = true;

                        }
                        else if (allNumbers[y_2][x] < treeHeight)
                        {
                            // continue counting, smaller tree ahead
                            distance++;
                            y_2--;

                        }
                    }
                    score *= distance;

                    //Look down
                    y_2 = y + 1;
                    distance = 0;
                    found = false;
                    while (!found)
                    {
                        if (y_2 >= width)
                        {
                            // reached end of board
                            found = true;

                        }
                        else if (allNumbers[y_2][x] >= treeHeight)
                        {
                            // reached a high tree
                            distance++;
                            found = true;

                        }
                        else if (allNumbers[y_2][x] < treeHeight)
                        {
                            // continue counting, smaller tree ahead
                            distance++;
                            y_2++;

                        }
                    }
                    score *= distance;

                    //Look left
                    int x_2 = x - 1;
                    distance = 0;
                    found = false;
                    while (!found)
                    {
                        if (x_2 < 0)
                        {
                            // reached end of board
                            found = true;

                        }
                        else if (allNumbers[y][x_2] >= treeHeight)
                        {
                            // reached a high tree
                            distance++;
                            found = true;

                        }
                        else if (allNumbers[y][x_2] < treeHeight)
                        {
                            // continue counting, smaller tree ahead
                            distance++;
                            x_2--;

                        }
                    }
                    score *= distance;

                    //Look right
                    x_2 = x + 1;
                    distance = 0;
                    found = false;
                    while (!found)
                    {
                        if (x_2 >= height)
                        {
                            // reached end of board
                            found = true;

                        }
                        else if (allNumbers[y][x_2] >= treeHeight)
                        {
                            // reached a high tree
                            distance++;
                            found = true;

                        }
                        else if (allNumbers[y][x_2] < treeHeight)
                        {
                            // continue counting, smaller tree ahead
                            distance++;
                            x_2++;

                        }
                    }
                    score *= distance;


                    if (score > answerPart2)
                    {
                        answerPart2 = score;
                    }
                }
            }

            System.Console.WriteLine("Answer part 1: " + answerPart1 + " and part 2: " + answerPart2);

        }
    }
}

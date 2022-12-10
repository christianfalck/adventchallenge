using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode
{
    class Day9_2022
    {
        public static void calculate()
        {
            string[] lines = File.ReadLines("./../../../inputfiles/2022day9.txt").ToArray();
            // starting positions
            int head_x = 0;
            int head_y = 0;
            int tail_x = 0;
            int tail_y = 0;
            // keeping track of the tails positions
            Dictionary<string, int> tailPositions = new Dictionary<string, int>();
            tailPositions.Add("x0y0", 1);
            foreach (string line in lines)
            {
                int steps = int.Parse(line[line.IndexOf(" ")..]);
                if (line.StartsWith("R"))
                {
                    //move right
                    for (int a = 0; a < steps; a++)
                    {
                        head_x++;
                        if (tail_x < head_x - 1)
                        {
                            //tail is falling behind, move closer
                            tail_x++;
                            if (tail_y != head_y)
                            {
                                //move in y-axis while we're already moving
                                tail_y = head_y; // this covers both up and down 
                            }
                            // adding the position if it's the first, or increasing the value if not
                            string key = "x" + tail_x + "y" + tail_y;
                            tailPositions.TryGetValue(key, out var currentCount);
                            tailPositions[key] = currentCount + 1;
                        }
                    }
                }
                else if (line.StartsWith("L"))
                {
                    //move left
                    for (int a = 0; a < steps; a++)
                    {
                        head_x--;
                        if (tail_x > head_x + 1)
                        {
                            //tail is falling behind, move closer
                            tail_x--;
                            if (tail_y != head_y)
                            {
                                //move in y-axis while we're already moving
                                tail_y = head_y; // this covers both up and down
                            }
                            // adding the position if it's the first, or increasing the value if not
                            string key = "x" + tail_x + "y" + tail_y;
                            tailPositions.TryGetValue(key, out var currentCount);
                            tailPositions[key] = currentCount + 1;
                        }
                    }
                }
                else if (line.StartsWith("D"))
                {
                    //move down
                    for (int a = 0; a < steps; a++)
                    {
                        head_y++;
                        if (tail_y < head_y - 1)
                        {
                            //tail is falling behind, move closer
                            tail_y++;
                            if (tail_x != head_x)
                            {
                                tail_x = head_x;
                            }
                            // adding the position if it's the first, or increasing the value if not
                            string key = "x" + tail_x + "y" + tail_y;
                            tailPositions.TryGetValue(key, out var currentCount);
                            tailPositions[key] = currentCount + 1;
                        }
                    }
                }
                else if (line.StartsWith("U"))
                {
                    //move up
                    for (int a = 0; a < steps; a++)
                    {
                        head_y--;
                        if (tail_y > head_y + 1)
                        {
                            //tail is falling behind, move closer
                            tail_y--;
                            if (tail_x != head_x)
                            {
                                tail_x = head_x;
                            }
                            string key = "x" + tail_x + "y" + tail_y;
                            tailPositions.TryGetValue(key, out var currentCount);
                            tailPositions[key] = currentCount + 1;
                        }
                    }
                }
            }

            string answerPart1 = ""+tailPositions.Count;

            //Part 2

            // positions of all knots
            int[] positionsX = new int[10];
            int[] positionsY = new int[10];

            tailPositions = new Dictionary<string, int>();
            tailPositions.Add("x0y0", 1);

            foreach (string line in lines)
            {
                string command = line[..1];
                int steps = int.Parse(line[line.IndexOf(" ")..]);
                for (int s = 0; s < steps; s++)
                {
                    // move the head
                    switch (command)
                    {
                        case "U":
                            //move up
                            positionsY[0]--;
                            break;
                        case "D":
                            //move down
                            positionsY[0]++;
                            break;
                        case "L":
                            //move left
                            positionsX[0]--;
                            break;
                        case "R":
                            //move right
                            positionsX[0]++;
                            break;
                    }

                    // move the knots and tail
                    for (int a = 1; a <= 9; a++)
                    {
                        if (Math.Abs(positionsY[a] - positionsY[a - 1]) >= 2)
                        {
                            // Need to move in Y-axis
                            // If head is bigger, then we'll add 1, otherwise remove 1. 
                            positionsY[a] = positionsY[a] + (positionsY[a - 1] - positionsY[a]) / 2;
                            // Move in X-axis while we're at it
                            if (positionsX[a] < positionsX[a - 1])
                            {
                                positionsX[a]++;
                            }
                            else if (positionsX[a] > positionsX[a - 1])
                            {
                                positionsX[a]--;
                            }
                        }
                        if (Math.Abs(positionsX[a] - positionsX[a - 1]) >= 2)
                        {
                            // Need to move in X-axis
                            // If head is bigger, then we'll add 1, otherwise remove 1. 
                            positionsX[a] = positionsX[a] + (positionsX[a - 1] - positionsX[a]) / 2;
                            // Move in Y-axis while we're at it. 
                            if (positionsY[a] < positionsY[a - 1])
                            {
                                positionsY[a]++;
                            }
                            else if (positionsY[a] > positionsY[a - 1])
                            {
                                positionsY[a]--;
                            }
                        }
                    }
                    // Don't know why I keep storing the number of times a position has been visited but hey
                    string key = "x" + positionsX[9] + "y" + positionsY[9];
                    tailPositions.TryGetValue(key, out var currentCount);
                    tailPositions[key] = currentCount + 1;
                }
            }
            System.Console.WriteLine("Answer part 1: " + answerPart1 + ", answer part 2: " + tailPositions.Count);

        }

    }
}

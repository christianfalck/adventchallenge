using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode
{
    internal class Day14_2022
    {
        public static void calculate()
        {
            // Setup
            string[] input = System.IO.File.ReadLines("./../../../inputfiles/2022day14.txt").ToArray();

            // Parse all lines and create a list of lines. 498,4 -> 498,6 -> 496,6 = [498,4]->[498,6] & [498,6]->[496,6]
            Dictionary<Point,int> rocksAndSand = new Dictionary<Point, int>();
            
            // We need to keep check on when we fall out of map for part 1 and add an extra floor for part 2
            int largestY = 0;

            // Store all rocks in a dictionary with the coordinates as key and 1 as value (1 for rock)
            foreach(string i in input)
            {
                string[] split = i.Split("->");
                for(int a = 0; a<split.Length-1; a++)
                {
                    int x1 = int.Parse(split[a][..split[a].IndexOf(",")].Trim());
                    int y1 = int.Parse(split[a][(split[a].IndexOf(",")+1)..].Trim());
                    int x2 = int.Parse(split[a+1][..split[a+1].IndexOf(",")].Trim());
                    int y2 = int.Parse(split[a+1][(split[a+1].IndexOf(",")+1)..].Trim());

                    // Store the largest y
                    if(y1> largestY)
                        largestY = y1;
                    if(y2> largestY)
                        largestY = y2;

                    if (x1 == x2)
                    {
                        // Vertical line
                        if (y2 < y1)
                        {
                            for (int b = y2; b <= y1; b++) {
                                Point p = new Point(x1, b);
                                if(!rocksAndSand.ContainsKey(p))
                                    rocksAndSand.Add(p, 1); // 1 for rock
                            }
                        }
                        else
                        {
                            for (int b = y1; b <= y2; b++) {
                                Point p = new Point(x1, b);
                                if (!rocksAndSand.ContainsKey(p)) 
                                    rocksAndSand.Add(p, 1); // 1 for rock
                            }
                        }
                    }
                    else
                    {
                        // horizontal line
                        if (x2 < x1)
                        {
                            for (int b = x2; b <= x1; b++)
                            {
                                Point p = new Point(b,y1);
                                if (!rocksAndSand.ContainsKey(p))
                                    rocksAndSand.Add(p, 1); // 1 for rock
                            }
                        }
                        else
                        {
                            for (int b = x1; b <= x2; b++)
                            {
                                Point p = new Point(b, y1);
                                if (!rocksAndSand.ContainsKey(p))
                                    rocksAndSand.Add(p, 1); // 1 for rock
                            }
                        }
                    }



                }
            }

            // For part 2 we need a horizontal line large enough to keep the sand from falling
            for(int a = 0; a <=1000; a++)
            {
                rocksAndSand.Add(new Point(a, largestY + 2), 1);
            }

            // Add sand until it falls off the map
            bool noMoreSand = false; // indicates that the cave is full
            int unitsOfSand = 0;
            int answerPart1 = 0;
            int answerPart2 = 0;
            while(!noMoreSand)
            {
                // move one piece of sand
                bool sandIsPlaced = false;
                // The sand is pouring into the cave from point 500,0.
                int x = 500;
                int y = 0;
                while (!sandIsPlaced && !noMoreSand)
                {
                    y++;
                    // If we've fallen out of the known map, we stop part 1
                    if(answerPart1 == 0 && y > largestY)
                    {
                        // Part 1 is done
                        answerPart1= unitsOfSand;
                    }
                    if (rocksAndSand.ContainsKey(new Point(x, y)))
                    {
                        // The sand cannot continue falling right down since there's a rock there
                        if (rocksAndSand.ContainsKey(new Point(x - 1, y)))
                        {
                            // The sand cannot move down/left since there's a rock there
                            if (rocksAndSand.ContainsKey(new Point(x + 1, y)))
                            {
                                // The sand cannot move to the rigth either: Place the sand like this
                                //  o 
                                // ###
                                rocksAndSand.Add(new Point(x, y - 1), 2); // 2 for sand
                                sandIsPlaced = true;
                                unitsOfSand++;

                                // Part2: 
                                if (y == 1)
                                {
                                    noMoreSand = true;
                                    answerPart2 = unitsOfSand;
                                    break;
                                }
                            }
                            else
                            {
                                // move one step to the right and continue falling
                                x++;
                            }
                        }
                        else
                        {
                            // move one step to the left and continue falling
                            x--;
                        }
                    }
                }
            }

            System.Console.WriteLine("Answer part 1: " + answerPart1 + " and part 2: " + answerPart2);
        }
    }
}

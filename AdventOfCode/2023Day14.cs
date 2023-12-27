using System.Linq;

namespace AdventOfCode
{
    internal class Day14_2023
    {
        public static void calculate()
        {
            // Setup
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day14.txt").ToArray();
            bool[][] cubicRocks = new bool[lines.Length][];
            bool[][] roundRocks = new bool[lines.Length][];
            int answer = 0;
            // Translate rocks into two maps, one for cubic rocks (that don't move) and one for round rocks
            for (int y = 0; y < lines.Length; y++)
            {
                cubicRocks[y] = new bool[lines[0].Length];
                roundRocks[y] = new bool[lines[0].Length];
                // Place all rocks in a map
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        cubicRocks[y][x] = true;
                    }
                    else if (lines[y][x] == 'O')
                    {
                        roundRocks[y][x] = true;
                    }
                }
            }
            // Move all round rocks to the north
            for (int a = 1; a < roundRocks.Length; a++) // starts with 1 instead of 0 since we don't need to move the first row
            {
                for (int b = 0; b < roundRocks[0].Length; b++)
                {
                    if (roundRocks[a][b])
                    {
                        //CalculateHowMany(roundRocks);
                        bool canStillMove = true; 
                        // move it upwards as far as it goes. 
                        // Don't forget to remove the old rock
                        for (int i = a - 1; i >= 0; i--)
                        {
                            if (canStillMove && !roundRocks[i][b] && !cubicRocks[i][b])
                            {
                                // actually move it one step at the time
                                roundRocks[i+1][b] = false;
                                roundRocks[i][b] = true;
                            }
                            else
                            {
                                canStillMove = false; // don't continue moving this rock
                            }
                        }
                    }
                }
            }
            // Calculate the value of the rocks
            for (int a = 0; a < roundRocks.Length; a++)
            {
                for (int b = 0; b < roundRocks[0].Length; b++)
                {
                    if (roundRocks[a][b])
                    {
                        // Add the weight, first row has lines.length weight, then it decreases with 1 for each row
                        answer += lines.Length - a; 
                    }
                }
            }

            // Part 2 is to rotate to North, West, South & East .... 1000000000 times
            // Idea: Rotate and then calculate load for each cycle. There should start to emerge a pattern after a while so that 
            // we can stop after a smaller number of cycles.
            // Test: Do a reasonable number of cycles, enough to start the pattern but not break the computer. Calculate load L. 
            // Then go through the same number of cycles again and for each cycle, calculate the load and see if it's == L.
            // When we find the match, assume we have the pattern. 

            System.Console.WriteLine("Answer: " + answer);
        }

        // Just used as a visual debug
        static int CalculateHowMany(bool[][] map)
        {
            int answer = 0;
            foreach(bool[] line in map)
            {
                System.Console.WriteLine("");
                foreach (bool test in line)
                {
                    if (test)
                    {
                        System.Console.Write("0");
                        answer++;
                    }   
                    else
                        System.Console.Write(" ");
                }
            }
            System.Console.WriteLine("");
            return answer;
        }

        
    }
}

using System.Linq;

namespace AdventOfCode
{
    internal class Day25_2021
    {
        public static void calculate()
        {
            // Setup
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2021day25.txt").ToArray();
            int[][] cucumbers = new int[lines.Length][];
            bool[][] cucumbersToMove = new bool[lines.Length][];
            for (int y = 0; y < lines.Length; y++)
            {
                cucumbers[y] = new int[lines[0].Length];
                cucumbersToMove[y] = new bool[lines[0].Length];
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '>')
                    {
                        cucumbers[y][x] = 1; // Moving east = 1
                    }
                    else if (lines[y][x] == 'v')
                    {
                        cucumbers[y][x] = 2; // Moving south = 2
                    }
                    else
                    {
                        cucumbers[y][x] = 0; // No cucumber = 0
                    }
                }
            }

            // Continue by starting to move the cucumbers around
            bool move = true;
            int iterations = 0;
            while (move) 
            {
                // Move all eastward 
                bool east = moveEast(cucumbers, cucumbersToMove);
                // Move all southward
                bool south = moveSouth(cucumbers, cucumbersToMove);
                move = east || south; // either one is still moving, keep going
                iterations++;
            }
            System.Console.WriteLine("Answer: " + iterations);
        }

        static bool moveEast(int[][] cucumbers, bool[][] cucumbersToMove)
        {
            //TODO find a better way to clear the matrix
            //bool[][] tmpCucumbersToMove = (bool[][])cucumbersToMove.Clone(); <- only shallow copy which
            //means changing in one will also change the other
            for (int i = 0; i < cucumbersToMove.Length; i++)
                for (int j = 0; j < cucumbersToMove[0].Length; j++)
                    cucumbersToMove[i][j] = false;
            bool movedThisTurn = false; 
            // Start with finding all eastward moving cucumbers that don't have a neighbour to the east. 
            // If a cucumber is on the far east, look at the location far west (world wrap)
            for (int i = 0; i < cucumbers.Length; i++)
            {
                for (int j = 0; j < cucumbers[0].Length; j++)
                {
                    if (cucumbers[i][j] == 1) // Cucumber facing east
                    {
                        int destination = ((j + 1) == cucumbers[0].Length) ? 0 : j + 1; // world wrap
                        if (cucumbers[i][destination] == 0)
                        {
                            cucumbersToMove[i][j] = true;
                            movedThisTurn = true; 
                        }
                    }
                }
            }
            // Continue by moving all those who could be moved
            for (int i = 0; i < cucumbers.Length; i++)
            {
                for (int j = 0; j < cucumbers[0].Length; j++)
                {
                    if (cucumbersToMove[i][j])
                    {
                        int destination = ((j + 1) == cucumbers[0].Length) ? 0 : j + 1; // world wrap
                        cucumbers[i][j] = 0;
                        cucumbers[i][destination] = 1;
                    }
                }
            }
            return movedThisTurn; // There might still be more moves ahead
        }

        static bool moveSouth(int[][] cucumbers, bool[][] cucumbersToMove)
        {
            for (int i = 0; i < cucumbersToMove.Length; i++)
                for (int j = 0; j < cucumbersToMove[0].Length; j++)
                    cucumbersToMove[i][j] = false;  // Otherwise we'll reuse the same matrix
            bool movedThisTurn = false; 
            // Start with finding all southward moving cucumbers that don't have a neighbour to the south. 
            // If a cucumber is on the far south, look at the location far north (World wrap)
            for (int i = 0; i < cucumbers.Length; i++)
            {
                for (int j = 0; j < cucumbers[0].Length; j++)
                {
                    if (cucumbers[i][j] == 2) // Cucumber facing south
                    {
                        int destination = ((i + 1) == cucumbers.Length) ? 0 : i + 1; // world wrap
                        if (cucumbers[destination][j] == 0)
                        {
                            cucumbersToMove[i][j] = true;
                            movedThisTurn = true; 
                        }
                    }
                }
            }
            // Continue by moving all those who could be moved
            for (int i = 0; i < cucumbers.Length; i++)
            {
                for (int j = 0; j < cucumbers[0].Length; j++)
                {
                    if (cucumbersToMove[i][j])
                    {
                        int destination = ((i + 1) == cucumbers.Length) ? 0 : i + 1; // world wrap
                        cucumbers[i][j] = 0;
                        cucumbers[destination][j] = 2;
                    }
                }
            }
            return movedThisTurn; // There might still be more moves ahead
        }
    }
}

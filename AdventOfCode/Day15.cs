using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day15
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/day15.txt").ToArray();
            int[][] allNumbers = new int[lines.Length][];
            int[][] costToEnd = new int[lines.Length][]; // The cost from this point to the end
            for (int y = 0; y < lines.Length; y++)
            {
                allNumbers[y] = new int[lines[y].Length];
                costToEnd[y] = new int[lines[y].Length];
                for (int x = 0; x < lines[y].Length; x++)
                {
                    allNumbers[y][x] = lines[y][x] - '0';
                    costToEnd[y][x] = -1;
                }
            }
            int answer = stepOne(0, 0, allNumbers, costToEnd);

            System.Console.WriteLine("Answer part 1: " + answer);

        }

        public static int stepOne(int x, int y, int[][] numbers, int[][] costToEnd)
        {
            //If this point has already been calculated, we don't need to do it again. 
            if (costToEnd[y][x] > 0)
                return costToEnd[y][x] + numbers[y][x];
            if (x == numbers[0].Length - 1 && y == numbers.Length - 1)
            {
                // Bottom right
                costToEnd[y][x] = 0; // Since we're already at the end
                return numbers[y][x];
            }
            else if (y == numbers.Length - 1)
            {
                //bottom row
                int cost = stepOne(x + 1, y, numbers, costToEnd);
                costToEnd[y][x] = cost;
                return cost + numbers[y][x];
            }
            else if (x == numbers[0].Length - 1)
            {
                // far right
                int cost = stepOne(x, y + 1, numbers, costToEnd);
                costToEnd[y][x] = cost;
                return cost + numbers[y][x];
            }
            else if (x == 0 && y == 0)
            {
                // Don't return it's own value
                int a = stepOne(x + 1, y, numbers, costToEnd);
                int b = stepOne(x, y + 1, numbers, costToEnd);
                if (a < b)
                {
                    costToEnd[y][x] = a; // Not needed but completes the calculation
                    return a;
                }
                else
                {
                    costToEnd[y][x] = b; // Not needed but completes the calculation
                    return b;
                }
            }
            else
            {
                // somewhere in the middle
                int a = stepOne(x + 1, y, numbers, costToEnd);
                int b = stepOne(x, y + 1, numbers, costToEnd);
                if (a < b)
                {
                    costToEnd[y][x] = a;
                    return a + numbers[y][x];
                }
                else
                {
                    costToEnd[y][x] = b;
                    return b + numbers[y][x];
                }
            }
        }
    }
}

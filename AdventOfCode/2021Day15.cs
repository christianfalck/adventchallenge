using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day15_2021
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2021day15.txt").ToArray();
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

            // Part 2
            // Thanks to https://github.com/encse for input to how it's supposed to be solved correctly
            var allNumbersDictionary = new Dictionary<Point, int>(); // the initial risk values
            var costFromStartDictionary = new Dictionary<Point, int>(); // calculated risk 
            for (int y = 0; y < lines.Length; y++)
                for (int i = 0; i < 5; i++)
                    for (int x = 0; x < lines[y].Length; x++)
                        for (int j = 0; j < 5; j++)
                        {
                            int val = (lines[y][x] - '0') + (j + i);
                            if (val > 9)
                                val = val - 9;
                            allNumbersDictionary.Add(new Point(x + j * lines[y].Length, y + i * lines.Length), val);

                        }
            bool endFound = false;
            var myQueue = new PriorityQueue<Point, int>();
            myQueue.Enqueue(new Point(0, 0), 0); // Start with upper left 
            var endPoint = new Point(lines[0].Length * 5 - 1, lines.Length * 5 - 1);
            var startPoint = new Point(0, 0);
            costFromStartDictionary.Add(startPoint, 0);
            while (!endFound)
            {
                var point = myQueue.Dequeue();
                foreach (var neighbour in Neighbours(point))
                {
                    if (allNumbersDictionary.ContainsKey(neighbour) && !costFromStartDictionary.ContainsKey(neighbour))
                    {
                        var totalRisk = costFromStartDictionary[point] + allNumbersDictionary[neighbour];
                        costFromStartDictionary[neighbour] = totalRisk;
                        if (neighbour == endPoint)
                        {
                            endFound = true;
                        }
                        myQueue.Enqueue(neighbour, totalRisk);
                    }
                    // Theoretically there might be a case where we reach a point again, but considering we're 
                    // calculating with smallest total risk first, each point will be reached by the lowest risk score
                    // first
                }
            }
            answer = costFromStartDictionary[endPoint];
            System.Console.WriteLine("Answer part 2: " + answer);
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

        // Returning all neighbours, can return invalid neighbors if point is on the edge
        public static IEnumerable<Point> Neighbours(Point point) =>
        new[] {
           point with {y = point.y + 1},
           point with {y = point.y - 1},
           point with {x = point.x + 1},
           point with {x = point.x - 1},
        };
    }
}

record Point(int x, int y); // New feature in Visual studio 2022! 
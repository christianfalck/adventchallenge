using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day12_2022
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2022day12.txt").ToArray();
            var elevationDictionary = new Dictionary<Point, int>(); // the elevation for each point
            var costFromStartDictionary = new Dictionary<Point, int>(); // how far we've walked in each point 

            Point startPositionPart1 = null;
            List<Point> startPositionsPart2 = new List<Point>();
            Point endPosition = null;

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == 'S')
                    {
                        startPositionPart1 = new Point(x, y);
                        startPositionsPart2.Add(new Point(x, y));
                        elevationDictionary.Add(new Point(x, y), 'a' - 'a'); // start counts as a
                    }
                    else if (lines[y][x] == 'E')
                    {
                        endPosition = new Point(x, y);
                        elevationDictionary.Add(new Point(x, y), 'z' - 'a'); // end counts as z
                    }
                    else
                    {
                        if (lines[y][x] == 'a')
                        {
                            startPositionsPart2.Add(new Point(x, y));
                        }
                        elevationDictionary.Add(new Point(x, y), lines[y][x] - 'a');
                    }
                }
            }

            // Part 1
            int answerPart1;

            bool endFound = false;
            var myQueue = new PriorityQueue<Point, int>();
            myQueue.Enqueue(startPositionPart1, 0); 

            costFromStartDictionary.Add(startPositionPart1, 0);
            while (!endFound)
            {
                var point = myQueue.Dequeue();
                foreach (var neighbour in Neighbours(point))
                {
                    if (elevationDictionary.ContainsKey(neighbour) && !costFromStartDictionary.ContainsKey(neighbour) &&
                        (elevationDictionary[neighbour] - 1 <= elevationDictionary[point])) // can only move if it's maximum one step higher
                    {
                        var distance = costFromStartDictionary[point] + 1;
                        costFromStartDictionary[neighbour] = distance;
                        if (neighbour == endPosition)
                        {
                            endFound = true;
                        }
                        myQueue.Enqueue(neighbour, distance);
                    }
                }
            }
            answerPart1 = costFromStartDictionary[endPosition];

            // Part 2
            int answerPart2 = int.MaxValue; // this is used to compare to

            foreach (Point p in startPositionsPart2)
            {
                endFound = false;
                myQueue = new PriorityQueue<Point, int>();
                myQueue.Enqueue(p, 0);
                costFromStartDictionary = new Dictionary<Point, int>();
                costFromStartDictionary.Add(p, 0);
                while (!endFound)
                {
                    if(myQueue.Count == 0)
                    {
                        // this starting position has no valid neighbours
                        costFromStartDictionary[endPosition] = int.MaxValue; 
                        break;
                    }
                    var point = myQueue.Dequeue();
                    foreach (var neighbour in Neighbours(point))
                    {
                        if (elevationDictionary.ContainsKey(neighbour) && !costFromStartDictionary.ContainsKey(neighbour) &&
                            (elevationDictionary[neighbour] - 1 <= elevationDictionary[point])) // can only move if it's maximum one step higher
                        {
                            var distance = costFromStartDictionary[point] + 1;
                            costFromStartDictionary[neighbour] = distance;
                            if (neighbour == endPosition)
                            {
                                endFound = true;
                            }
                            myQueue.Enqueue(neighbour, distance);
                        }
                    }
                }
                if(answerPart2 > costFromStartDictionary[endPosition])
                    answerPart2 = costFromStartDictionary[endPosition];
            }

            System.Console.WriteLine("Answer part 1: " + answerPart1 + " and part 2: " + answerPart2);


            // Part 3 - A.K.A. WHAT IF....(Thanks to emilitito for pushing me into doing this even though I was already done with the assignment)
            // This gives the same result as Part 2 but more efficient as it traverses from the endpoint until it finds one of the startpoints
            // rather than the function above where I calculate all startpoints and keep the shortest path
            int answerPart3;
            bool startFound = false;
            Point startPosition= null;
            myQueue = new PriorityQueue<Point, int>();
            myQueue.Enqueue(endPosition, 0);
            var costFromEndDictionary = new Dictionary<Point, int>();
            costFromEndDictionary.Add(endPosition, 0);
            while (!startFound)
            {
                var point = myQueue.Dequeue();
                foreach (var neighbour in Neighbours(point))
                {
                    if (elevationDictionary.ContainsKey(neighbour) && !costFromEndDictionary.ContainsKey(neighbour) &&
                        (elevationDictionary[neighbour] >= elevationDictionary[point] - 1)) // can only move if it's maximum one step higher - BUT INVERTED :-)
                    {
                        var distance = costFromEndDictionary[point] + 1;
                        costFromEndDictionary[neighbour] = distance;
                        if (startPositionsPart2.Contains(neighbour))
                        {
                            startFound = true;
                            startPosition = neighbour;
                        }
                        myQueue.Enqueue(neighbour, distance);
                    }
                }
            }
            answerPart3 = costFromEndDictionary[startPosition];
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

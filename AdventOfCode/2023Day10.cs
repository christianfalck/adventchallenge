using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day10_2023
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day10.txt").ToArray();

            /* 
             * I was hoping for the part 2 to be some kind of verion of the tasks previous years
             * where we need to start at all kinds of positions and find the shortest one so I overengineered 
             * part 1
             * 
             * Walking in all possible directions from the start, we know how far we can get when we only find paths where 
             * we've already been
             */
            Point startPosition = null;
            var pipeDictionary = new Dictionary<Point, char>(); // the pipe in each point
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == 'S')
                    {
                        startPosition = new Point(x, y);
                        // TODO: Lazy and added this manually 
                        pipeDictionary.Add(new Point(x, y), '|');
                    }
                    else
                    {
                        pipeDictionary.Add(new Point(x, y), lines[y][x]);
                    }
                }
            }
            var costFromStartDictionary = new Dictionary<Point, int>(); // how far we've walked in each point 
            int answer = 0;
            var myQueue = new PriorityQueue<Point, int>();
            myQueue.Enqueue(startPosition, 0);

            costFromStartDictionary.Add(startPosition, 0);
            while (myQueue.Count>0)
            {
                var point = myQueue.Dequeue();
                foreach (var neighbour in Neighbours(point, pipeDictionary[point]))
                {
                    if (!costFromStartDictionary.ContainsKey(neighbour))
                    {
                        var distance = costFromStartDictionary[point] + 1;
                        costFromStartDictionary[neighbour] = distance;
                        myQueue.Enqueue(neighbour, distance);
                        answer = distance;
                    }
                }
            }

            System.Console.WriteLine("Answer part 1: " + answer);

            /*
             * Idea for part 2: 
             * Store all lines between two coordinates, like for this example [0,0 - 4,2]
             * .S-7.
             * .|.|.
             * .L-J.
             * Then we store 
             * 1,0-2,0 
             * 2,0-3,0 
             * 1,0-1,1 
             * 1,1-1,2 
             * 1,2-2,2 
             * 2,2-3,2 
             * 3,0-3,1 
             * 3,1-3,2
             * With all these borders, you start from all intersections by the edge (since they naturally are 
             * potentially outside the loop) and then go in all directions and look at all neighbours to find 
             * all empty spaces outside the loop.
             * Take the intersection 0,0/1,0/0,1/1,1. 0,0 is outside. 0,1 also. Only two ways to other 
             * intersections from here: Right where we're blocked by the 1,0-1,1 line.
             * Down where we have no line for 0,1-1,1. So here we get to 0,1/1,1/0,2/1,2 where we find a new node at 0,2.
             * 
             * It would be rather easy to find all outside if there wasn't the problem that animals can squeeze between pipes.
             * 
             * When we've marked all outside as described above, take 
             * all nodes (140*140) - the loop itself (answer1*2) - all marked as outside = answer2
             * */
        }

        // Returning all neighbours where this pipe leads
        public static List<Point> Neighbours(Point point, char pipe)
        {
            var neighboursToReturn = new List<Point>();
            // This for all different pipes: |, -, L, J, 7, F
            if (pipe == '|')
            {
                neighboursToReturn.Add(point with { y = point.y + 1 });
                neighboursToReturn.Add(point with { y = point.y - 1 });
            }
            if (pipe == '-')
            {
                neighboursToReturn.Add(point with { x = point.x + 1 });
                neighboursToReturn.Add(point with { x = point.x - 1 });
            }
            if (pipe == 'L')
            {
                neighboursToReturn.Add(point with { y = point.y - 1 });
                neighboursToReturn.Add(point with { x = point.x + 1 });
            }
            if (pipe == 'J')
            {
                neighboursToReturn.Add(point with { y = point.y - 1 });
                neighboursToReturn.Add(point with { x = point.x - 1 });
            }
            if (pipe == '7')
            {
                neighboursToReturn.Add(point with { y = point.y + 1 });
                neighboursToReturn.Add(point with { x = point.x - 1 });
            }
            if (pipe == 'F')
            {
                neighboursToReturn.Add(point with { y = point.y + 1 });
                neighboursToReturn.Add(point with { x = point.x + 1 });
            }
            return neighboursToReturn;
        }

    }
}

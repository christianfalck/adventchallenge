using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day16_2023
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day16.txt").ToArray();

            /* 
             * Create a Splitter dictionary with info about all splitters. 
             * Created a Visited dictionary where we store information from which direction each point has been visited,
             * since we don't need to continue calculate if we've already been in a point from that direction before. 
             * Answer for part 1 will be the number of keys in this dictionary. 
             * Create a class Beam with position and direction. 
             * Put all beams in a queue. 
             * Create a while-loop which runs as long as there are active beams. 
             */
            Beam startBeam = new Beam();
            startBeam.position = new Point(0, 0); // top left corner
            startBeam.myDirection = Beam.Direction.Right;
            Dictionary<Point, char> splitters = new Dictionary<Point, char>();
            Dictionary<Point, List<Beam.Direction>> visited = new Dictionary<Point, List<Beam.Direction>>();
            int answer = 0;
            var myQueue = new Queue<Beam>();
            myQueue.Enqueue(startBeam);
            int maxX = lines[0].Length;
            int maxY = lines.Length;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] != '.')
                    {
                        splitters.Add(new Point(x, y), lines[y][x]);
                    }
                    visited.Add(new Point(x, y), new List<Beam.Direction>());
                }
            }
            while (myQueue.Count > 0)
            {
                Beam currentBeam = myQueue.Dequeue();
                // Don't continue if the current Beam is out of bounds
                if (currentBeam.position.x < 0 || currentBeam.position.x >= maxX ||
                    currentBeam.position.y < 0 || currentBeam.position.y >= maxY)
                {
                    // Do nothing!
                    continue;
                }
                // mark current Point as visited
                if (visited[currentBeam.position].Contains(currentBeam.myDirection))
                {
                    // Already been here with the same direction. Skip the rest
                    continue;
                }
                else
                {
                    visited[currentBeam.position].Add(currentBeam.myDirection);
                }

                // if we don't stand on a splitter, move one step in direction
                if (!splitters.ContainsKey(currentBeam.position))
                {
                    int x = currentBeam.position.x + currentBeam.getDirection().Item2;
                    int y = currentBeam.position.y + currentBeam.getDirection().Item1;
                    currentBeam.position = new Point(x, y);
                    myQueue.Enqueue(currentBeam);
                }

                // if we stand on a splitter, remove this beam 
                // and create one or two new beams
                // One for /\, two for |-
                // move the new splitter(s) one step
                // put them on the queue
                else if (splitters.ContainsKey(currentBeam.position))
                {
                    int x, y;
                    switch (splitters[currentBeam.position])
                    {
                        case '/':
                            currentBeam.changeDirection('/');
                            x = currentBeam.position.x + currentBeam.getDirection().Item2;
                            y = currentBeam.position.y + currentBeam.getDirection().Item1;
                            currentBeam.position = new Point(x, y);
                            myQueue.Enqueue(currentBeam);
                            break;
                        case '\\': // in c# you have to define this by \\ but means a single \
                            currentBeam.changeDirection('\\');
                            x = currentBeam.position.x + currentBeam.getDirection().Item2;
                            y = currentBeam.position.y + currentBeam.getDirection().Item1;
                            currentBeam.position = new Point(x, y);
                            myQueue.Enqueue(currentBeam);
                            break;
                        case '|':
                            // if we hit the pointy end we just move through this one
                            if (currentBeam.myDirection == Beam.Direction.Up ||
                                currentBeam.myDirection == Beam.Direction.Down)
                            {
                                x = currentBeam.position.x + currentBeam.getDirection().Item2;
                                y = currentBeam.position.y + currentBeam.getDirection().Item1;
                                currentBeam.position = new Point(x, y);
                                myQueue.Enqueue(currentBeam);
                            }
                            else
                            {
                                // remove the beam and create two new beams instead
                                // add them to the queue
                                Beam b1 = new Beam();
                                b1.position = new Point(currentBeam.position.x, currentBeam.position.y - 1);
                                b1.myDirection = Beam.Direction.Up;
                                myQueue.Enqueue(b1);
                                Beam b2 = new Beam();
                                b2.position = new Point(currentBeam.position.x, currentBeam.position.y + 1);
                                b2.myDirection = Beam.Direction.Down;
                                myQueue.Enqueue(b2);
                            }
                            break;
                        case '-':
                            // if we hit the pointy end we just move through this one
                            if (currentBeam.myDirection == Beam.Direction.Left ||
                                currentBeam.myDirection == Beam.Direction.Right)
                            {
                                x = currentBeam.position.x + currentBeam.getDirection().Item2;
                                y = currentBeam.position.y + currentBeam.getDirection().Item1;
                                currentBeam.position = new Point(x, y);
                                myQueue.Enqueue(currentBeam);
                            }
                            else
                            {
                                // remove the beam and create two new beams instead
                                // add them to the queue
                                Beam b1 = new Beam();
                                b1.position = new Point(currentBeam.position.x - 1, currentBeam.position.y);
                                b1.myDirection = Beam.Direction.Left;
                                myQueue.Enqueue(b1);
                                Beam b2 = new Beam();
                                b2.position = new Point(currentBeam.position.x + 1, currentBeam.position.y);
                                b2.myDirection = Beam.Direction.Right;
                                myQueue.Enqueue(b2);
                            }
                            break;
                    }
                }
            }
            foreach(Point p in visited.Keys)
            {
                if (visited[p].Count > 0)
                    answer++;
            }

            System.Console.WriteLine("Answer part 1: " + answer);
        }
    }

    class Beam
    {
        public Point position { get; set; }

        public enum Direction { Left, Right, Up, Down }

        public Direction myDirection { get; set; }

        public (int, int) getDirection()
        {
            switch (myDirection)
            {
                case Direction.Left:
                    return (0, -1);
                case Direction.Right:
                    return (0, 1);
                case Direction.Up:
                    return (-1, 0);
                case Direction.Down:
                    return (1, 0);
                default:
                    return (0, 0);

            }
        }
        public void changeDirection(char mirror)
        {
            if (mirror == '/')
            {
                if (myDirection == Direction.Left)
                    myDirection = Direction.Down;
                else if (myDirection == Direction.Right)
                    myDirection = Direction.Up;
                else if (myDirection == Direction.Up)
                    myDirection = Direction.Right;
                else if (myDirection == Direction.Down)
                    myDirection = Direction.Left;
            }
            else if (mirror == '\\')
            {
                if (myDirection == Direction.Left)
                    myDirection = Direction.Up;
                else if (myDirection == Direction.Right)
                    myDirection = Direction.Down;
                else if (myDirection == Direction.Up)
                    myDirection = Direction.Left;
                else if (myDirection == Direction.Down)
                    myDirection = Direction.Right;
            }

        }
    }
}

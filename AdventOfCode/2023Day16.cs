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
             * Created a Visited dictionary where we store information about how many times each tile has been visited. 
             * Answer for part 1 will be the number of keys in this dictionary. 
             * Create a class Beam with position and direction. 
             * Put all beams in a queue. 
             * Create a while-loop which runs as long as there are active beams. 
             */
            Beam startBeam = new Beam();
            startBeam.position = new Point(0, 0); // top left corner
            startBeam.myDirection = Beam.Direction.Right;
            Dictionary<Point, char> splitters = new Dictionary<Point, char>();
            Dictionary<Point, int> visited = new Dictionary<Point, int>();
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
                }
            }
            while (myQueue.Count > 0)
            {
                /* TODO
                 * 
                 * Just add to the visited dictionary, don't increase
                 * Make sure you only add to the visited if we're in bounds
                 * Add some logic to check if we've been here before with the same direction
                 */


                Beam currentBeam = myQueue.Dequeue();
                // mark current Point as visited
                if (visited.ContainsKey(currentBeam.position))
                {
                    visited[currentBeam.position]++;
                }
                else
                {
                    visited.Add(currentBeam.position, 1);
                }

                // if we don't stand on a splitter, move one step in direction
                if (!splitters.ContainsKey(currentBeam.position))
                {
                    int x = currentBeam.position.x + currentBeam.getDirection().Item2;
                    int y = currentBeam.position.y + currentBeam.getDirection().Item1;
                    currentBeam.position = new Point(x, y);
                    // Add it to the queue again unless it's out of bounds
                    if (x >= 0 && x < maxX && y >= 0 && y < maxY)
                    {
                        myQueue.Enqueue(currentBeam);
                    }
                }

                // if we stand on a splitter, remove this beam (make sure it's removed from the queue)
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

            answer = visited.Count;

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

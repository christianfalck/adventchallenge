using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    internal class Day15_2022
    {
        public static void calculate()
        {
            string[] input = System.IO.File.ReadLines("./../../../inputfiles/2022day15.txt").ToArray();
            int answerPart1 = 0;
            BigInteger answerPart2 = 0;

            // Point is the location of the sensor, int is the range to the closest beacon
            Dictionary<Point, int> sensors = new Dictionary<Point, int>();

            // All beacons on the specified row, named checkRow here
            int checkRow = 2000000; // It's 10 for testdata, 2000000 for real data
            Dictionary<int, int> beaconsOnRow = new Dictionary<int, int>();

            // Store all sensors locations and the distance to closest beacon
            foreach (string i in input)
            {
                string sensorPos = i.Substring(i.IndexOf("x="), i.IndexOf(":") - i.IndexOf("x="));
                int sensorX = int.Parse(sensorPos.Substring(sensorPos.IndexOf("=") + 1, sensorPos.IndexOf(",") - sensorPos.IndexOf("=") - 1));
                int sensorY = int.Parse(sensorPos.Substring(sensorPos.LastIndexOf("=") + 1));

                string beaconPos = i.Substring(i.LastIndexOf("x="));
                int beaconX = int.Parse(beaconPos.Substring(beaconPos.IndexOf("=") + 1, beaconPos.IndexOf(",") - beaconPos.IndexOf("=") - 1));
                int beaconY = int.Parse(beaconPos.Substring(beaconPos.LastIndexOf("=") + 1));

                // Store the beacons on the specified row
                if (beaconY == checkRow && !beaconsOnRow.ContainsKey(beaconX))
                    beaconsOnRow.Add(beaconX, beaconY);

                sensors.Add(new Point(sensorX, sensorY), Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY));
            }


            // Part 1: All coordinates on the specified row that aren't beacons
            Dictionary<int, int> coveredCoordinates = new Dictionary<int, int>();

            // Go through which sensors that are in range of the specified row
            foreach (var sensor in sensors)
            {
                int distance = Math.Abs(sensor.Key.y - checkRow);
                // If the distance to the row is less than the range the sensor covers, then this sensor is relevant.
                // If they're the same, the only cover this sensor has is a beacon which means it's not relevant
                if (distance < sensor.Value)
                {
                    // This beacon covers some of the row we're checking
                    // that means the coordinate [sensor.x,checkrow] is covered as well as [range (sensor.Value) - distance] points in each direction 
                    // Add all x-numbers that are covered, but haven't been added before or are beacons
                    for (int a = (sensor.Key.x - (sensor.Value - distance)); a <= (sensor.Key.x + (sensor.Value - distance)); a++)
                    {
                        if (!coveredCoordinates.ContainsKey(a) && !beaconsOnRow.ContainsKey(a))
                        {
                            coveredCoordinates.Add(a, checkRow);
                        }
                    }

                }
            }

            answerPart1 = coveredCoordinates.Count();

            // Part 2:
            // Add all positions 1 step outside each range in a dictionary where x and y 0<=value<=4000000
            // For each sensor, see which positions in the dictionary it can cover: remove these
            // Now there should be only one
            Dictionary<Point, int> potentialBeacons = new Dictionary<Point, int>();
            foreach (var sensor in sensors)
            {
                int distanceToBorder = sensor.Value + 1; // We want to add all points with this distance from sensor
                for (int a = 0; a <= distanceToBorder; a++)
                {
                    Point p1 = new Point(sensor.Key.x + a, sensor.Key.y + distanceToBorder - a);
                    Point p2 = new Point(sensor.Key.x - a, sensor.Key.y + distanceToBorder - a);
                    Point p3 = new Point(sensor.Key.x + a, sensor.Key.y - distanceToBorder + a);
                    Point p4 = new Point(sensor.Key.x - a, sensor.Key.y - distanceToBorder + a);
                    // X and Y must be between 0 and 4,000,000
                    if (p1.x >= 0 && p1.x <= 4000000 && p1.y >= 0 && p1.y <= 4000000 && !potentialBeacons.ContainsKey(p1))
                        potentialBeacons.Add(p1, 1);
                    if (p2.x >= 0 && p2.x <= 4000000 && p2.y >= 0 && p2.y <= 4000000 && !potentialBeacons.ContainsKey(p2))
                        potentialBeacons.Add(p2, 1);
                    if (p3.x >= 0 && p3.x <= 4000000 && p3.y >= 0 && p3.y <= 4000000 && !potentialBeacons.ContainsKey(p3))
                        potentialBeacons.Add(p3, 1);
                    if (p4.x >= 0 && p4.x <= 4000000 && p4.y >= 0 && p4.y <= 4000000 && !potentialBeacons.ContainsKey(p4))
                        potentialBeacons.Add(p4, 1);
                }
            }

            // Remove all potential beacons that are covered by sensors
            foreach (var s in sensors)
            {
                foreach (var b in potentialBeacons)
                {
                    // Within range: The distance between their X + distance between Y is smaller than sensor.value which is the range.
                    if (Math.Abs(b.Key.x - s.Key.x) + Math.Abs(b.Key.y - s.Key.y) < s.Value)
                    {
                        // This coordinate is within range and can be removed
                        potentialBeacons.Remove(b.Key);
                    }
                }
            }

            // For part 2 I have to take the only position a beacon can exist and multiply it's X with 4,000,000 and add it's Y for the answer. 
            answerPart2 = potentialBeacons.ElementAt(0).Key.x * new BigInteger(4000000) + potentialBeacons.ElementAt(0).Key.y;

            System.Console.WriteLine("Answer part 1: " + answerPart1 + " and part 2: " + answerPart2);
        }
    }
}

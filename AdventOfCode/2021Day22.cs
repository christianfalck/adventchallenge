using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day22_2021
    {
        public static void calculate()
        {
            int answer1 = 0;
            long answer2 = 0; // learned the hard way that long/ulong is a lot faster than BigInteger
            Dictionary<(int, int, int), bool> cubesActive = new();
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2021day22.txt").ToArray();
            foreach (string line in lines.Take(20))
            {
                bool active = line[..2] == "on";

                var coordinates = Regex.Split(line, @"[^\d-]+");

                int minX = int.Parse(coordinates[1]);
                int maxX = int.Parse(coordinates[2]);
                int minY = int.Parse(coordinates[3]);
                int maxY = int.Parse(coordinates[4]);
                int minZ = int.Parse(coordinates[5]);
                int maxZ = int.Parse(coordinates[6]);
                for (int i = minX; i <= maxX; i++)
                    for (int j = minY; j <= maxY; j++)
                        for (int k = minZ; k <= maxZ; k++)
                            cubesActive[(i, j, k)] = active;
            }
            answer1 = cubesActive.Count(a => a.Value);

            // Part 2 
            // The "pixels" are too many, but the cubes are only 420 initially. 
            // By storing minX,minY,minZ and maxX,maxY,maxZ for all those with ON, plus the intersections for those and OFF,
            // I can calculate active = [all cubes with on] - [on intersections] - 2 * [off intersections] and
            // a little more complicated when a pixel is being turned on/off several times. 

            // All processed cubes. Using x1 for minX and x2 for maxX when dealing with stored coordinates
            // Using an int to represent the on/off since I'll be keeping all intersections. (Started with bool)
            // ON + ON = intersection should be -1 so we don't count it twice
            // ON + OFF = intersection should be -1 and then I won't store the rest of the OFF cube
            // Don't know if we have more than 2 cubes in an intersection... we'll see
            Dictionary<(int x1, int x2, int y1, int y2, int z1, int z2), int> cubes = new();
            foreach (string line in lines)
            {
                bool active = line[..2] == "on";
                var coordinates = Regex.Split(line, @"[^\d-]+");
                int minX = int.Parse(coordinates[1]);
                int maxX = int.Parse(coordinates[2]);
                int minY = int.Parse(coordinates[3]);
                int maxY = int.Parse(coordinates[4]);
                int minZ = int.Parse(coordinates[5]);
                int maxZ = int.Parse(coordinates[6]);

                // Here we add all intersections with existing cubes as well as this new cube
                Dictionary<(int x1, int x2, int y1, int y2, int z1, int z2), int> newCubes = new();

                // Check through all existing cubes for intersections
                foreach (var checkCube in cubes)
                {
                    (int minX2, int maxX2, int minY2, int maxY2, int minZ2, int maxZ2) = checkCube.Key;

                    int tmpMinX = Math.Max(minX, minX2);
                    int tmpMaxX = Math.Min(maxX, maxX2);
                    int tmpMinY = Math.Max(minY, minY2);
                    int tmpMaxY = Math.Min(maxY, maxY2);
                    int tmpMinZ = Math.Max(minZ, minZ2);
                    int tmpMaxZ = Math.Min(maxZ, maxZ2);

                    if (tmpMinX <= tmpMaxX && tmpMinY <= tmpMaxY && tmpMinZ <= tmpMaxZ)
                    {
                        // There is an intersection
                        var tmpCube = (tmpMinX, tmpMaxX, tmpMinY, tmpMaxY, tmpMinZ, tmpMaxZ);
                        // There will be existing cubes with the same key (GetValueOrDefault)
                        // invert the old value (ON + ON and ON + ON = -1)
                        newCubes[tmpCube] = newCubes.GetValueOrDefault(tmpCube, 0) - checkCube.Value;
                    }
                }
                // OFF-cubes are only interesting if they intersect with existing cubes
                if (active)
                {
                    var newCube = (minX, maxX, minY, maxY, minZ, maxZ);
                    // There will be existing cubes with the same key (GetValueOrDefault)
                    newCubes[newCube] = newCubes.GetValueOrDefault(newCube, 0) + 1;
                }
                foreach (var newCube in newCubes)
                {
                    // There will be existing cubes with the same key (GetValueOrDefault)
                    cubes[newCube.Key] = cubes.GetValueOrDefault(newCube.Key, 0) + newCube.Value;
                }
            }
            // The last 1L is because it will use Long instead of int, which would result in OverFlowException
            answer2 = cubes.Sum(a => (a.Key.x2 - a.Key.x1 + 1) * (a.Key.y2 - a.Key.y1 + 1) * (a.Key.z2 - a.Key.z1 + 1) * a.Value * 1L);
            System.Console.WriteLine("Answer: " + answer1 + " and: " + answer2);
        }
    }
}

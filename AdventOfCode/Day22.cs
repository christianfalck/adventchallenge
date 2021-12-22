using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day22
    {
        public static void calculate()
        {
            int answer1 = 0;
            BigInteger answer2 = 0;
            Dictionary<(int, int, int), bool> cubesActive = new();
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/day22.txt"))
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
                                cubesActive[(i, j, k)]= active;
            }
            answer1 = cubesActive.Count(a => a.Value);
            System.Console.WriteLine("Answer: " + answer1);
        }
    }
}

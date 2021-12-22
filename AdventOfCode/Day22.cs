using System.Collections.Generic;
using System.Linq;
using System.Numerics;


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
                string x = line.Substring(line.IndexOf('x') + 2, line.IndexOf('y') - line.IndexOf('x') - 3);
                string y = line.Substring(line.IndexOf('y') + 2, line.IndexOf('z') - line.IndexOf('y') - 3);
                string z = line.Substring(line.IndexOf('z') + 2);
                int minX = int.Parse(x.Substring(0, x.IndexOf('.')));
                int maxX = int.Parse((x.Substring(x.IndexOf('.') + 2)));
                int minY = int.Parse(y.Substring(0, y.IndexOf('.')));
                int maxY = int.Parse((y.Substring(y.IndexOf('.') + 2)));
                int minZ = int.Parse(z.Substring(0, z.IndexOf('.')));
                int maxZ = int.Parse((z.Substring(z.IndexOf('.') + 2)));
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

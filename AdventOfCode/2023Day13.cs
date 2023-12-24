using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day13_2023
    {
        public static void calculate()
        {
            List<List<string>> mirrors = System.IO.File.ReadAllText("./../../../inputfiles/2023day13.txt").
                Split("\r\n" + "\r\n").Select(x => x.Split("\r\n").ToList()).ToList();

            int answer1 = 0;
            int answer2 = 0;

            // Take one mirror at the time
            foreach (List<string> mirror in mirrors)
            {
                int part1Horizontal = FindLocationOfMirror(mirror, true);
                // Rotate the mirror and calculate to check the vertical
                List<string> rotatedMirror = Rotate90(mirror);
                int part1Vertical = FindLocationOfMirror(rotatedMirror, true);

                // For part 2 every answer of part 1 is invalid
                answer2 += FindLocationOfMirror(mirror, false, part1Horizontal) * 100;
                answer2 += FindLocationOfMirror(rotatedMirror, false, part1Vertical);

                answer1 += part1Horizontal * 100 + part1Vertical;
            }
            System.Console.WriteLine("Answer: " + answer1 + ", and " + answer2);
        }

        static bool CheckIfMatch(string row1, string row2, bool part1)
        {
            int matches = Enumerable.Range(0, row1.Length).Count(c => row1[c] == row2[c]);
            if (matches == row1.Length) return true;
            if (!part1 && matches == row1.Length - 1) return true; // since one of the signs might be the smudge
            return false;
        }

        // We need to know if it's part 1 or 2 since we need to send this info to CheckIfMatch
        // We need to know about the answer in part 1 to remove this from part 2 since it can't be a match anymore
        static int FindLocationOfMirror(List<string> mirror, bool part1, int oldFind = -1)
        {
            // Loop through all lines and check with next line until you find a match
            for (int a = 0; a < mirror.Count - 1; a++)
            {
                if (CheckIfMatch(mirror[a], mirror[a + 1], part1))
                {
                    // If this was an answer in part1, remove it from part2
                    if (!part1 && oldFind == a+1) continue;
                    // Check if it's a mirror all the way
                    int loops = Math.Min(a, mirror.Count - (a + 2));
                    bool totalMirror = true;
                    for (int b = 1; b <= loops; b++)
                    {
                        // Just continue to check until we find one discrepancy
                        if (totalMirror)
                        {
                            totalMirror = CheckIfMatch(mirror[a - b], mirror[a + b + 1], part1);
                        }
                    }
                    if (totalMirror)
                        return (a + 1);
                }
            }
            return 0;
        }

        static List<string> Rotate90(List<string> mirror)
        {
            List<string> rotatedMirror = new();
            for (int x = 0; x < mirror[0].Length; x++)
            {
                string row = "";
                for (int y = mirror.Count - 1; y >= 0; y--)
                {
                    row += mirror[y][x];
                }
                rotatedMirror.Add(row);
            }
            return rotatedMirror;
        }
    }
}

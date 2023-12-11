using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day11_2023
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day11.txt").ToArray();
            int answer1 = 0;

            // Store the galaxies in a list
            List<Galaxy> galaxies = new List<Galaxy>();
            for (int row = 0; row < lines.Length; row++)
            {
                for (int col = 0; col < lines[row].Length; col++)
                {
                    if (lines[row][col] == '#')
                    {
                        Galaxy g = new Galaxy();
                        g.x = col;
                        g.y = row;
                        galaxies.Add(g);
                    }
                }
            }

            // For each row number, if there is no galaxy in that row, add y+1 for all galaxies below
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                // loop through all galaxies and see if any of them have y = row number and if you don't find any, 
                // loop through all of them again and add +1 to y for all that has y>i
                for (int j = lines[0].Length - 1; j >= 0; j--)
                {
                    if (lines[i][j] == '#')
                    {
                        // skip this row
                        goto EndOfRow;
                    }
                }
                // if we get here, there was no galaxies in the row
                foreach (Galaxy galaxy in galaxies)
                {
                    if (galaxy.y >= i)
                        galaxy.y++;
                }
            EndOfRow:
                int what = 2; // needed due to label? 
            }
            // For each column number, if there are no galaxies in that column, add x+1 for all galaxies to the right
            for (int i = lines[0].Length - 1; i >= 0; i--)
            {
                for (int j = lines.Length - 1; j >= 0; j--)
                {
                    if (lines[j][i] == '#')
                    {
                        // skip this row
                        goto EndOfCol;
                    }
                }
                // if we get here, there was no galaxies in the column
                foreach (Galaxy galaxy in galaxies)
                {
                    if (galaxy.x >= i)
                        galaxy.x++;
                }
            EndOfCol:
                int what = 2; // needed due to label? 
            }

            // For each pair of galaxies, count the steps needed. Steps = x1-x2 + y1-y2
            for (int i = 0; i < galaxies.Count(); i++)
            {
                for (int j = i + 1; j < galaxies.Count(); j++)
                {
                    answer1 += Math.Abs(galaxies[i].x - galaxies[j].x);
                    answer1 += Math.Abs(galaxies[i].y - galaxies[j].y);
                }
            }

            System.Console.WriteLine("Answer: " + answer1);

            //Part 2

            // Don't move all galaxies. Instead, calculate the distance based on the initial map. 
            // Then split space in quadrants where empty row/column is a border.
            // Put the galaxies in the quadrants and calculate how many such steps we have to take * 1000000.

            // Keeping old part 1 in order to verify my calculations

            BigInteger answer2 = 0;
            // Store the galaxies in a list
            galaxies = new List<Galaxy>();
            for (int row = 0; row < lines.Length; row++)
            {
                for (int col = 0; col < lines[row].Length; col++)
                {
                    if (lines[row][col] == '#')
                    {
                        Galaxy g = new Galaxy();
                        g.x = col;
                        g.y = row;
                        galaxies.Add(g);
                    }
                }
            }
            List<int> xBorders = new List<int>();
            List<int> yBorders = new List<int>();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        // skip this row
                        goto EndOfRow;
                    }
                }
                // if we get here, there was no galaxies in the row
                yBorders.Add(i);
            EndOfRow:
                int what = 2; // needed due to label? 
            }
            for (int i = 0; i < lines[0].Length; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (lines[j][i] == '#')
                    {
                        // skip this row
                        goto EndOfRow;
                    }
                }
                // if we get here, there was no galaxies in the row
                xBorders.Add(i);
            EndOfRow:
                int what = 2; // needed due to label? 
            }

            // Put all galaxies in quadrants
            foreach(Galaxy g in galaxies)
            {
                foreach(int x in xBorders)
                {
                    if (x < g.x)
                        g.xQuadrant++;
                }
                foreach (int y in yBorders)
                {
                    if (y < g.y)
                        g.yQuadrant++;
                }
            }

            int rawDistance = 0;
            int warps = 0;
            for (int i = 0; i < galaxies.Count(); i++)
            {
                for (int j = i + 1; j < galaxies.Count(); j++)
                {
                    rawDistance += Math.Abs(galaxies[i].x - galaxies[j].x);
                    rawDistance += Math.Abs(galaxies[i].y - galaxies[j].y);
                    warps += Math.Abs(galaxies[i].xQuadrant - galaxies[j].xQuadrant);
                    warps += Math.Abs(galaxies[i].yQuadrant - galaxies[j].yQuadrant);
                }
            }
            // For part 1 every warp is worth 1
            answer1 = rawDistance + warps;
            // For part 2 it's 1000000, can't multiply warps with 1000000 since it's too large for int
            answer2 = warps;
            answer2 *= 999999; // replace it with a million, makes it 999999 extra steps....
            answer2 += rawDistance;

            System.Console.WriteLine("Answer: " + answer1 + ", and " + answer2);
        }
    }

    class Galaxy
    {
        public int x { get; set; }
        public int y { get; set; }
        // meaning how many empty columns are to the left
        public int xQuadrant { get; set; }
        // meaning how many empty rows are above 
        public int yQuadrant { get; set; }
    }
}

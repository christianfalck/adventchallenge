using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day13_2021
    {
        public static void calculate()
        {
            List<(int, int)> dots = new List<(int, int)>();
            bool part1done = false;
            int answerPart1 = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2021day13.txt"))
            {
                if (line == "")
                {
                    // Do nothing
                }
                else if (line[0] != 'f')
                {
                    // The coordinates
                    int first = int.Parse(line.Substring(0, line.IndexOf(",")));
                    int second = int.Parse(line.Substring(line.IndexOf(",") + 1));
                    (int x, int y) dot = (first, second);
                    dots.Add(dot);
                }
                else
                {
                    // The folding instructions
                    if (line.IndexOf("x") > 0)
                        fold(dots, int.Parse(line.Substring(line.IndexOf("=") + 1)), -1);
                    else if (line.IndexOf("y") > 0)
                        fold(dots, -1, int.Parse(line.Substring(line.IndexOf("=") + 1)));
                    if (!part1done)
                    {
                        answerPart1 = count(dots);
                        part1done = true;
                    }
                }
            }
            System.Console.WriteLine("Answer part 1: " + answerPart1);
            printAnswer2(dots);
        }

        public static void fold(List<(int, int)> dots, int foldX, int foldY)
        {
            if (foldX > 0)
            {
                //foreach((int x, int y) dot in dots)
                for (int i = 0; i < dots.Count; i++)
                {
                    (int x, int y) dot = dots[i];
                    if (dot.x > foldX)
                    {
                        dot.x = 2 * foldX - dot.x;
                        dots[i] = dot; // Otherwise this won't store the change
                    }
                        
                }
            }
            else if (foldY > 0)
            {
                for (int i = 0; i < dots.Count; i++)
                {
                    (int x, int y) dot = dots[i];
                    if (dot.y > foldY)
                    {
                        dot.y = 2 * foldY - dot.y;
                        dots[i] = dot; // Otherwise this won't store the change
                    }

                }
            }
        }

        public static int count(List<(int, int)> dots)
        {
            //remove duplicates by giving them a value = 10000 * x + y
            Dictionary<int, int> dotsInNumberFormat = new Dictionary<int, int>();
            foreach ((int x, int y) dot in dots)
            {
                int dotAsNumber = dot.x * 10000 + dot.y;
                if (dotsInNumberFormat.ContainsKey(dotAsNumber))
                    dotsInNumberFormat[dotAsNumber]++; // Another duplicate
                else
                {
                    dotsInNumberFormat.Add(dotAsNumber, 1);
                }
            }
            return dotsInNumberFormat.Keys.Count;
        }

        public static void printAnswer2(List<(int, int)> dots)
        {
            System.Console.WriteLine("Answer part 2: ");
            // First all dots with y == 0, print # for each x found, empty otherwise, move on to y == 1 etc.
            Dictionary<int, List<int>> sortedDots = new Dictionary<int, List<int>>();
            int maxX = 0;
            int maxY = 0;
            foreach ((int x, int y) dot in dots)
            {
                if (dot.x > maxX)
                    maxX = dot.x;
                if (dot.y > maxY)
                    maxY = dot.y;
                if (sortedDots.ContainsKey(dot.y))
                    sortedDots[dot.y].Add(dot.x); 
                else
                {
                    List<int> newList = new List<int>();
                    newList.Add(dot.x);
                    sortedDots[dot.y] = newList;
                }
            }
            // Now we know xmax and ymax and have a dictionary with the dots
            for(int i = 0; i <= maxY; i++)
            {
                if (!sortedDots.ContainsKey(i))
                {
                    // Should this even happen? Empty row... 
                    System.Console.WriteLine("WHAT YEAR IS IT?! WHO IS PRESIDENT!?");
                }
                else
                {
                    for (int j = 0; j <= maxX; j++)
                    {
                        if (sortedDots[i].Contains(j))
                            System.Console.Write("#");
                        else
                            System.Console.Write(" ");
                    }
                }
                System.Console.WriteLine("");
            }
        }
    }
}

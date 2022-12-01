using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day5_2021
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2021day5.txt").ToArray();
            int[,] vents = new int[1000, 1000];
            foreach (string line in lines)
            {
                string startPoint = line.Substring(0, line.IndexOf(' '));
                string endPoint = line.Substring(line.IndexOf('>') + 2);
                int x1 = int.Parse(startPoint.Substring(0, startPoint.IndexOf(',')));
                int y1 = int.Parse(startPoint.Substring(startPoint.IndexOf(',') + 1));
                int x2 = int.Parse(endPoint.Substring(0, endPoint.IndexOf(',')));
                int y2 = int.Parse(endPoint.Substring(endPoint.IndexOf(',') + 1));
                if (x1 == x2)
                {
                    if (y2 < y1)
                    {
                        int ytemp = y1;
                        y1 = y2;
                        y2 = ytemp;
                    }
                    for (int j = y1; j <= y2; j++)
                    {
                        vents[x1, j]++;
                    }
                }
                else if (y1 == y2)
                {
                    if (x2 < x1)
                    {
                        int xtemp = x1;
                        x1 = x2;
                        x2 = xtemp;
                    }
                    for (int j = x1; j <= x2; j++)
                    {
                        vents[j, y1]++;
                    }
                }
            }
            int numberOfDangerAreasPart1 = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (vents[i, j] > 1)
                        numberOfDangerAreasPart1++;
                }
            }
            //part 2
            int numberOfDangerAreasPart2 = 0;
            foreach (string line in lines)
            {
                string startPoint = line.Substring(0, line.IndexOf(' '));
                string endPoint = line.Substring(line.IndexOf('>') + 2);
                int x1 = int.Parse(startPoint.Substring(0, startPoint.IndexOf(',')));
                int y1 = int.Parse(startPoint.Substring(startPoint.IndexOf(',') + 1));
                int x2 = int.Parse(endPoint.Substring(0, endPoint.IndexOf(',')));
                int y2 = int.Parse(endPoint.Substring(endPoint.IndexOf(',') + 1));
                if (x1 != x2 && y1 != y2)
                {
                    if (y2 < y1)
                    {
                        int ytemp = y1;
                        y1 = y2;
                        y2 = ytemp;
                        int xtemp = x1;
                        x1 = x2;
                        x2 = xtemp;
                    }
                    if (x1 < x2)
                    {
                        for (int j = y1; j <= y2; j++)
                        {
                            vents[x1, j]++;
                            x1++;
                        }
                    }
                    else
                    {
                        for (int j = y1; j <= y2; j++)
                        {
                            vents[x1, j]++;
                            x1--;
                        }
                    }
                }
            }
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (vents[i, j] > 1)
                        numberOfDangerAreasPart2++;
                }
            }
            System.Console.WriteLine("Answer: " + numberOfDangerAreasPart1 + ", and " + numberOfDangerAreasPart2);
        }
    }
}

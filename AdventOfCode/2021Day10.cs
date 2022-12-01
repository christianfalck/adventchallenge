using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode
{
    class Day10_2021
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2021day10.txt").ToArray();
            int answerPart1 = 0;
            List<BigInteger> answerPart2List = new List<BigInteger>();
            char[] openingStrings = { '(', '[', '{', '<' };
            char[] closingStrings = { ')', ']', '}', '>' };
            int[] values = { 3, 57, 1197, 25137 };
            foreach (string line in lines)
            {
                List<char> chunksNotClosed = new List<char>();
                char error = '0';
                for (int i = 0; i < line.Length; i++)
                {
                    if (openingStrings.Contains(line[i]))
                    {
                        chunksNotClosed.Add(line[i]);
                    }
                    else if (closingStrings.Contains(line[i]))
                    {
                        if (chunksNotClosed.Count == 0)
                        {
                            error = line[i]; //should probably never happen: that there is an closing one with a previous open? 
                            break;
                        }
                        else if (line[i] == closingStrings[0] && chunksNotClosed.Last() == openingStrings[0])
                            chunksNotClosed.RemoveAt(chunksNotClosed.Count - 1);
                        else if (line[i] == closingStrings[1] && chunksNotClosed.Last() == openingStrings[1])
                            chunksNotClosed.RemoveAt(chunksNotClosed.Count - 1);
                        else if (line[i] == closingStrings[2] && chunksNotClosed.Last() == openingStrings[2])
                            chunksNotClosed.RemoveAt(chunksNotClosed.Count - 1);
                        else if (line[i] == closingStrings[3] && chunksNotClosed.Last() == openingStrings[3])
                            chunksNotClosed.RemoveAt(chunksNotClosed.Count - 1);
                        else
                        {
                            error = line[i];
                            break;
                        }
                    }
                }
                if (error == closingStrings[0])
                    answerPart1 += values[0];
                else if (error == closingStrings[1])
                    answerPart1 += values[1];
                else if (error == closingStrings[2])
                    answerPart1 += values[2];
                else if (error == closingStrings[3])
                    answerPart1 += values[3];

                //Part 2
                else if (chunksNotClosed.Count > 0)
                {
                    BigInteger pointsForThisLine = 0; // Realized that the answer > int
                    int[] valuesPart2 = { 1, 2, 3, 4 };
                    for (int i = chunksNotClosed.Count - 1; i >=0 ; i--)
                    {
                        if (chunksNotClosed[i] == openingStrings[0])
                            pointsForThisLine = pointsForThisLine * 5 + valuesPart2[0];
                        else if (chunksNotClosed[i] == openingStrings[1])
                            pointsForThisLine = pointsForThisLine * 5 + valuesPart2[1];
                        else if (chunksNotClosed[i] == openingStrings[2])
                            pointsForThisLine = pointsForThisLine * 5 + valuesPart2[2];
                        else if (chunksNotClosed[i] == openingStrings[3])
                            pointsForThisLine = pointsForThisLine * 5 + valuesPart2[3];
                    }
                    answerPart2List.Add(pointsForThisLine);
                }
            }
            answerPart2List.Sort();
            BigInteger answerPart2 = answerPart2List[(answerPart2List.Count - 1) / 2];
            System.Console.WriteLine("Answer part 1: " + answerPart1 + ", and part 2: " + answerPart2);

        }
    }
}

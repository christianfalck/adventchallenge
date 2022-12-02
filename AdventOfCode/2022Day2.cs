using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day2_2022
    {
        public static void calculate()
        {
            /*
             * The winner of the whole tournament is the player with the highest score. 
             * Your total score is the sum of your scores for each round. The score for 
             * a single round is the score for the shape you selected (1 for Rock, 2 for 
             * Paper, and 3 for Scissors) plus the score for the outcome of the round 
             * (0 if you lost, 3 if the round was a draw, and 6 if you won).
             * */

            //Part 1
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2022day2.txt").ToArray();
            int totalScore = 0;
            foreach(String line in lines)
            {
                string opponent = line.Substring(0, 1);
                string you = line.Substring(2, 1);
                if(opponent == "A")
                {
                    if (you == "X")
                    {
                        totalScore += 4; //draw + rock
                    }
                    else if(you == "Y")
                    {
                        totalScore += 8; //win + paper
                    }
                    else if (you == "Z")
                    {
                        totalScore += 3; //scissor
                    }
                }
                else if (opponent == "B")
                {
                    if (you == "X")
                    {
                        totalScore += 1; //rock
                    }
                    else if (you == "Y")
                    {
                        totalScore += 5; //draw + paper
                    }
                    else if (you == "Z")
                    {
                        totalScore += 9; //win + scissor
                    }
                }
                else if (opponent == "C")
                {
                    if (you == "X")
                    {
                        totalScore += 7; //win + rock
                    }
                    else if (you == "Y")
                    {
                        totalScore += 2; // paper
                    }
                    else if (you == "Z")
                    {
                        totalScore += 6; //draw + scissor
                    }
                }
            }

            /*
             * the second column says how the round needs to end: X means you need to lose, 
             * Y means you need to end the round in a draw, and Z means you need to win
             */

            //Part 2 (lazy copy/paste version)
            int totalScore2 = 0;
            foreach (String line in lines)
            {
                string opponent = line.Substring(0, 1);
                string you = line.Substring(2, 1);
                if (opponent == "A")
                {
                    if (you == "X")
                    {
                        totalScore2 += 3; //lose + scissor
                    }
                    else if (you == "Y")
                    {
                        totalScore2 += 4; //draw + rock
                    }
                    else if (you == "Z")
                    {
                        totalScore2 += 8; //win + paper
                    }
                }
                else if (opponent == "B")
                {
                    if (you == "X")
                    {
                        totalScore2 += 1; //lose + rock
                    }
                    else if (you == "Y")
                    {
                        totalScore2 += 5; //draw + paper
                    }
                    else if (you == "Z")
                    {
                        totalScore2 += 9; //win + scissor
                    }
                }
                else if (opponent == "C")
                {
                    if (you == "X")
                    {
                        totalScore2 += 2; //lose + paper
                    }
                    else if (you == "Y")
                    {
                        totalScore2 += 6; // draw + scissor
                    }
                    else if (you == "Z")
                    {
                        totalScore2 += 7; //win + rock
                    }
                }
            }

            System.Console.WriteLine("Answer part 1: " + totalScore + " and part 2: " + totalScore2);

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day6_2020
    {
        public static void calculate()
        {
            var answers = new Dictionary<char, int>();
            int numberOfDifferentAnswers = 0;
            int numberOfSameAnswers = 0;
            int familyMembers = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2020day6.txt"))
            {
                if (line != "")
                {
                    familyMembers++;
                    foreach(char c in line)
                    {
                        if (!answers.ContainsKey(c))
                            answers.Add(c, 1);
                        else
                            answers[c]++;
                    }
                }
                if(line=="")
                {
                    numberOfDifferentAnswers += answers.Count;
                    foreach (var number in answers.Values)
                    {
                        if (number == familyMembers)
                            numberOfSameAnswers++;
                    }
                    answers = new Dictionary<char, int>();
                    familyMembers = 0;
                }
            }
            // Last line isn't empty so we need to add the numbers from the last family
            numberOfDifferentAnswers += answers.Count;
            foreach (var number in answers.Values)
            {
                if (number == familyMembers)
                    numberOfSameAnswers++;
            }
            System.Console.WriteLine("Answer: " + numberOfDifferentAnswers + ", and " + numberOfSameAnswers);
        }
    }
}

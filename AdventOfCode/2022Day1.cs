using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day1_2022
    {
        public static void calculate()
        {
            int maximumCalories = 0;
            int secondMost = 0;
            int thirdMost = 0;
            int calories = 0;

            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2022day1.txt"))
            {
                if (line != "")
                {
                    calories += Int32.Parse(line);
                }
                else
                {
                    if (calories > maximumCalories)
                    {
                        maximumCalories = calories;
                    }
                    else if (calories > secondMost)
                    {
                        secondMost = calories;
                    }
                    else if(calories > thirdMost)
                    {
                        thirdMost = calories;
                    }
                    calories = 0;
                }
            }
            int answer2 = maximumCalories + secondMost + thirdMost;
            System.Console.WriteLine("Answer: " + maximumCalories + " and: " + answer2);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day2
    {
        public static void calculate()
        {
            int horizontal = 0;
            int vertical = 0;
            int horizontal_advanced = 0;
            int vertical_advanced = 0;
            int aim = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/day2.txt"))
            {
                String[] command = line.Split(" ");
                switch (command[0])
                {
                    case "forward":
                        horizontal += Int32.Parse(command[1]);
                        horizontal_advanced += Int32.Parse(command[1]);
                        vertical_advanced += (Int32.Parse(command[1]) * aim);
                        break;
                    case "down":
                        vertical += Int32.Parse(command[1]);
                        aim += Int32.Parse(command[1]);
                        break;
                    case "up":
                        vertical -= Int32.Parse(command[1]);
                        aim -= Int32.Parse(command[1]);
                        break;
                }

            }

            System.Console.WriteLine("Answer: " + horizontal*vertical + ", and " + horizontal_advanced * vertical_advanced);
        }
    }
}

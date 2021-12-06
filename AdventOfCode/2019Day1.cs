using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day1_2019
    {
        public static void calculate()
        {
            int total_fuel = 0;
            int total_recursive_fuel = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2019day1.txt"))
            {
                int weight = Int32.Parse(line);
                int fuel = (int)(weight / 3) - 2;
                total_recursive_fuel += calculateRecursiveFuel(weight);
                total_fuel += fuel;
            }

            System.Console.WriteLine("Answer: " + total_fuel + " and: " + total_recursive_fuel);
        }

        public static int calculateRecursiveFuel(int mass)
        {
            int totalfuel = 0;
            while(mass > 0)
            {
                mass = (int)(mass / 3) - 2;
                if(mass>0)
                    totalfuel += mass;
            }
            return totalfuel; 
        }
    }
}

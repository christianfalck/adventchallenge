using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode
{
    class Day7_2021
    {
        public static void calculate()
        {
            string text = File.ReadAllText("./../../../inputfiles/2021day7.txt");
            int[] crabs = Array.ConvertAll<string, int>(text.Split(','), int.Parse);

            //Part 1

            int optimalAlignedHeight = 0;
            int optimalFuelConsumption = int.MaxValue;

            for (int height = crabs.Min(); height <= crabs.Max(); height++)
            {
                int fuelconsumption = 0;
                foreach (int crab in crabs)
                {
                    fuelconsumption += Math.Abs(crab - height);
                }
                if (fuelconsumption < optimalFuelConsumption)
                {
                    optimalFuelConsumption = fuelconsumption;
                    optimalAlignedHeight = height;
                }
            }

            System.Console.WriteLine("Part1. Height: " + optimalAlignedHeight + ", requires " + optimalFuelConsumption);

            // Part 2
            // Calculating fuel per distance once for each different distance
            var fuelPerDistance = new Dictionary<int, int>();
            int fuelConsumption = 0;
            for (int distance = 0; distance <= crabs.Max(); distance++)
            {
                fuelConsumption += distance;
                fuelPerDistance.Add(distance, fuelConsumption);
            }

            optimalAlignedHeight = 0;
            optimalFuelConsumption = int.MaxValue;

            // Same as before but with the increased fuel consumption
            for (int height = crabs.Min(); height <= crabs.Max(); height++)
            {
                int fuelconsumption = 0;
                foreach (int crab in crabs)
                {
                    fuelconsumption += fuelPerDistance[Math.Abs(crab - height)];
                }
                if (fuelconsumption < optimalFuelConsumption)
                {
                    optimalFuelConsumption = fuelconsumption;
                    optimalAlignedHeight = height;
                }
            }

            System.Console.WriteLine("Part 2. Height: " + optimalAlignedHeight + ", requires " + optimalFuelConsumption);
        }
    }
}

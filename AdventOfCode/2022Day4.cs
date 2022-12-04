using System.Linq;

namespace AdventOfCode
{
    class Day4_2022
    {
        public static void calculate()
        {
            // In how many assignment pairs does one range fully contain the other?
            int numberOfOverlapsPart1 = 0;

            //In how many assignment pairs do the ranges overlap?
            int numberOfOverlapsPart2 = 0;

            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2022day4.txt"))
            {
                // Extract the numbers which is in the format 46-83,29-84 (first pair and second pair separated by ,)
                var firstPairRange = line.Split(',').First().Split('-').Select(int.Parse);
                var secondPairRange = line.Split(',').Last().Split('-').Select(int.Parse);

                // Part 1
                if (firstPairRange.First() <= secondPairRange.First() && secondPairRange.Last() <= firstPairRange.Last())
                {
                    //.2345678.  2-8
                    //..34567..  3-7
                    numberOfOverlapsPart1++;
                }
                else if (firstPairRange.First() >= secondPairRange.First() && secondPairRange.Last() >= firstPairRange.Last())
                {
                    //...45678.  3-7
                    //.2345678.  2-8
                    numberOfOverlapsPart1++;
                }

                // Part 2
                if (firstPairRange.Last() < secondPairRange.First() || firstPairRange.First() > secondPairRange.Last())
                {
                    // no overlap
                }
                else
                {
                    numberOfOverlapsPart2++;
                }
            }

            System.Console.WriteLine("Answer: " + numberOfOverlapsPart1 + " and: " + numberOfOverlapsPart2);

        }
    }
}

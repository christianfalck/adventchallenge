using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode
{
    class Day6
    {
        public static void calculate()
        {
            string text = File.ReadAllText("./../../../inputfiles/day6.txt");
            int[] numbers = Array.ConvertAll<string, int>(text.Split(','), int.Parse);

            //Part 1: just adding new fishes like they did in the example
            var fishes = new List<int>(numbers);
            int numberOfDaysPart1 = 80;
            int answerPart1 = 0;
            // for each day, calculate how many fish there are
            for (int day = 1; day <= numberOfDaysPart1; day++)
            {
                var newFishes = new List<int>();
                for (int i = 0; i < fishes.Count; i++)
                {
                    fishes[i]--;
                    if (fishes[i] < 0)
                    {
                        fishes[i] = 6;
                        newFishes.Add(8);
                    }
                }
                fishes.AddRange(newFishes);
                if (day == numberOfDaysPart1)
                {
                    answerPart1 = fishes.Count;
                }
            }
            System.Console.WriteLine("Day: " + numberOfDaysPart1 + ", there are " + answerPart1);

            // part 2:
            // Calculate how many fishes that will be created from a "1", "2", "3", "4" and "5" fish and then multiply with the number of those fishes.
            BigInteger answerPart2 = 0;
            int numberOfDaysPart2 = 256;
            BigInteger[] howManyFishesWillEachGenerate = { 1, 1, 1, 1, 1 }; // starting with those fishes themselves
            for (int fish = 1; fish <= 5; fish++) // this is the number in the input = delay
            {
                int firstBatchOfKids = (numberOfDaysPart2 - fish - 1) / 7 + 1; // how many kids the first fish will have
                int[] thisgenfishes = Enumerable.Repeat(1, firstBatchOfKids).ToArray(); // those kids in an array
                int[] nextgenfishes = new int[firstBatchOfKids]; // the array for next generation kids = grandkids
                BigInteger totalNumberOfKids = 0;
                int nextGenerationBirths; // grandkids are also born with the same interval, this is how many birthdays
                int generations = 1; // 1 = the first kids, etc. 
                // to calculate how many items of each row of the pascal triangle we should sum, we calculate how many 
                // birthdays each generation have. When a generation no longer have any birthdays, we can stop the loop. 
                // Number of birthdays = Number of days - (delay + generation * days before first birth + 1) / birth loop rounded down since we can't create a kid in 5 months + the first birth
                // Eg. (80 - (1 + 1 * 9 + 1 )) / 7 + 1 = (80 - 11) / 7 + 1 = 9 + 1 = 10 since we round down. 
                while (numberOfDaysPart2 - (fish + generations * (7 + 2) + 1)  >= 0) 
                {
                    nextGenerationBirths = (numberOfDaysPart2 - (fish + generations * (7 + 2) + 1)) / 7 + 1;
                    nextgenfishes[0] = 1; // first value always 1
                    for (int f = 1; f < nextGenerationBirths; f++)
                    {
                        nextgenfishes[f] = nextgenfishes[f - 1] + thisgenfishes[f]; // pascals triangle
                    }
                    for (int f = nextGenerationBirths; f < firstBatchOfKids; f++)
                        nextgenfishes[f] = 0; // fill out remaining fields with 0 since that's values from last generation
                    //System.Console.WriteLine("Births: " + nextGenerationBirths + ": " + string.Join(", ", thisgenfishes) + " = " + thisgenfishes.Sum());
                    totalNumberOfKids += thisgenfishes.Sum();
                    thisgenfishes = nextgenfishes.ToArray();
                    generations++;
                }
                totalNumberOfKids += thisgenfishes.Sum(); //always 1 but for good measure 
                howManyFishesWillEachGenerate[fish - 1] += totalNumberOfKids; //index 0 have fish with startvalue 1 etc. 
                //System.Console.WriteLine("Number " + fish + ": " + (totalNumberOfKids + 1));
            }
            //Count how many times the different fishes appear
            var dictionary = new Dictionary<int, int>();
            foreach (var number in numbers)
            {
                if (dictionary.ContainsKey(number))
                    dictionary[number]++;
                else
                    dictionary.Add(number, 1);
            }
            for (int i = 1; i <= 5; i++)
            {
                answerPart2 += howManyFishesWillEachGenerate[i - 1] * dictionary[i];
            }
            System.Console.WriteLine("Day: " + numberOfDaysPart2 + ", there are " + answerPart2);


            // After solving day 14 I realized this is a more effective way: 
            BigInteger answer = 0;
            int numberOfDays = 256;
            BigInteger[] fishesWithDayLeft = new BigInteger[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (int i in numbers)
                fishesWithDayLeft[i]++;
            for(int i = 0; i < numberOfDays; i++)
            {
                BigInteger tmp = fishesWithDayLeft[0];
                for (int j = 0; j < fishesWithDayLeft.Length - 1; j++)
                    fishesWithDayLeft[j] = fishesWithDayLeft[j + 1];
                fishesWithDayLeft[8] = tmp; // new born babies
                fishesWithDayLeft[6] += tmp; // parents ready for another kid
            }
            answer = fishesWithDayLeft.Aggregate(BigInteger.Add);
            System.Console.WriteLine("Bonus answer after doing day 14 challenge: " + answer);
        }
    }
}

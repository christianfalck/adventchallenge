using System;
using System.Linq;

namespace AdventOfCode
{
    class Day4_2023
    {
        public static void calculate()
        {
            int answer = 0;
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day4.txt").ToArray();

            // Create an array of int with one number per lottery ticket and with the default value 1
            // since we start with one ticket of each type
            int[] numberOfTickets = Enumerable.Repeat(1, lines.Length).ToArray();

            // This is used to store how many wins each row has
            int[] numberOfWins = new int[lines.Length];

            int answer2 = 0;

            int i = 0;
            foreach (string line in lines)
            {
                string winningList = line.Substring(0, line.IndexOf("|") - 1);
                winningList = winningList.Substring(winningList.IndexOf(":") + 2);
                string myList = line.Substring(line.IndexOf("|") + 2);

                int[] winNumbers = ParseIntValues(winningList);
                int[] myNumbers = ParseIntValues(myList);

                int rowAnswer = 0;

                foreach (int a in winNumbers)
                    foreach (int b in myNumbers)
                        if (a == b)
                        {
                            if (rowAnswer == 0)
                                rowAnswer = 1;
                            else
                                rowAnswer *= 2;
                            // We also add one lottery ticket for part 2
                            numberOfWins[i]++;
                        }
                i++;
                answer += rowAnswer;
            }

            // Part 2: Loop through all lottery rows and collect new lottery tickets
            for (int j = 0; j < numberOfWins.Length; j++)
            {
                // count the tickets we have for that row
                answer2 += numberOfTickets[j];
                // Collect new tickets
                for (int k = 1; k <= numberOfWins[j]; k++)
                {
                    // For each row below, collect new tickets * the number of tickets we have on this row
                    try
                    {
                        numberOfTickets[j + k] += numberOfTickets[j];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        // "Cards will never make you copy a card past the end of the table."
                        break;
                    }
                }
            }

            System.Console.WriteLine("Answer part 1: " + answer + " and part 2: " + answer2);
        }

        static int[] ParseIntValues(string input)
        {
            string[] valueStrings = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] values = new int[valueStrings.Length];

            for (int i = 0; i < valueStrings.Length; i++)
            {
                if (int.TryParse(valueStrings[i], out int parsedValue))
                {
                    values[i] = parsedValue;
                }
                else
                {
                    // This should not happen
                    values[i] = 0;
                }
            }

            return values;
        }
    }
}
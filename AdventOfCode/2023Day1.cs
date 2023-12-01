using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day1_2023
    {
        public static void calculate()
        {
            // part 1
            int answer = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2023day1.txt"))
            {
                // Extracting all numbers from each line
                char[] numbers = line.Where(Char.IsDigit).ToArray();

                //Adding the first instance * 10
                answer += (numbers[0]-'0') * 10;

                //Adding the last instance
                answer += (numbers[numbers.Length-1] - '0');
            }

            // part 2
            int answer2 = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2023day1.txt"))
            {
                // Find the first instance of a number
                int index = Regex.Match(line, @"\d+").Index; // where we have the first 0-9 number
                int value = line[index]-'0'; //convert from char to int
                int[] indexOfFirst = new int[9];
                indexOfFirst[0] = line.IndexOf("one");
                indexOfFirst[1] = line.IndexOf("two");
                indexOfFirst[2] = line.IndexOf("three");
                indexOfFirst[3] = line.IndexOf("four");
                indexOfFirst[4] = line.IndexOf("five");
                indexOfFirst[5] = line.IndexOf("six");
                indexOfFirst[6] = line.IndexOf("seven");
                indexOfFirst[7] = line.IndexOf("eight");
                indexOfFirst[8] = line.IndexOf("nine");
                int valueNow = 0;
                // go through each index of first instance of potential string numbers
                // and if that was before the previous first, this is the new first
                foreach(int i in indexOfFirst)
                {
                    valueNow++;
                    if(i > -1 && i < index)
                    {
                        // This was before the previous first number
                        index = i;
                        value = valueNow;
                    }
                }

                //Adding the first instance * 10
                answer2 += value * 10;

                // Find the last instance of a number
                index = line.LastIndexOfAny(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                value = line[index]-'0';
                int[] indexOfLast = new int[9];
                indexOfLast[0] = line.LastIndexOf("one");
                indexOfLast[1] = line.LastIndexOf("two");
                indexOfLast[2] = line.LastIndexOf("three");
                indexOfLast[3] = line.LastIndexOf("four");
                indexOfLast[4] = line.LastIndexOf("five");
                indexOfLast[5] = line.LastIndexOf("six");
                indexOfLast[6] = line.LastIndexOf("seven");
                indexOfLast[7] = line.LastIndexOf("eight");
                indexOfLast[8] = line.LastIndexOf("nine");
                valueNow = 0;
                // go through each index of last instance of potential string numbers
                // and if that was after the previous last, this is the new last
                foreach (int i in indexOfLast)
                {
                    valueNow++;
                    if (i > index)
                    {
                        index = i;
                        value = valueNow;
                    }
                }

                //Adding the last instance
                answer2 += value;
            }
            System.Console.WriteLine("Answer: " + answer + " and: " + answer2);
        }
    }
}

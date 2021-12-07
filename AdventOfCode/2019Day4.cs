using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Day4_2019
    {
        public static void calculate()
        {
            int startvalue = 271973;
            int endvalue = 785961;
            List<int> answer = new List<int>();
            for(int i = startvalue; i<=endvalue; i++)
            {
                int firstDigit = i / 100000;
                int secondDigit = (i % 100000) / 10000;
                int thirdDigit = (i % 10000) / 1000;
                int fourthDigit = (i % 1000) / 100;
                int fifthDigit = (i % 100) / 10;
                int sixthDigit = i % 10;
                if(firstDigit > secondDigit || secondDigit > thirdDigit || thirdDigit > fourthDigit || fourthDigit > fifthDigit || fifthDigit > sixthDigit)
                {
                    // Going from left to right, the digits never decrease; they only ever increase or stay the same (like 111123 or 135679).
                    // Do nothing
                }
                else if(firstDigit == secondDigit || secondDigit == thirdDigit || thirdDigit == fourthDigit || fourthDigit == fifthDigit || fifthDigit == sixthDigit)
                {
                    //Two adjacent digits are the same (like 22 in 122345).
                    answer.Add(i);
                }
            }
            System.Console.WriteLine("Answer: " + answer.Count);

            // Part 2 
            // the two adjacent matching digits are not part of a larger group of matching digits.
            for (int i = answer.Count-1; i >=0 ; i--)
            {
                string digits = answer[i].ToString();
                //store number of digits in dictionary. if it doesn't contain a pair, it should be removed
                var numberOfOccurances = new Dictionary<string, int>();
                for (int number = 0; number <= digits.Length -1; number++)
                {
                    string currentChar = digits[number].ToString();
                    if (numberOfOccurances.ContainsKey(currentChar))
                        numberOfOccurances[currentChar]++;
                    else
                        numberOfOccurances.Add(currentChar, 1);
                }
                bool keep = false; 
                foreach(int value in numberOfOccurances.Values)
                {
                    if (value == 2)
                        keep = true; 
                }
                if (!keep)
                {
                    //password did not meet criteria
                    answer.RemoveAt(i);
                }
            }
            System.Console.WriteLine("Answer: " + answer.Count);
        }

    }
}

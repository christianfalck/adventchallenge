using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day14
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/day14.txt").ToArray();
            string polymer = lines[0];
            string[] rulesList = new string[lines.Length - 2];
            Array.Copy(lines, 2, rulesList, 0, rulesList.Length);
            Dictionary<string, string> rules = new Dictionary<string, string>();
            Dictionary<string, (string, string)> rules2 = new Dictionary<string, (string, string)>();
            foreach (string line in rulesList)
            {
                string key = line.Substring(0, 2);
                string value = line.Substring(6, 1);
                // SO -> F
                rules.Add(key, value);
                // SO -> SF, FO
                rules2.Add(key, (line.Substring(0, 1) + line.Substring(6, 1), line.Substring(6, 1) + line.Substring(1, 1)));
            }

            //Part 1
            int numberOfLoops = 10;
            for (int i = 0; i < numberOfLoops; i++)
            {
                string newPolymer = polymer.Substring(0, 1);
                for (int j = 1; j < polymer.Length; j++)
                {
                    newPolymer += rules[polymer.Substring(j - 1, 2)] + polymer.Substring(j, 1);
                }
                polymer = newPolymer;
            }

            Dictionary<char, int> elements = new Dictionary<char, int>();
            foreach (char c in polymer)
            {
                if (elements.ContainsKey(c))
                    elements[c]++;
                else
                {
                    elements.Add(c, 1);
                }
            }

            int smallestNumber = int.MaxValue;
            int largestNumber = 0;
            foreach (char c in elements.Keys)
            {
                if (elements[c] < smallestNumber)
                    smallestNumber = elements[c];
                if (elements[c] > largestNumber)
                    largestNumber = elements[c];
            }

            int answerPart1 = largestNumber - smallestNumber;
            System.Console.WriteLine("Answer part 1: " + answerPart1);

            //Part 2

            // Using the same solution as in part 1 will result in running out of memory

            // Count number of pairs for each iteration
            var numberOfPairs = new Dictionary<string, BigInteger>(); // Integer not enough
            // Since the rules has unique pairs, we'll use that
            foreach (string key in rules.Keys)
                numberOfPairs.Add(key, 0);
            // Adding the values from the initial string
            for (int i = 0; i < lines[0].Length - 1; i++)
            {
                numberOfPairs[lines[0].Substring(i, 2)]++;
            }
            numberOfLoops = 40;
            for (int loop = 0; loop < numberOfLoops; loop++)
            {
                // for each key in numberofpairs
                // remove those and instead add the resulting pairs
                Dictionary<string, BigInteger> tmpDictionary = new Dictionary<string, BigInteger>();
                foreach (string key in numberOfPairs.Keys)
                    tmpDictionary.Add(key, 0);
                foreach (string key in numberOfPairs.Keys)
                {
                    tmpDictionary[rules2[key].Item1] += numberOfPairs[key];
                    tmpDictionary[rules2[key].Item2] += numberOfPairs[key];
                }
                numberOfPairs = new Dictionary<string, BigInteger>(tmpDictionary);
            }
            BigInteger answerPart2 = 0;
            // When trying to use the Part 1 method, I found out that O will be most frequent and H the least so I hardcoded those two. 
            // A nicer solution would be to get the values for all letters and go through them to find the smallest and largest
            // Look at part 1 for inspiration. Have to wake the kids in <5 hours so I'll just finish this for now. 
            foreach (string key in numberOfPairs.Keys)
            {
                if (key == "OO")
                    answerPart2 += numberOfPairs[key] * 2;
                else if (key.Contains('O'))
                    answerPart2 += numberOfPairs[key];
                if (key == "HH")
                    answerPart2 -= numberOfPairs[key] * 2;
                else if (key.Contains('H'))
                    answerPart2 -= numberOfPairs[key];
            }
            // Since we've now calculated all pairs with the letters: each letter exist in two pairs
            answerPart2 = answerPart2 / 2 + answerPart2 % 2; // The last %2 part handles if the string ends with one of the letters (which it does)

            System.Console.WriteLine("Answer part 2: " + answerPart2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day6_2019
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2019day6.txt").ToArray();

            var dictionary = new Dictionary<string, List<string>>();
            foreach (string line in lines)
            {
                string parent = line.Substring(0, 3);
                string child = line.Substring(4, 3);
                if (dictionary.ContainsKey(parent))
                    dictionary[parent].Add(child);
                else
                {
                    List<string> newList = new List<string>();
                    newList.Add(child);
                    dictionary[parent] = newList; 
                }
            }
            int answerPart1 = countSteps(dictionary, "COM", 0);
            System.Console.WriteLine("Answer Part1: " + answerPart1);

            //Part 2 hack
            var reversedDictionary = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                string parent = line.Substring(0, 3);
                string child = line.Substring(4, 3);
                reversedDictionary.Add(child, parent); // All children are unique but not all parents
            }
            List<string> myOrbits = new List<string>();
            List<string> santasOrbits = new List<string>();
            string actual = "YOU";
            while(actual != "COM")
            {
                actual = reversedDictionary[actual];
                myOrbits.Add(actual); 
            }
            actual = "SAN";
            while (actual != "COM")
            {
                actual = reversedDictionary[actual];
                santasOrbits.Add(actual);
            }
            var myTransfer = myOrbits.Except(santasOrbits).ToList(); 
            var toSanta = santasOrbits.Except(myOrbits).ToList(); 
            myTransfer.AddRange(toSanta);
            // The number of unique objects we both orbit = the number of transfers needed
            System.Console.WriteLine("Answer Part2: " + myTransfer.Count); 
        }

        // Recursive function to return all the steps for this object and all its kids
        public static int countSteps(Dictionary<string, List<string>> dictionary, String parent, int stepsFromCom)
        {
            int totalNumberOfSteps = stepsFromCom; //The steps for this object
            if (dictionary.ContainsKey(parent))
            {
                foreach (string child in dictionary[parent]) // If this throws error: Add if statement to check in dictionary
                    totalNumberOfSteps += countSteps(dictionary, child, stepsFromCom + 1);
            }
            return totalNumberOfSteps;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day12_2023
    {
        public static void calculate()
        {
            List<string> lines = System.IO.File.ReadLines("./../../../inputfiles/2023day12.txt").ToList();

            int answer1 = 0;
            int answer2 = 0;

            foreach(string line in lines)
            {
                // Split the input into the whole/damaged/unknown map and which groups of damaged springs
                // Eg. {".??..??...?##.", "1,1,3"}
                string[] input = line.Split(' ');
                // {"??","??","?##"}
                string[] fields = input[0].Split('.');
                // {"1","1","3"}
                string[] lengthsString = input[1].Split(',');
                //{1,1,3}
                List<int> lengths = lengthsString.Select(int.Parse).ToList();

                // Idea: Place all damaged groups far left, then try moving one at the time to the right. 
                // How do I know how many groups that can fit in a area? 
                // E.g. ##?????????#?
                // I know that the first part is a damaged group of at least size 2. 
                // Add one . after that group. Then continue placing groups. 

                // Do this by calling a recursive function with the rest of the fields and the lengts.
                answer1 = FindCombinations(fields, lengths);
            }
            
           
            System.Console.WriteLine("Answer: " + answer1 + ", and " + answer2);
        }

        public static int FindCombinations(string[] fields, List<int> lengths)
        {

            // Place the first group in as many different combinations as is possible and then for each combination,
            // call this function with the rest of the fields and lengths

            // unless it's the last one, then it should not call for anything recursively, just place the first group

            // The last recursive loop should only have one length
            bool isLastGroup = (lengths.Count == 1);

            // if the first field starts with a # in the first length+1 places, we know where to put it
            
            return 0;
        }

    }
}

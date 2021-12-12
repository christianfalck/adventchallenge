using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day12
    {
        public static void calculate()
        {
            List<int> answerPart1 = new List<int>();
            Dictionary<string, List<string>> paths = new Dictionary<string, List<string>>();
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/day12.txt"))
            {
                string first = line.Substring(0, line.IndexOf("-"));
                string second = line.Substring(line.IndexOf("-") + 1);
                // Creating a dictionary containing the path both ways in order to make it easier in the recursive call
                if (paths.ContainsKey(first))
                    paths[first].Add(second);
                else
                {
                    List<string> newList = new List<string>();
                    newList.Add(second);
                    paths[first] = newList;
                }
                if (paths.ContainsKey(second))
                    paths[second].Add(first);
                else
                {
                    List<string> newList = new List<string>();
                    newList.Add(first);
                    paths[second] = newList;
                }
            }
            calculateNextStep(paths, "start", new List<string>(), answerPart1);
            System.Console.WriteLine("Answer part 1: " + answerPart1.Count);

            //Part 2 - pretty much the same idea but optimized since the first solution takes too long
            // * Rename all caves to numbers
            Dictionary<int, List<int>> pathsOptimized = new Dictionary<int, List<int>>();
            int[] answerPart2 = { 0 };
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/day12.txt"))
            {
                string first = line.Substring(0, line.IndexOf("-"));
                string second = line.Substring(line.IndexOf("-") + 1);
                // Large gaves has index 10+
                string[] caves = { "start", "dc", "ho", "uh", "xc", "yf", "qo", "end", "", "", "NF", "EM", "TP", "MY" };
                //string[] caves = { "start", "fs", "he", "pj", "zg", "sl", "", "end", "", "", "DX","RW","WI"};
                int firstOptimized = Array.IndexOf(caves, first);
                int secondOptimized = Array.IndexOf(caves, second);
                // Creating a dictionary containing the path both ways in order to make it easier in the recursive call
                if (pathsOptimized.ContainsKey(firstOptimized))
                    pathsOptimized[firstOptimized].Add(secondOptimized);
                else
                {
                    List<int> newList = new List<int>();
                    newList.Add(secondOptimized);
                    pathsOptimized[firstOptimized] = newList;
                }
                if (pathsOptimized.ContainsKey(secondOptimized))
                    pathsOptimized[secondOptimized].Add(firstOptimized);
                else
                {
                    List<int> newList = new List<int>();
                    newList.Add(firstOptimized);
                    pathsOptimized[secondOptimized] = newList;
                }
            }
            calculateNextStepOptimized(pathsOptimized, 0, new List<int>(), answerPart2,false);
            System.Console.WriteLine("Answer part 2: " + answerPart2[0]);
        }

        // Recursive function to find all paths
        public static void calculateNextStep(Dictionary<string, List<string>> paths, String thisStep, List<string> pathUntilThis, List<int> answerPart1)
        {
            List<string> pathWithThis = new List<string>(pathUntilThis); //Cloning in order to not reuse the same path
            pathWithThis.Add(thisStep); // Adding the current cave
            foreach (string nextStep in paths[thisStep])
            {
                if (nextStep == "end")
                {
                    //System.Console.WriteLine("Path: " + string.Join(">", pathWithThis));
                    answerPart1.Add(pathWithThis.Count);
                }
                else if (nextStep == "start")
                {
                    //Do nothing. We can't go back to start. 
                }
                else if (!(Char.IsLower(nextStep[0]) && pathWithThis.Contains(nextStep)))
                {
                    // Only reason for not moving on is if the next cave is in small letters and already visited
                    calculateNextStep(paths, nextStep, pathWithThis, answerPart1);
                }
            }
        }

        // Recursive function to find all paths for Part 2
        public static void calculateNextStepOptimized(Dictionary<int, List<int>> paths, int thisStep, List<int> pathUntilThis, int[] answerPart2, bool smallCaveHaveBeenVisitedTwice)
        {
            List<int> pathWithThis = new List<int>(pathUntilThis); //Cloning in order to not reuse the same path
            pathWithThis.Add(thisStep); // Adding the current cave
            foreach (int nextStep in paths[thisStep])
            {
                if (nextStep == 7) //End
                {
                    //System.Console.WriteLine("Path: " + string.Join(">", pathWithThis));
                    answerPart2[0]++;
                }
                else if (nextStep == 0) //Start
                {
                    //Do nothing. We can't go back to start. 
                }
                else if (nextStep > 9)
                {
                    // Next cave is big
                    calculateNextStepOptimized(paths, nextStep, pathWithThis, answerPart2, smallCaveHaveBeenVisitedTwice);
                }
                else if (nextStep < 10 && pathUntilThis.Where(temp => temp.Equals(nextStep)).Select(temp => temp).Count() == 0)
                {
                    // next cave is a small but haven't been visited before
                    calculateNextStepOptimized(paths, nextStep, pathWithThis, answerPart2, smallCaveHaveBeenVisitedTwice);
                }
                else if (nextStep < 10 && pathUntilThis.Where(temp => temp.Equals(nextStep)).Select(temp => temp).Count() == 1 && !smallCaveHaveBeenVisitedTwice)
                {
                    // next cave is small and have been visited once and no other small caves have been visited more than once
                    calculateNextStepOptimized(paths, nextStep, pathWithThis, answerPart2, true);
                }
            }
        }
    }
}

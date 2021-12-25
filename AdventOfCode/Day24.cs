using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day24
    {
        // I had input from https://github.com/Bpendragon to rewrite this into a better solution

        // The input is the same iteration of 18 steps, repeated 14 times with only 3 differenses
        int[] zDiv = new int[14]; // z is divided by either 1 or 26 at row 5
        int[] xAdd = new int[14]; // the different values added to X at row 6
        int[] yAdd = new int[14]; // the different values added to Y at row 16

        List<long> MaxZinIteration = new(); // How large z potentially can be in each iteration since we divide by 26 but not always

        Dictionary<(int iteration, long z), List<string>> potentialInputs = new(); // TODO better name of dictionary

        readonly string[] lines = System.IO.File.ReadLines("./../../../inputfiles/day24.txt").ToArray();

        public void calculate()
        {
            // Getting the different values for the different iterations
            for (int i = 0; i < 14; i++)
            {
                zDiv[i] = int.Parse(lines[(18 * i) + 4].Split(" ")[2]);
                xAdd[i] = int.Parse(lines[(18 * i) + 5].Split(" ")[2]);
                yAdd[i] = int.Parse(lines[(18 * i) + 15].Split(" ")[2]);
            }

            for (int i = 0; i < zDiv.Length; i++)
            {
                // Create a list of the max values of z based on in which iterations it's being divided by 26
                MaxZinIteration.Add(zDiv.Skip(i).Aggregate(1L, (a, b) => a * b));
            }

            List<string> ValidInputNumbers = recursiveStepwiseIteration(0,0);
            ValidInputNumbers.Sort();

            System.Console.WriteLine("Answer: " + ValidInputNumbers.Last() + ", and " + ValidInputNumbers.First());
        }

        // Go through the algorithm one step at the time
        public List<string> recursiveStepwiseIteration(int iteration, long z)
        {
            // We don't have to calculate the same numbers several times
            if(potentialInputs.ContainsKey((iteration, z))) 
                return potentialInputs[(iteration, z)];
            // If z = 0 after 14 iterations, we have found a correct input
            if (iteration >= 14)
            {
                if (z == 0)
                {
                    return new() { "" }; // when we go back recursively, the input string will be built from this
                }
                else
                {
                    return null; // not a valid input
                }
            }

            // Z will never be able to reach 0
            if (z > MaxZinIteration[iteration])
            {
                return null;
            }

            // building up the input one number at the time
            List<string> buildInputString = new();
            long inputToCheck = xAdd[iteration] + z % 26;
            List<string> inputStringFromNextToEnd;
            if (0 < inputToCheck && inputToCheck < 10)
            {
                long nextZ = calculateNextZ(iteration, z, inputToCheck);
                inputStringFromNextToEnd = recursiveStepwiseIteration(iteration + 1, nextZ);
                // If we have valid inputs from end backwards to the current iteration, add current input
                if (null != inputStringFromNextToEnd)
                {
                    foreach (var partOfInputString in inputStringFromNextToEnd)
                    {
                        buildInputString.Add($"{inputToCheck}{partOfInputString}");
                    }
                }
            }
            else
            {
                foreach (int tryingAllPotentialInputNumbers in Enumerable.Range(1, 9))
                {
                    long nextZ = calculateNextZ(iteration, z, tryingAllPotentialInputNumbers);
                    inputStringFromNextToEnd = recursiveStepwiseIteration(iteration + 1, nextZ);
                    // If we have valid inputs from end backwards to the current iteration, add current input
                    if (null != inputStringFromNextToEnd)
                    {
                        foreach (var partOfInputString in inputStringFromNextToEnd)
                        {
                            buildInputString.Add($"{tryingAllPotentialInputNumbers}{partOfInputString}");
                        }
                    }
                }
            }
            potentialInputs[(iteration, z)] = buildInputString;
            return buildInputString;
        }

        // if inputValue == (z / [1 or 26]) % 26 + xAdd => z = z / zDiv, else z = 26 * z / zDiv + inputValue + yAdd
        public long calculateNextZ(int iteration, long z, long inputValue)
        {
            if (inputValue == z % 26 + xAdd[iteration])
            {
                z = z / zDiv[iteration];
            }
            else
            {
                z = 26 * z / zDiv[iteration] + inputValue + yAdd[iteration];
            }
            return z;
        }
    }
}

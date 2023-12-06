using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day5_2023
    {
        public static void calculate()
        {

            BigInteger answer = 0;
            BigInteger answer2 = 0;

            // First is the initial list of seeds we're translating. Second is the output. 
            // During seed-to-soil, we'll populate the output and when we reach an empty row, we move those to input. 
            // And do the same thing. 
            List<string> lines = System.IO.File.ReadLines("./../../../inputfiles/2023day5.txt").ToList();
            // Parse the row with the seeds
            MatchCollection matches = Regex.Matches(lines.First(), @"\d+");
            BigInteger[] input = new BigInteger[matches.Count];
            BigInteger[] output = new BigInteger[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                input[i] = BigInteger.Parse(matches[i].Value);
                output[i] = -1; // to identify that it hasn't been translated
            }
            // Remove the first two rows since it's already handled
            lines.RemoveAt(0);
            lines.RemoveAt(0);
            foreach (string line in lines)
            {
                // Identify when we have a translation row
                matches = Regex.Matches(line, @"\d+");
                if (matches.Count > 0)
                {
                    // This is a translation row with output input range
                    // Identify if any of the inputs are in the range of this rows inputs
                    BigInteger outStart = BigInteger.Parse(matches[0].Value);
                    BigInteger inStart = BigInteger.Parse(matches[1].Value);
                    BigInteger range = BigInteger.Parse(matches[2].Value);
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (input[i] >= inStart && input[i] < (inStart + range))
                        {
                            // inStart -> outStart
                            // since we've moved input - inStart steps, you have to add this to outStart
                            output[i] = outStart + (input[i] - inStart);
                        }
                    }
                    int test = 0;
                }
                if (line == "")
                {
                    // Move output to input and start next translation
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (output[i] > -1)
                        {
                            // only need to move those who had a translation
                            input[i] = output[i];
                        }
                    }

                }
            }
            answer = output[0];
            // Find the lowest output
            for (int i = 0; i < output.Length; i++)
            {
                if (answer > output[i])
                {
                    answer = output[i];
                }
            }

            System.Console.WriteLine("Answer: " + answer + ", and " + answer2);
        }
    }
}


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

            // Part 2: 
            // Use ranges instead. 
            // 3489262449 + 222250568 is the first range
            // find any translation that handles the first value and fetch the range within the translation. 
            // 3483784779 + 43058912 will cover some of this and translate into 4251908384 + 37581242
            // where 37581242 = 43058912 - (3489262449-3483784779)
            // The remaining range 3526843691 + 184669326 will be partly covered by 3526843691 + 39775874
            // Which will translate into 3583007889 + 39775874
            // The remaining 3566619565 + 144893452 will be partly covered by 3566619565 + 111511702
            // Translating into 3949194199 + 111511702
            // The remaining 3678131267 + 33381750 will be partly covered by 3678131267 + 33029785
            // Translating into 3549978104 + 33029785
            // The remaining  3711161052 + 351965 will be covered by 3711161052 + 116605441
            // Translating into 1199516138 + 351965
            // ------------
            // So the first range 3489262449 + 222250568 will be translated seed-to-soil into 5 new ranges
            // 4251908384 + 37581242
            // 3583007889 + 39775874
            // 3949194199 + 111511702
            // 3549978104 + 33029785
            // 1199516138 + 351965
            // ------------
            // There are 10 ranges from start. Following about the same pattern as above, they will split into 10 * 5^8 ranges
            // which means 3906250 ranges. This number is so big that it might be required to find ranges that border eachother
            // and merge them

            System.Console.WriteLine("Answer: " + answer + ", and " + answer2);
        }
    }
}


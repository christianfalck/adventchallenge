using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day6_2022
    {
        public static void calculate()
        {
            string input = File.ReadAllText("./../../../inputfiles/2022day6.txt");

            int answerpart1 = 0;
            int answerpart2 = 0;

            int loop = 0;
            //loop until we find a string with unique characters
            while (input.Length > 0)
            {

                //Part 1 we should look at the last 4 characters, for part 2 it's 14 characters
                string part1String = input[..4]; // same as substring(0,4)
                string part2String = input[..14];

                //compare how many that are distinct, if it's all, we're done. Also a check that this is done only once. 
                if (answerpart1 == 0 && part1String.Distinct().Count() == part1String.Length)
                {
                    answerpart1 = loop + part1String.Length; // loop is 0 when we're looking at the first Length (4) characters. 
                }

                //compare how many that are distinct, if it's all, we're done. We've looked through loop + [the length of the message] characters which is the answer. 
                if (answerpart2 == 0 && part2String.Distinct().Count() == part2String.Length)
                {
                    answerpart2 = loop + part2String.Length;
                    break; // when we have 14 distinct, we definately have 4. Safe to take a break. 
                }

                input = input[1..]; //one step forward
                loop++;
            }

            System.Console.WriteLine("Answer part 1: " + answerpart1 + ", and part 2: " + answerpart2);
        }
    }
}

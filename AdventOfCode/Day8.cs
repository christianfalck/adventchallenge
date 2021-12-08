using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day8
    {
        public static void calculate()
        {
            int answerPart1 = 0;
            int answerPart2 = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/day8.txt"))
            {
                string input = line.Substring(0, line.IndexOf("|") - 1);
                string output = line.Substring(line.IndexOf("|") + 2);


                String[] inputCommands = input.Split(" ");
                String[] outputCommands = output.Split(" ");

                foreach (string outputCommand in outputCommands)
                {
                    // 1 has length 2, 7 has length 3, 4 has length 4 and 8 length 7 which are the unique numbers
                    if (outputCommand.Length < 5 || outputCommand.Length == 7)
                        answerPart1++;
                }

                //Part 2
                // I'll deduce one number at the time and it will look ugly codewise but will be easier to follow (I hope)
                string[] numberCodes = { "", "", "", "", "", "", "", "", "", "" };
                int numberOfNumbersFound = 0;
                while (numberOfNumbersFound < 10)
                {
                    foreach (string inputCommand in inputCommands)
                    {
                        // 1 has length 2, 7 has length 3, 4 has length 4 and 8 length 7 which are the unique numbers
                        if (inputCommand.Length == 2 && numberCodes[1] == "")
                        {
                            numberCodes[1] = inputCommand;
                            numberOfNumbersFound++;
                        }
                        else if (inputCommand.Length == 3 && numberCodes[7] == "")
                        {
                            numberCodes[7] = inputCommand;
                            numberOfNumbersFound++;
                        }
                        else if (inputCommand.Length == 4 && numberCodes[4] == "")
                        {
                            numberCodes[4] = inputCommand;
                            numberOfNumbersFound++;
                        }
                        else if (inputCommand.Length == 7 && numberCodes[8] == "")
                        {
                            numberCodes[8] = inputCommand;
                            numberOfNumbersFound++;
                        }
                        // 6 has length 6 and doesn't contain the parts from 1
                        else if (inputCommand.Length == 6 && (inputCommand.Except(numberCodes[1])).Count() == 5 && numberCodes[6] == "")
                        {
                            numberCodes[6] = inputCommand;
                            numberOfNumbersFound++;
                        }
                        // 3 has length 5 and contains all parts from 1
                        else if (inputCommand.Length == 5 && (inputCommand.Except(numberCodes[1])).Count() == 3 && numberCodes[3] == "")
                        {
                            numberCodes[3] = inputCommand;
                            numberOfNumbersFound++;
                        }
                        // 9 has length 6 and contains all parts from 4
                        else if (inputCommand.Length == 6 && (inputCommand.Except(numberCodes[4])).Count() == 2 && numberCodes[9] == "")
                        {
                            numberCodes[9] = inputCommand;
                            numberOfNumbersFound++;
                        }
                        // 0 is the remaining one with length 6
                        else if (inputCommand.Length == 6 && numberCodes[9] != "" && (inputCommand.Except(numberCodes[9])).Count() > 0 && numberCodes[6] != "" && (inputCommand.Except(numberCodes[6])).Count() > 0 && numberCodes[0] == "")
                        {
                            numberCodes[0] = inputCommand;
                            numberOfNumbersFound++;
                        }
                        // 5 is the only one with length 5 that can fit in 6
                        else if (inputCommand.Length == 5 && numberCodes[6] != "" && (numberCodes[6].Except(inputCommand)).Count() == 1 && numberCodes[5] == "")
                        {
                            numberCodes[5] = inputCommand;
                            numberOfNumbersFound++;
                        }
                        // 2 has length 5 and differs 2 from 5
                        else if (inputCommand.Length == 5 && numberCodes[5] != "" && (numberCodes[5].Except(inputCommand)).Count() == 2 && numberCodes[2] == "")
                        {
                            numberCodes[2] = inputCommand;
                            numberOfNumbersFound++;
                        }
                    }
                }
                // Now we'll decode the output
                int outputvalue = 0;
                int multiplier = 1;
                foreach(string outputCommand in outputCommands.Reverse()) // reverse due to the multiplier part
                {
                    for(int i = 0; i < 10; i++)
                    {
                        if(numberCodes[i].Except(outputCommand).Count() == 0 && outputCommand.Except(numberCodes[i]).Count() == 0)
                        {
                            outputvalue += (i * multiplier);
                            multiplier *= 10;
                        }
                    }
                }
                answerPart2 += outputvalue;
            }

            System.Console.WriteLine("Answer: " + answerPart1 + ", and " + answerPart2);
        }
    }
}

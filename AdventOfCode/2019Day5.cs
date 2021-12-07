using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Day5_2019
    {
        public static void calculate()
        {
            string text = File.ReadAllText("./../../../inputfiles/2019day5.txt");
            int[] commands = Array.ConvertAll<string, int>(text.Split(','), int.Parse);
            int commandAt = 0; //where we're currently executing
            while (commandAt < commands.Length)
            {
                int instruction = commands[commandAt] % 100;
                int firstParamMode = (commands[commandAt] / 100) % 10;
                int secondParamMode = (commands[commandAt] / 1000) % 10;
                int thirdParamMode = (commands[commandAt] / 10000) % 10;
                if (instruction == 1)
                {
                    //Addition
                    int firstParam = (firstParamMode == 0) ? commands[commandAt + 1] : commandAt + 1;
                    int secondParam = (secondParamMode == 0) ? commands[commandAt + 2] : commandAt + 2;
                    int thirdParam = (thirdParamMode == 0) ? commands[commandAt + 3] : commandAt + 3;
                    if(thirdParamMode != 0)
                        System.Console.WriteLine("ERROR! Parameters that an instruction writes to will never be in immediate mode.");
                    commands[thirdParam] = commands[firstParam] + commands[secondParam];
                    commandAt += 4;
                }
                else if (instruction == 2)
                {
                    //Multiplication
                    int firstParam = (firstParamMode == 0) ? commands[commandAt + 1] : commandAt + 1;
                    int secondParam = (secondParamMode == 0) ? commands[commandAt + 2] : commandAt + 2;
                    int thirdParam = (thirdParamMode == 0) ? commands[commandAt + 3] : commandAt + 3;
                    if (thirdParamMode != 0)
                        System.Console.WriteLine("ERROR! Parameters that an instruction writes to will never be in immediate mode.");
                    commands[thirdParam] = commands[firstParam] * commands[secondParam];
                    commandAt += 4;
                }
                else if(instruction == 3)
                {
                    //Input
                    Console.Write("Please identify yourself (1 for part 1 and 5 for part 2): ");
                    string input = Console.ReadLine();
                    int firstParam = (firstParamMode == 0) ? commands[commandAt + 1] : commandAt + 1;
                    commands[firstParam] = int.Parse(input); // Will give exception if user writes !number
                    commandAt += 2;
                }
                else if (instruction == 4)
                {
                    //Output
                    Console.WriteLine(commands[commands[commandAt + 1]]);
                    commandAt += 2;
                }
                else if (instruction == 5)
                {
                    //jump-if-true
                    int firstParam = (firstParamMode == 0) ? commands[commandAt + 1] : commandAt + 1;
                    int secondParam = (secondParamMode == 0) ? commands[commandAt + 2] : commandAt + 2;
                    if (commands[firstParam] == 0)
                        commandAt += 3;
                    else
                        commandAt = commands[secondParam]; 
                }
                else if (instruction == 6)
                {
                    //jump-if-false
                    int firstParam = (firstParamMode == 0) ? commands[commandAt + 1] : commandAt + 1;
                    int secondParam = (secondParamMode == 0) ? commands[commandAt + 2] : commandAt + 2;
                    if (commands[firstParam] == 0)
                        commandAt = commands[secondParam];
                    else
                        commandAt += 3;
                }
                else if (instruction == 7)
                {
                    //less than
                    int firstParam = (firstParamMode == 0) ? commands[commandAt + 1] : commandAt + 1;
                    int secondParam = (secondParamMode == 0) ? commands[commandAt + 2] : commandAt + 2;
                    int thirdParam = (thirdParamMode == 0) ? commands[commandAt + 3] : commandAt + 3;
                    if (commands[firstParam] < commands[secondParam])
                        commands[thirdParam] = 1;
                    else
                        commands[thirdParam] = 0;
                    if (thirdParamMode != 0)
                        System.Console.WriteLine("ERROR! Parameters that an instruction writes to will never be in immediate mode.");
                    commandAt += 4;
                }
                else if (instruction == 8)
                {
                    //equals
                    int firstParam = (firstParamMode == 0) ? commands[commandAt + 1] : commandAt + 1;
                    int secondParam = (secondParamMode == 0) ? commands[commandAt + 2] : commandAt + 2;
                    int thirdParam = (thirdParamMode == 0) ? commands[commandAt + 3] : commandAt + 3;
                    if (commands[firstParam] == commands[secondParam])
                        commands[thirdParam] = 1;
                    else
                        commands[thirdParam] = 0;
                    if (thirdParamMode != 0)
                        System.Console.WriteLine("ERROR! Parameters that an instruction writes to will never be in immediate mode.");
                    commandAt += 4;
                }
                else if (instruction == 99)
                {
                    //End program
                    break;
                }
                else
                {
                    System.Console.WriteLine("Error, something went wrong");
                    break;
                }
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Day2_2019
    {
        public static void calculate()
        {
            int noun = 0, verb = 0;
            for (noun = 0; noun < 100; noun++)
            {
                for (verb = 0; verb < 100; verb++)
                {
                    if (runProgram(noun, verb) == 19690720)
                        goto Answer;
                }
            }
            Answer:
                System.Console.WriteLine("Answer: " + runProgram(12,2) + " and: " + (100*noun+verb));
        }

        public static int runProgram(int noun, int verb)
        {
            string text = File.ReadAllText("./../../../inputfiles/2019day2.txt");
            int[] commands = Array.ConvertAll<string, int>(text.Split(','), int.Parse);
            commands[1] = noun;
            commands[2] = verb;
            int commandAt = 0; //where we're currently executing
            while (commandAt < commands.Length)
            {
                if (commands[commandAt] == 1)
                {
                    //Addition
                    commands[commands[commandAt + 3]] = commands[commands[commandAt + 1]] + commands[commands[commandAt + 2]];
                }
                else if (commands[commandAt] == 2)
                {
                    //Multiplication
                    commands[commands[commandAt + 3]] = commands[commands[commandAt + 1]] * commands[commands[commandAt + 2]];
                }
                else if (commands[commandAt] == 99)
                {
                    //End program
                    break;
                }
                else
                {
                    System.Console.WriteLine("Error, something went wrong");
                    break;
                }
                commandAt += 4;
            }
            return commands[0];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day24
    {
        public static void calculate()
        {
            List<string> variableNames = new List<string> { "w", "x", "y", "z" };
            bool found = false;
            long modelNumber = 99999999999999;
            while (!found)
            {
                modelNumber -= 1;
                string modelNumberAsString = modelNumber + "";
                int modelNumberAsStringIndex = 0;
                int[] variables = { 0, 0, 0, 0 };
                foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/day24.txt"))
                {
                    String[] command = line.Split(" ");
                    switch (command[0])
                    {
                        case "inp": // It's always w, have programmed it more general though
                            int index = variableNames.FindIndex(a => a.Contains(command[1]));
                            int inputNumber = modelNumberAsString[modelNumberAsStringIndex++];
                            variables[index] = inputNumber;
                            break;
                        case "add":
                            index = variableNames.FindIndex(a => a.Contains(command[1]));
                            if (int.TryParse(command[2], out int n))
                            {
                                // It's a number
                                variables[index] += int.Parse(command[2]);
                            }
                            else
                            {
                                int tmpIndex = variableNames.FindIndex(a => a.Contains(command[2]));
                                variables[index] += variables[tmpIndex];
                            }
                            break;
                        case "mul":
                            index = variableNames.FindIndex(a => a.Contains(command[1]));
                            if (int.TryParse(command[2], out n))
                            {
                                // It's a number
                                variables[index] *= int.Parse(command[2]);
                            }
                            else
                            {
                                int tmpIndex = variableNames.FindIndex(a => a.Contains(command[2]));
                                variables[index] *= variables[tmpIndex];
                            }
                            break;
                        case "div":
                            index = variableNames.FindIndex(a => a.Contains(command[1]));
                            if (int.TryParse(command[2], out n))
                            {
                                // It's a number
                                variables[index] /= int.Parse(command[2]);
                            }
                            else
                            {
                                int tmpIndex = variableNames.FindIndex(a => a.Contains(command[2]));
                                variables[index] /= variables[tmpIndex];
                            }
                            break;
                        case "mod": // It's always mod 26 but I'll implement it more general
                            index = variableNames.FindIndex(a => a.Contains(command[1]));
                            if (int.TryParse(command[2], out n))
                            {
                                // It's a number
                                variables[index] %= int.Parse(command[2]);
                            }
                            else
                            {
                                int tmpIndex = variableNames.FindIndex(a => a.Contains(command[2]));
                                variables[index] %= variables[tmpIndex];
                            }
                            break;
                        case "eql":
                            index = variableNames.FindIndex(a => a.Contains(command[1]));
                            if (int.TryParse(command[2], out n))
                            {
                                // It's a number
                                variables[index] = variables[index] == int.Parse(command[2]) ? 1 : 0;
                            }
                            else
                            {
                                int tmpIndex = variableNames.FindIndex(a => a.Contains(command[2]));
                                variables[index] = variables[index] == variables[tmpIndex] ? 1 : 0;
                            }
                            break;
                    }
                }
                // Test if it's a valid modelnumber
                if (variables[3] == 0)
                {
                    found = true; 
                }
            }

            System.Console.WriteLine("Answer: " + modelNumber + ", and " + 2);
        }
    }
}


using System;
using System.Linq;

class Day5_2022
{

    public static void calculate()
    {
        string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2022day5.txt").ToArray();

        // Assuming it will always be 9 stacks and adding an empty stack in the beginning since we want to use number instead of index
        // Having one setup for each part of the assignment to be able to go through them in the same loop
        String[] stacksPart1 = new String[10];
        String[] stacksPart2 = new String[10];

        int numberOfMaximumCrates = 0; //counting how many lines of crates we should go through

        foreach (string line in lines)
        {
            if (line.StartsWith(" 1 ")) 
            {
                // We have passed all rows with initial stacks
                // Go through the rows describing the intial crate stacks, starting from bottom
                for (int i = numberOfMaximumCrates-1; i >= 0 ; i--)
                {
                    // first stack = index 1, second stack = index 5, third stack = index 9 etc. So stack X = 1 + (X - 1) * 4
                    // Example from input file: [W]     [G] [Q] [P] [J] [F] [M] [C] Where second stack is empty
                    for (int s = 1; s <= 9; s++)
                    {
                        String crate = lines[i][1 + (s - 1) * 4].ToString().Trim(); 
                        stacksPart1[s] += crate;
                    }
                }
                stacksPart2 = (String[]) stacksPart1.Clone(); // cloning since a = will result in a pointer to the same array.
            }
            else if (line.StartsWith("move"))
            {
                string remainingInput = line.Substring(line.IndexOf(" ") + 1);
                int numberToMove = int.Parse(remainingInput.Substring(0, remainingInput.IndexOf(" ")));
                remainingInput = remainingInput.Substring(remainingInput.Length - 6);
                int fromPile = int.Parse(remainingInput.Substring(0, remainingInput.IndexOf(" ")));
                int toPile = int.Parse(remainingInput.Substring(remainingInput.LastIndexOf(" ")));

                //Part1 where I take the crates that should be moved by the crane, reverse them and put them on the other stack
                char[] cratesToMove = stacksPart1[fromPile].Substring(stacksPart1[fromPile].Length - numberToMove).ToCharArray();
                Array.Reverse(cratesToMove);
                stacksPart1[toPile] += new string(cratesToMove);
                stacksPart1[fromPile] = stacksPart1[fromPile].Substring(0, stacksPart1[fromPile].Length - numberToMove);

                ////part2 just move the crates as is 
                stacksPart2[toPile] += stacksPart2[fromPile].Substring(stacksPart2[fromPile].Length - numberToMove);
                stacksPart2[fromPile] = stacksPart2[fromPile].Substring(0, stacksPart2[fromPile].Length - numberToMove);
            }
            else
            {
                // count the number of lines with crates in the beginning of the input file
                numberOfMaximumCrates++;
            }
        }

        System.Console.WriteLine("Answer part 1: ");
        for (int s = 1; s <= 9; s++)
        {
            System.Console.Write(stacksPart1[s].Last());
        }
        System.Console.WriteLine();
        System.Console.WriteLine("Answer part 2: ");
        for (int s = 1; s <= 9; s++)
        {
            System.Console.Write(stacksPart2[s].Last());
        }
        System.Console.WriteLine();
    }
}

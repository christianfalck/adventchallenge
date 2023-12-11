using System;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day9_2023
    {
        public static void calculate()
        {
            BigInteger answer = 0;
            BigInteger answer2 = 0;
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day9.txt").ToArray();
            
            foreach (string line in lines)
            {
                // Parse the input
                string[] values = line.Split(' ');
                int[] numbers = new int[values.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = Convert.ToInt32(values[i]);
                }
                // For this assignment we just need the next and previous number in the sequence
                answer += CalculateNext(numbers);
                answer2 += CalculatePrevious(numbers);
            }
            System.Console.WriteLine("Answer part 1: " + answer + " and part 2: " + answer2);
        }

        // The number previous to the others are the same if we have a homogenous array e.g. 3 3 3 3 -> 3
        // The number previous is just as described in the assignment: Just take the first and subtract the difference
        static int CalculatePrevious(int[] numbers)
        {
            if (AreAllEqual(numbers))
            {
                return numbers[0];
            }
            int[] differences = new int[numbers.Length - 1];
            for (int i = 0; i < numbers.Count() - 1; i++)
            {
                differences[i] = numbers[i + 1] - numbers[i];
            }
            return numbers[0] - CalculatePrevious(differences);
        }

        // Recursive function that will return the upcoming number in a sequence
        static int CalculateNext(int[] numbers)
        {
            if (AreAllEqual(numbers))
            {
                // 3 3 3 3 -> next will be 3 since nothing changes
                return numbers[0];
            }
            int[] differences = new int[numbers.Length - 1];
            for (int i = 0; i < numbers.Count() - 1; i++)
            {
                differences[i] = numbers[i + 1] - numbers[i];
            }
            return numbers[numbers.Length - 1] + CalculateNext(differences);
        }

        // check if all numbers in the array are the same e.g. 3 3 3 3 
        static bool AreAllEqual(int[] numbers)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] != numbers[0])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
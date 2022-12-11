using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode
{
    class Day11_2022
    {
        public void calculate()
        {
            string[] lines = File.ReadLines("./../../../inputfiles/2022day11.txt").ToArray();

            // Parse the input
            int numberOfMonkeys = (lines.Length + 1) / 7; // 6 lines is used for each monkey, with an empty line between

            // Part 1:
            Monkey[] monkeys = new Monkey[numberOfMonkeys];
            for (int a = 0; a < numberOfMonkeys; a++)
            {
                string[] monkeyDescription = new string[6];
                Array.Copy(lines, a * 7, monkeyDescription, 0, 6);
                monkeys[a] = new Monkey(monkeyDescription);
            }

            for (int b = 0; b < 20; b++)
            {
                foreach (Monkey currentMonkey in monkeys)
                {
                    List<(int, long)> thrownObjects = currentMonkey.inspect(true,3);
                    foreach ((int, long) thrownObject in thrownObjects)
                    {
                        monkeys[thrownObject.Item1].throwTo(thrownObject.Item2);
                    }
                }
            }

            var busiestMonkeys = monkeys.OrderByDescending(x => x.getNumberOfInspects()).ToArray();
            long answerPart1 = busiestMonkeys[0].getNumberOfInspects() * busiestMonkeys[1].getNumberOfInspects();

            // Part 2:
            monkeys = new Monkey[numberOfMonkeys];

            // Find a way to reduce the stress levels
            // All tests are prime numbers. 
            // If we multiply these, we get a number that can be subtracted until we have a number < the prime numbers multiplied (called stressrelief here). 
            // (It's what modulus means)
            // We'll replace the stress factor with this number instead
            long stressRelief = 1;

            for (int a = 0; a < numberOfMonkeys; a++)
            {
                string[] monkeyDescription = new string[6];
                Array.Copy(lines, a * 7, monkeyDescription, 0, 6);
                monkeys[a] = new Monkey(monkeyDescription);
                stressRelief *= monkeys[a].getTestNumber();
            }

            for (int b = 0; b < 10000; b++)
            {
                foreach (Monkey currentMonkey in monkeys)
                {
                    List<(int, long)> thrownObjects = currentMonkey.inspect(false,stressRelief);
                    foreach ((int, long) thrownObject in thrownObjects)
                    {
                        monkeys[thrownObject.Item1].throwTo(thrownObject.Item2);
                    }
                }
            }

            busiestMonkeys = monkeys.OrderByDescending(x => x.getNumberOfInspects()).ToArray();
            long answerPart2 = busiestMonkeys[0].getNumberOfInspects() * busiestMonkeys[1].getNumberOfInspects();

            //foreach (Monkey currentMonkey in monkeys)
            //{
            //    System.Console.WriteLine("Monkey " + currentMonkey.getNumber() + ": " + currentMonkey.getNumberOfInspects());
            //}

            busiestMonkeys = monkeys.OrderByDescending(x => x.getNumberOfInspects()).ToArray();
            System.Console.WriteLine("Answer part 1: " + answerPart1 + " and part 2: " + answerPart2);

        }
    }

    class Monkey
    {
        int number; // The number of the monkey
        List<long> items; // The items the monkey currently have
        string operatorString; // the operatorString
        int test; // the number test should be divided by
        int throwToIfTrue; // if the number is divisible by test, throw to this monkey
        int throwToIfFalse; //otherwise throw to this
        long numberOfInspects = 0; //part 1 of the assignment: How many times have this monkey looked at an item
        public Monkey(string[] monkeyDescription)
        {
            number = int.Parse(monkeyDescription[0].Substring(monkeyDescription[0].IndexOf(" ") + 1, 1));
            items = monkeyDescription[1].Substring(monkeyDescription[1].IndexOf(":") + 1).Split(",").Select(x => long.Parse(x)).ToList();
            operatorString = monkeyDescription[2][(monkeyDescription[2].IndexOf("=") + 2)..];
            test = int.Parse(monkeyDescription[3][monkeyDescription[3].LastIndexOf(" ")..]);
            throwToIfTrue = int.Parse(monkeyDescription[4][monkeyDescription[4].LastIndexOf(" ")..]);
            throwToIfFalse = int.Parse(monkeyDescription[5][monkeyDescription[5].LastIndexOf(" ")..]);
        }

        public void throwTo(long number)
        {
            items.Add(number);
        }

        public long getNumber()
        {
            return number;
        }

        public long getNumberOfInspects()
        {
            return numberOfInspects;
        }

        public int getTestNumber()
        {
            return test;
        }

        public List<(int, long)> inspect(bool isPart1, long stressrelief)
        {
            List<(int, long)> toThrow = new List<(int, long)>();
            foreach (long item in items)
            {
                long newWorryLevel = operate(item);
                if (isPart1)
                {
                    // Part 1 should be divided by 3 to keep calm
                    newWorryLevel /= stressrelief;
                }
                else
                {
                    // Part 2 should be modulus the factor of all test numbers
                    // Take three prime numbers: X, Y and Z. 
                    // Any number that can be evenly divided by (X*Y*Z) can be evenly divided by X and by Y and by Z. 
                    // Since all numbers we use to test are prime numbers, we can modulus by the factor (p1*p2*...*p8)
                    newWorryLevel %= stressrelief;
                }

                if (newWorryLevel % test == 0)
                {
                    (int, long) t = (throwToIfTrue, newWorryLevel);
                    toThrow.Add(t);
                }
                else
                {
                    (int, long) t = (throwToIfFalse, newWorryLevel);
                    toThrow.Add(t);
                }
                numberOfInspects++;
            }
            items.Clear();
            return toThrow;
        }

        public long operate(long item)
        {
            long modifyWith;
            if (operatorString.EndsWith("old"))
            {
                modifyWith = item;
            }
            else
            {
                modifyWith = int.Parse(operatorString[operatorString.LastIndexOf(" ")..]);
            }
            // Modifying based on type of modifier. For part1 it's only + and *
            switch (operatorString.Substring(operatorString.IndexOf(" ") + 1, 1))
            {
                case "+":
                    return item + modifyWith;
                case "-":
                    return item - modifyWith;
                case "*":
                    return item * modifyWith;
                case "/":
                    return item / modifyWith;
            }
            return item;
        }

    }

}

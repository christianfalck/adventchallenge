﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day18_2021
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2021day18.txt").ToArray();

            // Part 1
            int answerPart1 = DoHomework(lines);

            // Part 2 - I have to apologise but the oldest kid is nagging me for attention to his computer game. So no pretty solution :-)
            int answerPart2 = 0;
            for(int i = 0; i < lines.Length; i++)
                for(int j = 0; j < lines.Length; j++)
                {
                    if(i != j)
                    {
                        List<string> tmpList = new List<string>();
                        tmpList.Add(lines[i]);
                        tmpList.Add(lines[j]);
                        int tmp = DoHomework(tmpList.ToArray());
                        if (tmp > answerPart2)
                            answerPart2 = tmp;
                        tmpList = new List<string>();
                        tmpList.Add(lines[j]);
                        tmpList.Add(lines[i]);
                        tmp = DoHomework(tmpList.ToArray());
                        if (tmp > answerPart2)
                            answerPart2 = tmp;
                    }
                }
            
            System.Console.WriteLine("Answer part 1: " + answerPart1 + ", and part 2: " + answerPart2);
        }

        public static int DoHomework(string[] lines)
        {
            string assignmentSoFar = lines[0];
            SnailNumberWithHeight[] myList = Transform(assignmentSoFar);
            for (int rowNbr = 1; rowNbr < lines.Length; rowNbr++)
            {
                SnailNumberWithHeight[] newList = Transform(lines[rowNbr]);
                AddParenthesis(myList);
                AddParenthesis(newList);
                myList = myList.Concat(newList).ToArray();
            AfterSplit:
                // Calculate by 
                // 1. Explode
                bool allExploded = false;
                while (!allExploded)
                {
                    allExploded = true;
                    for (int i = 1; i < myList.Length; i++)
                    {
                        if (myList[i].parentheses > 4 && myList[i].parentheses == myList[i - 1].parentheses)
                        {
                            // Explode
                            if (i - 2 >= 0)
                                myList[i - 2].value += myList[i - 1].value;
                            if (i + 1 < myList.Length)
                                myList[i + 1].value += myList[i].value;
                            myList[i].value = 0; // The pair is replaced by a 0
                            myList[i].parentheses--; // The 0 will not be surrounded by a parenthesis
                            myList = myList.Where((source, index) => index != i - 1).ToArray(); // Remove the other value
                            allExploded = false;
                        }
                    }
                }
                // 2. Split
                var tmpList = myList.ToList();
                for (int i = 0; i < myList.Length; i++)
                {
                    if (myList[i].value > 9)
                    {
                        // Split!
                        SnailNumberWithHeight a = new SnailNumberWithHeight((int)Math.Floor((double)myList[i].value / 2), myList[i].parentheses + 1);
                        SnailNumberWithHeight b = new SnailNumberWithHeight((int)Math.Ceiling((double)myList[i].value / 2), myList[i].parentheses + 1);
                        tmpList.RemoveAt(i);
                        tmpList.Insert(i, b);
                        tmpList.Insert(i, a);
                        myList = tmpList.ToArray();
                        goto AfterSplit;
                    }
                }
            }

            // Calculate magnitude
            // Find the element with the most parenteses and calculate that + the next. Start over until only one remains. 
            while (myList.Length > 1)
            {
                int maxParentheses = myList.Max(x => x.parentheses);

                for (int i = 0; i < myList.Length - 1; i++)
                {
                    if (myList[i].parentheses == maxParentheses)
                    {
                        int newValue = myList[i].value * 3 + myList[i + 1].value * 2;
                        myList[i].value = newValue;
                        myList[i].parentheses--;
                        myList = myList.Where((source, index) => index != i + 1).ToArray(); // Remove the other value
                    }
                }
            }
            return myList[0].value;
        }

        // Transforms the number into a series of numbers with height (number of paranteses around) and value. 
        public static SnailNumberWithHeight[] Transform(string snailNumber)
        {
            var numbers = new List<SnailNumberWithHeight>();
            int nbrOfParentheses = 0;
            for (int i = 0; i < snailNumber.Length; i++)
            {
                if (snailNumber[i] == '[')
                    nbrOfParentheses++;
                else if (snailNumber[i] == ']')
                    nbrOfParentheses--;
                else if (snailNumber[i] == ',')
                {
                    // Do nothing
                }
                else
                {
                    // A number
                    numbers.Add(new SnailNumberWithHeight(snailNumber[i] - '0', nbrOfParentheses));
                }
            }
            return numbers.ToArray();
        }

        // Add a [] around the current number of values: Corresponding to a + sign
        public static void AddParenthesis(SnailNumberWithHeight[] snwh)
        {
            foreach (SnailNumberWithHeight s in snwh)
                s.parentheses++;
        }
    }

    public class SnailNumberWithHeight
    {
        public SnailNumberWithHeight(int value, int parentheses)
        {
            this.value = value;
            this.parentheses = parentheses;
        }
        public int value;
        public int parentheses;
    }
}

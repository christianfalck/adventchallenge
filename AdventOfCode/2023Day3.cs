using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day3_2023
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2023day3.txt").ToArray();

            int answer = 0;

            List<FoundNumber> allNumbers = new List<FoundNumber>();
            List<FoundSymbol> allSymbols = new List<FoundSymbol>();

            int rowIndex = 0;
            foreach (string line in lines)
            {
                int columnIndex = 1; // first character on each row is index 1
                foreach (char c in line)
                {
                    // Find all symbols and store their coordinates
                    if (!char.IsDigit(c) && c != '.')
                    {
                        FoundSymbol fs = new FoundSymbol();
                        fs.coordinates = new Point(columnIndex, rowIndex);
                        fs.symbol = c;
                        allSymbols.Add(fs);
                    }
                    columnIndex++;
                }
                // Find all numbers and store their coordinates
                MatchCollection matches = Regex.Matches(line, @"\d+");

                foreach (Match match in matches)
                {
                    allNumbers.Add(new FoundNumber(Int32.Parse(match.Value), rowIndex, match.Index + 1));
                }

                rowIndex++;
            }

            // For each number, add it to the answer for part 1 if a neighbour is a symbol
            foreach (FoundNumber fn in allNumbers)
            {
                int thisValue = fn.myValueForSymbol(allSymbols);
                answer += thisValue;
            }

            int answer2 = 0;

            foreach (FoundSymbol fs in allSymbols)
            {
                if (fs.symbol == '*')
                    answer2 += fs.valueOfNeighbours();
            }
            System.Console.WriteLine("Answer part 1: " + answer + " and part 2: " + answer2);
        }
    }

    class FoundNumber
    {

        public FoundNumber(int value, int row, int column)
        {
            this.Value = value;
            this.myStart = new Point(column, row);
            this.length = (int)Math.Log10(value) + 1;
        }

        public Point myStart { get; set; }

        public int length { get; set; }

        public int Value { get; set; }

        // Part 1: return the value if it has a signal as neighbour
        // Part 2: add this value to it's neighbouring symbols
        public int myValueForSymbol(List<FoundSymbol> allSymbols)
        {
            bool found = false;
            foreach (FoundSymbol fs in allSymbols)
            {
                // It's in the boundary if it's at most 1 step away in every direction
                if (fs.coordinates.x >= myStart.x - 1 && fs.coordinates.x <= myStart.x + length)
                {
                    if (fs.coordinates.y >= myStart.y - 1 && fs.coordinates.y <= myStart.y + 1)
                    {
                        // We have a symbol within reach, return the value of this number
                        found = true;
                        fs.neighbours.Add(Value); // adding this value to the symbol for part 2
                    }
                }
            }
            if (found)
                return Value;
            // No symbol within reach, return 0;
            return 0;
        }
    }

    class FoundSymbol
    {
        public FoundSymbol()
        {
            neighbours = new List<int>();
        }

        // Column, row
        public Point coordinates { get; set; }
        public char symbol { get; set; }

        public List<int> neighbours { get; set; }

        // Return the product of the two neighbours. If there are more or less number of neighbours, return 0.
        public int valueOfNeighbours()
        {
            if (neighbours.Count == 2)
                return neighbours[0] * neighbours[1];
            else
                return 0;
        }
    }

}

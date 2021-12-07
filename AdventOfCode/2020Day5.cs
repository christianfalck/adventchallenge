using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day5_2020
    {
        public static void calculate()

        { 
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2020day5.txt").ToArray();

            //Part 1
            double highestSeatOnThePlane = 0;
            foreach(string l in lines)
            {
                double id = getSeatID(l);
                if (id > highestSeatOnThePlane)
                    highestSeatOnThePlane = id;
            }
            
            System.Console.WriteLine("Answer Part1: " + highestSeatOnThePlane);

            //Part 2
            var occupiedSeatsOnEachRow = new Dictionary<int, List<int>>();
            foreach (string l in lines)
            {
                int row = getRow(l);
                int column = getColumn(l);
                if (!occupiedSeatsOnEachRow.ContainsKey(row))
                    occupiedSeatsOnEachRow.Add(row, new List<int>());
                occupiedSeatsOnEachRow[row].Add(column);
            }
            foreach (var item in occupiedSeatsOnEachRow.Keys)
            {
                if(occupiedSeatsOnEachRow[item].Count == 7)
                {
                    //Free seat!
                    List<int> mySeat = Enumerable.Range(0, 7).Except(occupiedSeatsOnEachRow[item]).ToList();
                    int myColumn = mySeat.Sum(); // since it's only one seat
                    System.Console.WriteLine("Answer Part2. Row " + item + " and Column " + myColumn + " gives ID: " + (item*8+myColumn));
                }
            }
        }

        public static double getSeatID(string input)
        {
            double row = 0;
            for(int i = 0; i<8; i++)
            {
                if (input[i] == 'B')
                    row += Math.Pow(2, 6 - i);
            }
            double column = 0;
            for (int i = 0; i < 3; i++)
            {
                if (input[7+i] == 'R')
                    column += Math.Pow(2, 2 - i);
            }
            return ((row*8)+column);
        }
        public static int getRow(string input)
        {
            int row = 0;
            for (int i = 0; i < 8; i++)
            {
                if (input[i] == 'B')
                    row += (int)Math.Pow(2, 6 - i); // cast can normally give outofboundsexception 
            }
            return row;
        }
        public static int getColumn(string input)
        {
            int column = 0;
            for (int i = 0; i < 3; i++)
            {
                if (input[7+i] == 'R')
                    column += (int)Math.Pow(2, 2 - i); ; // cast can normally give outofboundsexception 
            }
            return column;
        }

    }
}

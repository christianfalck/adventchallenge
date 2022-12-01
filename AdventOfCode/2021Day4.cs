using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day4_2021
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2021day4.txt").ToArray();
            //read numbers drawn
            int[] bingoNumbers = Array.ConvertAll<string, int>(lines[0].Split(','), int.Parse);
            //read boards
            int row = 0;
            int boardIndex = 0;
            Board[] boards = new Board[(lines.Length - 1) / 6];
            for (int i = 1; i < lines.Length; i++)
            {
                if (row == 0)
                {
                    // empty row
                    boards[boardIndex] = new Board();
                }
                else
                {
                    string lineWithoutFirstSpace = lines[i].Trim();
                    string[] firstRowAsString = System.Text.RegularExpressions.Regex.Split(lineWithoutFirstSpace, @"\s{1,}");
                    int[] firstRowAsInt = Array.ConvertAll(firstRowAsString, s => Int32.Parse(s));
                    boards[boardIndex].numbers[row - 1] = firstRowAsInt;
                }
                row++;
                if (row == 6) // only 5 rows per bingo board
                {
                    row = 0;
                    boardIndex++;
                }
            }
            //Identify the winning board
            int numberOfFinishedBoards = 0; // total number of boards = 100
            int part1answer = 0;
            int part2answer = 0;
            foreach (int n in bingoNumbers)
            {
                foreach (Board b in boards)
                {
                    int result = b.checkNumber(n);
                    if (result > 0)
                    {
                        if(numberOfFinishedBoards == 0)
                        {
                            part1answer = result * n; 
                        }
                        numberOfFinishedBoards++;
                        if(numberOfFinishedBoards == 100)
                        {
                            part2answer = result * n;
                        }
                    }
                }
            }
            System.Console.WriteLine("Answer: " + part1answer + ", and " + part2answer);
        }
    }
    public class Board
    {
        public int[][] numbers;
        public bool[][] numberFound;
        public bool alreadyWon = false; 
        public Board()
        {
            numbers = new int[5][];
            numberFound = new bool[5][];
            numberFound[0] = new bool[5];
            numberFound[1] = new bool[5];
            numberFound[2] = new bool[5];
            numberFound[3] = new bool[5];
            numberFound[4] = new bool[5];
        }
        //returns the sum of all unmarked numbers in case of full row, 0 otherwise
        public int checkNumber(int shoutedNumber)
        {
            if (alreadyWon)
                return 0;
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if(numbers[i][j] == shoutedNumber)
                    {
                        numberFound[i][j] = true;
                        if ((numberFound[i][0] && numberFound[i][1] && numberFound[i][2] && numberFound[i][3] && numberFound[i][4]) || 
                            (numberFound[0][j] && numberFound[1][j] && numberFound[2][j] && numberFound[3][j] && numberFound[4][j]))
                        {
                            int sum = 0;
                            alreadyWon = true; 
                            for (int k = 0; k < 5; k++)
                            {
                                for (int l = 0; l < 5; l++)
                                {
                                    if (!numberFound[k][l])
                                    {
                                        sum += numbers[k][l];
                                    }
                                }
                            }
                            return sum;
                        }
                    }
                }
            }
            return 0;
        }
    }
}

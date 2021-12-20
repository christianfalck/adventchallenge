using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day20
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/day20.txt").ToArray();

            bool[] algorithm = new bool[lines[0].Length];
            for (int i = 0; i < lines[0].Length; i++)
                algorithm[i] = lines[0][i] == '#' ? true : false; 

            // Initial iteration of the image, with two frames of . around to simplify next iteration
            List<bool[]> image = new List<bool[]>();
            int widthNextIteration = lines[2].Length + 4;
            bool[] emptyrow = new bool[widthNextIteration];
            image.Add(emptyrow);
            image.Add(emptyrow);
            for (int i = 2; i < lines.Length; i++)
            {
                bool[] row = new bool[widthNextIteration];
                int index = 2;
                foreach (char c in lines[i])
                {
                    row[index++] = (c == '#') ? true : false;
                }
                image.Add(row);
            }
            image.Add(emptyrow);
            image.Add(emptyrow);

            // An iteration
            // For each iteration, look through the current x * x image and calculate next one 
            int numberOfIterations = 2;
            int whitepixels = 0;
            for (int i = 0; i < numberOfIterations; i++)
            {
                for (int a = 0; a < image.Count; a++)
                {
                    for (int b = 0; b < image[0].Length; b++)
                    {
                        if (image[a][b])
                        {
                            Console.Write("#");
                            whitepixels++;
                        }
                        else
                            Console.Write(".");
                    }

                    Console.WriteLine("");
                }
                Console.WriteLine("------ " + whitepixels +" --------");

                widthNextIteration += 2;
                List<bool[]> newImage = new List<bool[]>();
                emptyrow = new bool[widthNextIteration];
                newImage.Add(emptyrow);
                newImage.Add(emptyrow);
                for (int rowNumber = 1; rowNumber < image.Count - 1; rowNumber++) // don't need to calculate first or last
                {
                    bool[] newRow = new bool[widthNextIteration];
                    for (int colNumber = 1; colNumber < widthNextIteration - 3; colNumber++) // don't need to calculate first or last
                    {
                        
                        int valueToCheckInAlgorithm = 256 * (image[rowNumber - 1][colNumber - 1] ? 1 : 0) +
                            128 * (image[rowNumber - 1][colNumber] ? 1 : 0) +
                            64 * (image[rowNumber - 1][colNumber + 1] ? 1 : 0) +
                            32 * (image[rowNumber][colNumber - 1] ? 1 : 0) +
                            16 * (image[rowNumber][colNumber] ? 1 : 0) +
                            8 * (image[rowNumber][colNumber + 1] ? 1 : 0) +
                            4 * (image[rowNumber+1][colNumber - 1] ? 1 : 0) +
                            2 * (image[rowNumber+1][colNumber] ? 1 : 0) +
                            1 * (image[rowNumber+1][colNumber + 1] ? 1 : 0);
                        newRow[colNumber+1] = algorithm[valueToCheckInAlgorithm]; // shift new image one step to the right 
                    }
                    newImage.Add(newRow);
                }
                newImage.Add(emptyrow);
                newImage.Add(emptyrow);
                image = newImage;
            }
            int answer1 = 0;
            for (int i = 0; i < image.Count; i++)
            {
                for (int j = 0; j < image[0].Length; j++)
                {
                    if (image[i][j])
                    {
                        answer1++;
                        Console.Write("#");
                    }
                    else
                        Console.Write(".");
                }
                    
                Console.WriteLine("");
            }
                
            System.Console.WriteLine("Answer: " + answer1 + ", and " + 2);
        }
    }

    record Pixel(int x, int y);
}

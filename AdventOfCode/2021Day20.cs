using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day20_2021
    {
        public static void calculate()
        {
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2021day20.txt").ToArray();

            string algorithm = lines[0];

            // Trying storing in dictionary with coordinates
            Dictionary<Pixel, string> image = new Dictionary<Pixel, string>();
            for (int y = 2; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[2].Length; x++)
                {
                    image[new Pixel(x, y - 2)] = lines[y][x].ToString(); // y-2 since we're looking at input row 3 for our row 1
                }
            }

            int answer1 = 0;
            int answer2 = 0;
            int numberOfIterations = 50;
            for (int i = 1; i <= numberOfIterations; i++)
            {
                Dictionary<Pixel, string> enhancedImage = new();
                string defaultPixel = i % 2 == 1 ? "." : "#"; // All uncalculated 
                // Since all "surrounding" pixels invert color every enhancement, we need to know how large our image is
                int minX = image.Min(a => a.Key.x);
                int maxX = image.Max(a => a.Key.x);
                int minY = image.Min(a => a.Key.y);
                int maxY = image.Max(a => a.Key.y);

                // More than 2 pixels out doesn't matter since those will be surrounded by similar pixels and invert next step.
                for (int x = minX - 2; x < maxX + 2; x++)
                {
                    for (int y = minY - 2; y < maxY + 2; y++)
                    {
                        // If the value haven't been calculated, it's inverted (which is why I use GetValueOrDefault)
                        //TODO: This should be able to do in a nicer way!
                        int valueToCheckInAlgorithm = image.GetValueOrDefault(new Pixel(x - 1, y - 1), defaultPixel) == "#" ? 256 : 0;
                        valueToCheckInAlgorithm += image.GetValueOrDefault(new Pixel(x, y - 1), defaultPixel) == "#" ? 128 : 0;
                        valueToCheckInAlgorithm += image.GetValueOrDefault(new Pixel(x + 1, y - 1), defaultPixel) == "#" ? 64 : 0;
                        valueToCheckInAlgorithm += image.GetValueOrDefault(new Pixel(x - 1, y), defaultPixel) == "#" ? 32 : 0;
                        valueToCheckInAlgorithm += image.GetValueOrDefault(new Pixel(x, y), defaultPixel) == "#" ? 16 : 0;
                        valueToCheckInAlgorithm += image.GetValueOrDefault(new Pixel(x + 1, y), defaultPixel) == "#" ? 8 : 0;
                        valueToCheckInAlgorithm += image.GetValueOrDefault(new Pixel(x - 1, y + 1), defaultPixel) == "#" ? 4 : 0;
                        valueToCheckInAlgorithm += image.GetValueOrDefault(new Pixel(x, y + 1), defaultPixel) == "#" ? 2 : 0;
                        valueToCheckInAlgorithm += image.GetValueOrDefault(new Pixel(x + 1, y + 1), defaultPixel) == "#" ? 1 : 0;

                        enhancedImage[new Pixel(x, y)] = algorithm[valueToCheckInAlgorithm].ToString();
                    }
                }
                image = enhancedImage;
                if (i == 2)
                    answer1 = image.Values.Count(a => a == "#"); 
            }
            answer2 = image.Values.Count(a => a == "#");

            System.Console.WriteLine("Answer: " + answer1 + ", and " + answer2);
        }
    }

    // Used as a key in the dictionary
    public struct Pixel
    {
        public int x;
        public int y;
        public Pixel(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

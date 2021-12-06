using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode
{
    class Day3_2020
    {
        public static void calculate()
        {
            int[] offset = { 1, 3, 5, 7, 1 };
            int[] index = new int[offset.Length];
            int[] numberOfTrees = new int[offset.Length];
            bool odd = true; 
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2020day3.txt"))
            {
                for (int i = 0; i < offset.Length-1; i++)
                {
                    if (line[index[i]] == '#')
                        numberOfTrees[i]++;
                    index[i] += offset[i];
                    if (index[i] >= line.Length)
                        index[i] -= line.Length;
                }
                // the one that only runs every 2nd loop
                if(odd)
                {
                    if (line[index[4]] == '#')
                        numberOfTrees[4]++;
                    index[4] += offset[4];
                    if (index[4] >= line.Length)
                        index[4] -= line.Length;
                }
                odd=!odd;
            }

            BigInteger multiplied = new BigInteger(1);
            foreach (int t in numberOfTrees)
                multiplied *= t;
            System.Console.WriteLine("Answer: " + "[{0}]", string.Join(", ", numberOfTrees) + ", and multiplied: "+ multiplied);

        }
    }
}

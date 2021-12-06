using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day1
    {
        public static void calculate()
        {
            int prev_sonar = 0;
            int this_sonar;
            int number_of_increases = 0;
            int number_of_advanced_increases = 0;
            int count = 0;
            int A = 0, B = 0, C = 0;
            int old_sum, new_sum;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/day1.txt"))
            {
                this_sonar = Int32.Parse(line);
                count++;
                if (this_sonar > prev_sonar)
                {
                    if (count != 1)
                        number_of_increases++;
                }
                prev_sonar = this_sonar;

                old_sum = A + B + C;
                C = B;
                B = A;
                A = this_sonar;
                new_sum = A + B + C;
                if (count > 3 && new_sum > old_sum)
                    number_of_advanced_increases++;
            }

            System.Console.WriteLine("Answer: " + number_of_increases + ", and " + number_of_advanced_increases);
        }
    }
}

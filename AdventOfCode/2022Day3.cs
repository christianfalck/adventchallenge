using System.Linq;

namespace AdventOfCode
{
    class Day3_2022
    {
        public static void calculate()
        {
            // compare first and second half of the line, take the value of the common character and add all values from all lines. 
            string[] lines = System.IO.File.ReadLines("./../../../inputfiles/2022day3.txt").ToArray();
            int score = 0;
            foreach (string line in lines)
            {
                string first = line.Substring(0, line.Length / 2);
                string second = line.Substring(line.Length / 2);

                // Intersects will give a list but from this assignment I know that the list will only contain one element
                var character = first.Intersect(second).First();

                int value = (character - '0');
                if (value > 48) // a = 49 -> 1
                {
                    score += value - 48;
                }
                else // A = 17 -> 27
                {
                    score += value + 10;
                }
            }

            //Part 2
            // Here the assignment is to look at three rows at the time and find the one character that is unique for all three rows. 
            int score2 = 0;
            for (int i = 0; i < lines.Length; i += 3)
            {

                string first = lines[i];
                string second = lines[i + 1];
                string third = lines[i + 2];

                // The intersection of two rows will probably be more than one character but when intersecting with the third we should only get one as per assignment
                var characters = first.Intersect(second);
                var character = third.Intersect(characters).First();

                // Same calculation as before to identify the values. 
                int value = (character - '0');
                if (value > 49)
                {
                    score2 += value - 48;
                }
                else
                {
                    score2 += value + 10;
                }
            }

            System.Console.WriteLine("Answer: " + score + ", and " + score2);
        }
    }
}

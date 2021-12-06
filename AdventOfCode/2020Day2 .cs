using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day2_2020
    {
        public static void calculate()
        {

            //för varje rad
            //hämta ut policy
            //räkna antalet bokstäver som är specificerat i policyn
            //se om antalet bokstäver är i spannet som är tillåtet
            //ta bort lösenordet annars
            List<string> validPasswordsPart1 = new List<string>();
            List<string> validPasswordsPart2 = new List<string>();
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2020day2.txt"))
            {
                int charsFound = line.Substring(line.IndexOf(":"),line.Length- line.IndexOf(":")).Count(f => f == getCharacter(line));
                if (getMinimumNumber(line) <= charsFound && charsFound <= getMaximumNumber(line))
                {
                    validPasswordsPart1.Add(line);
                }
                if (hasCharacter(line, getMinimumNumber(line), getMaximumNumber(line), getCharacter(line)))
                    validPasswordsPart2.Add(line);
                System.Console.WriteLine(line);
                System.Console.WriteLine("one..." + line[line.IndexOf(":") + 2 + getMinimumNumber(line) - 1] + "...two..." + line[line.IndexOf(":") + 2 + getMaximumNumber(line) - 1] + "...test..." + hasCharacter(line, getMinimumNumber(line), getMaximumNumber(line), getCharacter(line)));
            }
                
            System.Console.WriteLine("Answer: " + validPasswordsPart1.Count + " and: " + validPasswordsPart2.Count);

        }
        public static char getCharacter(string line)
        {
            return line[line.IndexOf(":") - 1];
        }
        public static int getMinimumNumber(string line)
        {
            return Int32.Parse((line.Substring(0, line.IndexOf("-"))));
        }
        public static int getMaximumNumber(string line)
        {
            return Int32.Parse((line.Substring(line.IndexOf("-")+1, line.IndexOf(" ") - line.IndexOf("-"))));
        }
        public static bool hasCharacter(string line, int first, int second, char character)
        {
            //find out where the password starts
            int start = line.IndexOf(":") + 2;
            // remember that their index is +1
            return line[start+first-1] == character ^ line[start+second-1] == character;
        }
    }
}

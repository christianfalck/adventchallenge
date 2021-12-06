using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day4_2020
    {
        public static void calculate()
        {
            bool byr = false;
            bool iyr = false;
            bool eyr = false;
            bool hgt = false;
            bool hcl = false;
            bool ecl = false;
            bool pid = false;
            bool byr2 = false;
            bool iyr2 = false;
            bool eyr2 = false;
            bool hgt2 = false;
            bool hcl2 = false;
            bool ecl2 = false;
            bool pid2 = false;
            //bool cid = false;
            int numberOfValidPassportsPart1 = 0;
            int numberOfValidPassportsPart2 = 0;
            int numberOfPassports = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2020day4.txt"))
            {
                if (line != "")
                {
                    string[] values = line.Split(" ");
                    for (int i = 0; i < values.Length; i++)
                    {
                        string valueName = values[i].Substring(0, 3);
                        if (valueName == "byr")
                        {
                            byr = true;
                            int bYear = int.Parse(values[i].Substring(values[i].IndexOf(":") + 1));
                            if (bYear >= 1920 && bYear <= 2002)
                                byr2 = true;
                        }
                        else if (valueName == "iyr")
                        {
                            iyr = true;
                            int iYear = int.Parse(values[i].Substring(values[i].IndexOf(":") + 1));
                            if (iYear >= 2010 && iYear <= 2020)
                                iyr2 = true;
                        }
                        else if (valueName == "eyr")
                        {
                            eyr = true;
                            int eYear = int.Parse(values[i].Substring(values[i].IndexOf(":") + 1));
                            if (eYear >= 2020 && eYear <= 2030)
                                eyr2 = true;
                        }
                        else if (valueName == "hgt")
                        {
                            hgt = true;
                            string heightTotal = values[i].Substring(values[i].IndexOf(":") + 1);
                            if ((heightTotal.IndexOf("cm") > 0))
                            {
                                int height = int.Parse(heightTotal.Substring(0, heightTotal.IndexOf("cm")));
                                if (height >= 150 && height <= 193)
                                    hgt2 = true;
                            }
                            else if (heightTotal.IndexOf("in") > 0)
                            {
                                int height = int.Parse(heightTotal.Substring(0, heightTotal.IndexOf("in")));
                                if (height >= 59 && height <= 76)
                                    hgt2 = true;
                            }
                        }
                        else if (valueName == "hcl")
                        {
                            hcl = true;
                            string haircolor = values[i].Substring(values[i].IndexOf(":") + 2);
                            var pattern = "^[0-9a-f]{6}$";
                            if (values[i].Substring(values[i].IndexOf(":")+1, 1) == "#")
                                if(Regex.Match(haircolor, pattern).Success)
                                    hcl2 = true;

                        }
                        else if (valueName == "ecl")
                        {
                            ecl = true;
                            string eyeColor = values[i].Substring(values[i].IndexOf(":") + 1);
                            if(eyeColor.Contains("amb") || eyeColor.Contains("blu") || eyeColor.Contains("brn") || eyeColor.Contains("gry") || eyeColor.Contains("grn") || eyeColor.Contains("hzl") || eyeColor.Contains("oth"))
                                ecl2 = true;
                        }
                        else if (valueName == "pid")
                        {
                            pid = true;
                            string passportID = values[i].Substring(values[i].IndexOf(":") + 1);
                            var pattern = "^\\d{9}$";
                            if (Regex.Match(passportID, pattern).Success)
                                pid2 = true;
                        }
                    }
                }
                if(line=="")
                {
                    if (byr && iyr && eyr && hgt && hcl && ecl && pid)
                    {
                        numberOfValidPassportsPart1++; 
                    }
                    if (byr2 && iyr2 && eyr2 && hgt2 && hcl2 && ecl2 && pid2)
                    {
                        numberOfValidPassportsPart2++; 
                    }
                    numberOfPassports++;
                    byr = false;
                    iyr = false;
                    eyr = false;
                    hgt = false;
                    hcl = false;
                    ecl = false;
                    pid = false;
                    byr2 = false;
                    iyr2 = false;
                    eyr2 = false;
                    hgt2 = false;
                    hcl2 = false;
                    ecl2 = false;
                    pid2 = false;
                }
            }
            if (byr && iyr && eyr && hgt && hcl && ecl && pid)
            {
                numberOfValidPassportsPart1++;
            }
            if (byr2 && iyr2 && eyr2 && hgt2 && hcl2 && ecl2 && pid2)
            {
                numberOfValidPassportsPart2++;
            }
            System.Console.WriteLine("Answer: " + numberOfValidPassportsPart1 + ", and " + numberOfValidPassportsPart2);
        }
    }
}

﻿using System;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                System.Console.WriteLine("Which day should we execute? (year+number, 20191 for the first one 2019.");
                String choice = System.Console.ReadLine();
                switch (choice)
                {
                    case "20231":
                        Day1_2023.calculate();
                        break;
                    case "20232":
                        Day2_2023.calculate();
                        break;
                    case "20233":
                        Day3_2023.calculate();
                        break;
                    case "20234":
                        Day4_2023.calculate();
                        break;
                    case "20235":
                        Day5_2023.calculate();
                        break;
                    case "20236":
                        Day6_2023.calculate();
                        break;
                    case "20237":
                        Day7_2023.calculate();
                        break;
                    case "20238":
                        Day8_2023.calculate();
                        break;
                    case "20239":
                        Day9_2023.calculate();
                        break;
                    case "202310":
                        Day10_2023.calculate();
                        break;
                    case "202311":
                        Day11_2023.calculate();
                        break;
                    case "202312":
                        Day12_2023.calculate();
                        break;
                    case "202313":
                        Day13_2023.calculate();
                        break;
                    case "202314":
                        Day14_2023.calculate();
                        break;
                    case "202315":
                        Day15_2023.calculate();
                        break;
                    case "202316":
                        Day16_2023.calculate();
                        break;
                    case "20221":
                        Day1_2022.calculate();
                        break;
                    case "20222":
                        Day2_2022.calculate();
                        break;
                    case "20223":
                        Day3_2022.calculate();
                        break;
                    case "20224":
                        Day4_2022.calculate();
                        break;
                    case "20225":
                        Day5_2022.calculate();
                        break;
                    case "20226":
                        Day6_2022.calculate();
                        break;
                    case "20227":
                        new Day7_2022().calculate();
                        break;
                    case "20228":
                        Day8_2022.calculate();
                        break;
                    case "20229":
                        Day9_2022.calculate();
                        break;
                    case "202210":
                        new Day10_2022().calculate();
                        break;
                    case "202211":
                        new Day11_2022().calculate();
                        break;
                    case "202212":
                        Day12_2022.calculate();
                        break;
                    case "202213":
                        Day13_2022.calculate();
                        break;
                    case "202214":
                        Day14_2022.calculate();
                        break;
                    case "202215":
                        Day15_2022.calculate();
                        break;
                    case "20211":
                        Day1_2021.calculate();
                        break;
                    case "20212":
                        Day2_2021.calculate();
                        break;
                    case "20213":
                        Day3_2021.calculate();
                        break;
                    case "20214":
                        Day4_2021.calculate();
                        break;
                    case "20215":
                        Day5_2021.calculate();
                        break;
                    case "20216":
                        Day6_2021.calculate();
                        break;
                    case "20217":
                        Day7_2021.calculate();
                        break;
                    case "20218":
                        Day8_2021.calculate();
                        break;
                    case "20219":
                        Day9_2021.calculate();
                        break;
                    case "202110":
                        Day10_2021.calculate();
                        break;
                    case "202111":
                        Day11_2021.calculate();
                        break;
                    case "202112":
                        Day12_2021.calculate();
                        break;
                    case "202113":
                        Day13_2021.calculate();
                        break;
                    case "202114":
                        Day14_2021.calculate();
                        break;
                    case "202115":
                        Day15_2021.calculate();
                        break;
                    case "202116":
                        Day16_2021.calculate();
                        break;
                    case "202117":
                        Day17_2021.calculate();
                        break;
                    case "202118":
                        Day18_2021.calculate();
                        break;
                    case "202119":
                        new Day19_2021().calculate();
                        break;
                    case "202120":
                        Day20_2021.calculate();
                        break;
                    case "202121":
                        new Day21_2021();
                        break;
                    case "202122":
                        Day22_2021.calculate();
                        break;
                    case "202123":
                        Day23_2021.calculate();
                        break;
                    case "202124":
                        new Day24_2021().calculate();
                        break;
                    case "202125":
                        Day25_2021.calculate();
                        break;
                    case "20191":
                        Day1_2019.calculate();
                        break;
                    case "20192":
                        Day2_2019.calculate();
                        break;
                    case "20193":
                        Day3_2019.calculate();
                        break;
                    case "20194":
                        Day4_2019.calculate();
                        break;
                    case "20195":
                        Day5_2019.calculate();
                        break;
                    case "20196":
                        Day6_2019.calculate();
                        break;
                    case "20202":
                        Day2_2020.calculate();
                        break;
                    case "20203":
                        Day3_2020.calculate();
                        break;
                    case "20204":
                        Day4_2020.calculate();
                        break;
                    case "20205":
                        Day5_2020.calculate();
                        break;
                    case "20206":
                        Day6_2020.calculate();
                        break;
                    default:
                        System.Console.WriteLine("That one isn't implemented");
                        break;
                }
            }
        }
    }
}

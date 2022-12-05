using System;
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
                switch(choice)
                {
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

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
                System.Console.WriteLine("Which day should we execute? (1-25 for 2021, otherwise year+number, 20191 for the first one 2019.");
                String choice = System.Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        Day1.calculate();
                        break;
                    case "2":
                        Day2.calculate();
                        break;
                    case "3":
                        Day3.calculate();
                        break;
                    case "4":
                        Day4.calculate();
                        break;
                    case "5":
                        Day5.calculate();
                        break;
                    case "6":
                        Day6.calculate();
                        break;
                    case "7":
                        Day7.calculate();
                        break;
                    case "8":
                        Day8.calculate();
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

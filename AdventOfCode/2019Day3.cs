using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class MyLine
    {
        public bool isXSet;
        public int setValue;
        public int minVarValue;
        public int maxVarValue;
        public int startVariable; // to calculate distance to intersection. (part 2)
    }

    class IntersectionPoint
    {
        public int x, y;
        //part 2: z = total distance travelled
        public int z;
        public IntersectionPoint(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
    }

    class Day3_2019
    {
        public static void calculate()
        {
            parseFile();
        }

        public static bool doesTheseInterSect(MyLine a, MyLine b)
        {
            if(a.setValue == 0 && b.setValue == 0)
            {
                // center doesn't count
                return false; 
            }
            if(a.isXSet && b.isXSet)
            { 
                // both going vertical
                return false;  
            }
            if (!a.isXSet && !b.isXSet)
            {
                // both going horisontal
                return false;
            }
            return (a.minVarValue <= b.setValue) && (b.setValue <= a.maxVarValue) && (b.minVarValue <= a.setValue) && (a.setValue <= b.maxVarValue);
        }

        public static void parseFile()
        {
            string text = File.ReadAllText("./../../../inputfiles/2019day3.txt");
            int secondSetStartsAt = text.IndexOf("\n");
            string firstSet = text.Substring(0, secondSetStartsAt);
            string secondSet = text.Substring(secondSetStartsAt + 1);
            string[] firstSetCommands = firstSet.Split(',');
            string[] secondSetCommands = secondSet.Split(',');
            MyLine[] firstSetLines = new MyLine[firstSetCommands.Length];
            MyLine[] secondSetLines = new MyLine[secondSetCommands.Length];
            int c = 0;
            int[] coordinate = { 0, 0 };
            foreach (string com in firstSetCommands)
            {
                //Store all lines in first set
                firstSetLines[c] = StoreLine(com, coordinate);
                c++;
            }
            coordinate[0] = 0; 
            coordinate[1] = 0;
            c = 0;
            foreach (string com in secondSetCommands)
            {
                //Store all lines in second set
                secondSetLines[c]= StoreLine(com, coordinate);
                c++;
            }
            var intersectionPoints = new List<IntersectionPoint>();
            int distanceFirstSet = 0;
            foreach (MyLine a in firstSetLines)
            {
                int distanceSecondSet = 0;
                foreach (MyLine b in secondSetLines)
                {
                    if (doesTheseInterSect(a, b))
                    {
                        //part 2 calculates the distance of the travelled path until intersection: 
                        int part2 = distanceFirstSet + distanceSecondSet + Math.Abs(a.startVariable - b.setValue) + Math.Abs(b.startVariable - a.setValue);
                        if (a.isXSet)
                        {
                            IntersectionPoint i = new IntersectionPoint(a.setValue, b.setValue, part2);
                            intersectionPoints.Add(i);
                        }
                        else
                        {
                            IntersectionPoint i = new IntersectionPoint(b.setValue, a.setValue, part2);
                            intersectionPoints.Add(i);
                        }
                    }
                    distanceSecondSet += (b.maxVarValue - b.minVarValue);
                }
                distanceFirstSet += (a.maxVarValue - a.minVarValue);
            }
            IntersectionPoint smallestDistance = new IntersectionPoint(1000000, 1000000, 1000000);
            foreach (IntersectionPoint i in intersectionPoints)
            {
                if(Math.Abs(i.x)+ Math.Abs(i.y) < smallestDistance.x+smallestDistance.y)
                {
                    smallestDistance.x = Math.Abs(i.x);
                    smallestDistance.y = Math.Abs(i.y);
                }
                if(i.z < smallestDistance.z)
                {
                    smallestDistance.z = i.z;
                }
            }
            System.Console.WriteLine("Answer: " + (smallestDistance.x + smallestDistance.y) + ", and " + smallestDistance.z);
        }

        public static MyLine StoreLine(string command, int[] coordinates)
        {
            MyLine myLine = new MyLine();
            switch (command.Substring(0,1))
            {
                case "U":
                    //up
                    myLine.isXSet = true;
                    myLine.setValue = coordinates[0];
                    myLine.startVariable = coordinates[1];
                    myLine.minVarValue = coordinates[1];
                    coordinates[1] += Int32.Parse(command.Substring(1, command.Length - 1));
                    myLine.maxVarValue = coordinates[1];
                    break;
                case "R":
                    //right
                    myLine.isXSet = false;
                    myLine.setValue = coordinates[1];
                    myLine.startVariable = coordinates[0];
                    myLine.minVarValue = coordinates[0];
                    coordinates[0] += Int32.Parse(command.Substring(1, command.Length - 1));
                    myLine.maxVarValue = coordinates[0];
                    break;
                case "D":
                    //down
                    myLine.isXSet = true;
                    myLine.setValue = coordinates[0];
                    myLine.startVariable = coordinates[1];
                    myLine.maxVarValue = coordinates[1];
                    coordinates[1] -= Int32.Parse(command.Substring(1, command.Length - 1));
                    myLine.minVarValue = coordinates[1];
                    break;
                case "L":
                    //left
                    myLine.isXSet = false;
                    myLine.setValue = coordinates[1];
                    myLine.startVariable = coordinates[0];
                    myLine.maxVarValue = coordinates[0];
                    coordinates[0] -= Int32.Parse(command.Substring(1, command.Length - 1));
                    myLine.minVarValue = coordinates[0];
                    break;
            }
            return myLine;
        }
    }
}

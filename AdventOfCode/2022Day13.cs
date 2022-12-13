using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace AdventOfCode
{
    class Day13_2022
    {
        public static void calculate()
        {
            // Part 1
            int lineNumber = 1;
            JsonNode jNode1 = null;
            int answerPart1 = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2022day13.txt"))
            {
                if (lineNumber % 3 == 1)
                {
                    //first line to compare
                    jNode1 = JsonNode.Parse(line);
                }
                else if (lineNumber % 3 == 2)
                {
                    //second line to compare
                    JsonNode jNode2 = JsonNode.Parse(line);
                    if (compareJson(jNode1, jNode2) >= 0)
                    {
                        // first line is smaller which is correct format
                        answerPart1 += ((lineNumber + 1) / 3);
                        System.Console.WriteLine("Seems right: " + ((lineNumber + 1) / 3));
                    }
                }
                lineNumber++;
            }

            // Part 2
            List<string> lines = System.IO.File.ReadLines("./../../../inputfiles/2022day13.txt").ToList();

            // Create a list of JsonNodes
            List<JsonNode> nodes = new List<JsonNode>();
            foreach (string line in lines)
            {
                if (line != "")
                    nodes.Add(JsonNode.Parse(line));
            }

            // Add the two extra lines
            JsonNode firstDividerPacket = JsonNode.Parse("[[2]]");
            JsonNode secondDividerPacket = JsonNode.Parse("[[6]]");
            nodes.Add(firstDividerPacket);
            nodes.Add(secondDividerPacket);

            // Use the sort function together with the sorter in part 1. This will result in a list with the largest first. 
            nodes.Sort(compareJson);

            // Get the index of the two new lines
            // since we've sorted "backwards", line x = number of lines - index x
            // if I had sorted with smallest first, line x = lindex x + 1 since first row has index 0. 
            int index1 = nodes.Count - nodes.IndexOf(firstDividerPacket);
            int index2 = nodes.Count - nodes.IndexOf(secondDividerPacket);

            int answerPart2 = index1 * index2;

            System.Console.WriteLine("Answer: " + answerPart1 + " and part 2: " + answerPart2);
        }

        // 1 = first node is smaller, which is the right order (actually any positive value)
        // 0 = they are the same, which is the right order 
        // -1 = second node is smaller, which is the wrong order (actually any negative value)
        public static int compareJson(JsonNode firstNode, JsonNode secondNode)
        {
            if (firstNode is JsonArray || secondNode is JsonArray)
            {
                JsonArray firstArray = null;
                if (firstNode is JsonValue)
                {
                    // If one is an array and the other a value, transform the value into an array
                    firstArray = new JsonArray((int)firstNode);
                }
                else
                {
                    firstArray = (JsonArray)firstNode;
                }

                JsonArray secondArray = null;
                if (secondNode is JsonValue)
                {
                    secondArray = new JsonArray((int)secondNode);
                }
                else
                {
                    secondArray = (JsonArray)secondNode;
                }

                // go through the components in the array one by one
                int comparator = 0;
                // find the length of the shortest array 
                int count = firstArray.Count <= secondArray.Count ? firstArray.Count : secondArray.Count;
                for (int index = 0; index < count; index++)
                {
                    comparator = compareJson(firstArray[index], secondArray[index]);
                    if (comparator != 0)
                        return comparator;
                }
                // if both are the same then check length, [1,2,3,4] > [1,2,3]
                return (secondArray.Count - firstArray.Count);
            }
            else
            {
                // The nodes are either values or arrays and since both were not arrays, they are values. 
                // We can just convert to int and return the difference
                return (int)secondNode - (int)firstNode;
            }
        }
    }
}

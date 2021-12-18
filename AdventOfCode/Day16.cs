using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AdventOfCode
{
    class Day16
    {
        public static void calculate()
        {
            string input = File.ReadAllText("./../../../inputfiles/day16.txt");
            // Translate into bits
            string inputAsBits = "";
            foreach (char c in input)
            {
                string thisHex = Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
                inputAsBits += thisHex;
            }
            ReturnValues rv = ReadPackage(inputAsBits);
            System.Console.WriteLine("Answer part 1: " + rv.version + ", and part 2: " + rv.value);
        }

        //Returning version number
        public static ReturnValues ReadPackage(string packageText)
        {
            int index = 0;
            BigInteger value = 0; // For literal value, we just return the value. For others, we'll calculate and return
            // Read three bits = version
            string version = packageText.Substring(index, 3);
            int accumulatedVersion = Convert.ToInt32(version, 2);
            index += 3;
            List<BigInteger> valuesInSubpackets = new List<BigInteger>();
            // Read three bits = type ID (4 = literal value, all other are operator)
            string typeIDBin = packageText.Substring(index, 3);
            int typeIDDec = Convert.ToInt32(typeIDBin, 2);
            index += 3;
            if (typeIDDec == 4)
            {
                // Packet type 4 = Read 5 bits. If the first bit = 1, continue afterwards. Otherwise this is the last value. 
                // The following 4 bits = the value. 
                do
                {
                    value *= 16; // if we open another package, it has 4 bits => the first one is 16 times larger. 
                    string valueText = packageText.Substring(index + 1, 4);
                    int valueTextBin = Convert.ToInt32(valueText, 2);
                    value += valueTextBin;
                    index += 5;
                }
                while (packageText.Substring(index - 5, 1) == "1");
            }
            else
            {
                // If packet type != 4, next bit is length type
                // ID. 0 = 15 bits = a number.
                // ID. 1 = 11 bits = number of sub-packets
                string lengthTypeID = packageText.Substring(index, 1);
                index++;
                if (lengthTypeID == "0")
                {
                    // If the length type ID is 0, then the next 15 bits are a number that
                    // represents the total length in bits of the sub-packets contained by this packet.
                    int totalLengthInBits = Convert.ToInt32(packageText.Substring(index, 15), 2);
                    index += 15;
                    int tmpindex = 0;
                    while (tmpindex < totalLengthInBits)
                    {
                        string subPacket = packageText.Substring(index + tmpindex); // TODO: Limit length
                        ReturnValues tmp = ReadPackage(subPacket);
                        accumulatedVersion += tmp.version;
                        valuesInSubpackets.Add(tmp.value);
                        tmpindex += tmp.index;
                    }
                    index += totalLengthInBits; 

                }
                else if (lengthTypeID == "1")
                {
                    // If the length type ID is 1, then the next 11 bits are a number that represents
                    // the number of sub-packets immediately contained by this packet.
                    int totalLengthInPackages = Convert.ToInt32(packageText.Substring(index, 11), 2);
                    index += 11;
                    for (int i = 0; i < totalLengthInPackages; i++)
                    {
                        ReturnValues tmp = ReadPackage(packageText.Substring(index));
                        accumulatedVersion += tmp.version;
                        valuesInSubpackets.Add(tmp.value);
                        index += tmp.index;
                    }
                }
                // Calculate
                if (typeIDDec == 0)
                {
                    // SUM
                    foreach (BigInteger i in valuesInSubpackets)
                        value += i;
                }
                else if (typeIDDec == 1)
                {
                    // Multiply
                    bool first = true;
                    foreach (BigInteger i in valuesInSubpackets)
                    {
                        if (first)
                        {
                            value = i;
                            first = false;
                        }
                        else
                            value *= i;
                    }
                }
                else if (typeIDDec == 2)
                {
                    // minimum
                    value = new BigInteger(9223372036854775807); // max BigInteger
                    foreach (BigInteger i in valuesInSubpackets)
                        if (i < value)
                            value = i;
                }
                else if (typeIDDec == 3)
                {
                    // MAXIMUM
                    foreach (BigInteger i in valuesInSubpackets)
                        if (i > value)
                            value = i;
                }
                else if (typeIDDec == 5)
                {
                    // GREATER THAN
                    if (valuesInSubpackets[0] > valuesInSubpackets[1])
                        value = 1;
                    else
                        value = 0;
                }
                else if (typeIDDec == 6)
                {
                    // LESS THAN
                    if (valuesInSubpackets[0] < valuesInSubpackets[1])
                        value = 1;
                    else
                        value = 0;
                }
                else if (typeIDDec == 7)
                {
                    // EQUAL TO
                    if (valuesInSubpackets[0] == valuesInSubpackets[1])
                        value = 1;
                    else
                        value = 0;
                }
            }
            return new ReturnValues(accumulatedVersion, index, value);
        }

    }
}

record ReturnValues(int version, int index, BigInteger value);

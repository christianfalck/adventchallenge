using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day19_2021
    {
        // TODO: Only 2 hours of sleep due to sick kids = I won't make this a static solution as with the previous.
        // Also, I got some ideas for how to handle the rotation from the solution megathread at Reddit. 
        Dictionary<int, HashSet<Location>> scannersBeacons; // The scanners and all the corresponding beacons
        HashSet<Location> listOfBeacons; // All beacons in the system
        Dictionary<int, Location> scannerLocations; // All scanners in the system
        public void calculate()
        {
            HashSet<Location> beacons = null;
            scannersBeacons = new();
            scannerLocations = new();

            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2021Day19.txt"))
            {
                if (line.StartsWith("---"))
                {
                    beacons = new();
                    scannersBeacons.Add(int.Parse(Regex.Matches(line, @"(\d+)")[0].ToString()), beacons);
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    // Do nothing
                }
                else
                {
                    beacons.Add(new Location(
                    int.Parse(line.Substring(0, line.IndexOf(','))),
                    int.Parse(line.Substring(line.IndexOf(',') + 1, line.LastIndexOf(',') - line.IndexOf(',') - 1)),
                    int.Parse(line.Substring(line.LastIndexOf(',') + 1))));
                }
            }
            listOfBeacons = new HashSet<Location>(scannersBeacons[0]); // add all the beacons from the first scanner
            scannerLocations.Add(0, new Location(0, 0, 0)); // add the first scanner at Location 0, 0, 0

            // Using the fact that the difference between two beacons are unique (it was my theory at first but then confirmed when it worked)
            // The first scanner and it's beacons are used to compare. Go through the rest of the scanners and rotate them in the 24
            // different ways in order to see which one that has 12 identical distances. 
            // Check where that scanner is in relation to the first one and use that difference to align all the beacons from the second scanner.

            // Start by adding the differences between the beacons for the first scanner
            Dictionary<Location, Location> differencesBetweenAllBeacons = CalculateDifferences(listOfBeacons);
            Queue<int> scannersToCheck = new();
            for (int i = 1; i < scannersBeacons.Count; i++)
            {
                scannersToCheck.Enqueue(i);
            }
            // If two scanners have 12 similar beacons, they're close and we'll add the second one.
            // Otherwise, move on to next scanner until we find a scanner where 12 beacons are the same. 
            while (scannersToCheck.Count > 0)
            {
                int scanner = scannersToCheck.Dequeue();
                var thisScannersBeacons = scannersBeacons[scanner];
                Func<Location, Location> scannerRotation = null;
                Location howToMove = null;
                foreach (var rotation in GetRotations())
                {
                    if (TestRotation(differencesBetweenAllBeacons, thisScannersBeacons, rotation, out howToMove))
                    {
                        scannerRotation = rotation;
                        break;
                    }
                }
                if (scannerRotation != null)
                {
                    var rotated = RotateBeacons(thisScannersBeacons, scannerRotation);
                    var aligned = AlignBeacons(rotated, howToMove);

                    foreach (Location beacon in aligned)
                    {
                        listOfBeacons.Add(beacon);
                    }
                    differencesBetweenAllBeacons = CalculateDifferences(listOfBeacons);
                    scannerLocations.Add(scanner, howToMove);
                }
                else
                {
                    scannersToCheck.Enqueue(scanner);
                }
            }

            int answer1 = listOfBeacons.Count;

            //Part 2

            HashSet<(int, int)> tested = new();
            int answer2 = 0; // Longest distance between two beacons

            foreach (int scannerID in scannerLocations.Keys)
                foreach (int compareScannerID in scannerLocations.Keys)
                {
                    if (scannerID != compareScannerID)
                    {
                        (int, int) firstPair = (scannerID, compareScannerID);
                        (int, int) secondPair = (compareScannerID, scannerID);

                        if (!tested.Contains(firstPair) && !tested.Contains(secondPair))
                        {
                            // If we've checked the distasnce between two scanners, we don't have to test the opposite
                            int distance = scannerLocations[scannerID].ManhattanDistance(scannerLocations[compareScannerID]);
                            if (distance > answer2)
                            {
                                answer2 = distance;
                            }
                            tested.Add(firstPair); // we only have to add one of them since we check both directions
                        }
                    }
                }

            System.Console.WriteLine("Answer: " + answer1 + ", and " + answer2);
        }

        // Returns a Dictionary where key = the difference between two beacons and value = one of the beacons. 
        // With these you can get to the other beacon. Note that b1.Difference(b2)!= b2.Difference(b1)
        private static Dictionary<Location, Location> CalculateDifferences(HashSet<Location> beacons)
        {
            Dictionary<Location, Location> differences = new();
            foreach (var beacon1 in beacons)
            {
                foreach (var beacon2 in beacons)
                {
                    if (beacon1 != beacon2)
                    {
                        Location difference = beacon2.Difference(beacon1);
                        if (!differences.ContainsKey(difference))
                            differences.Add(difference, beacon2);
                    }
                }
            }
            return differences;
        }

        // Try rotating the set of beacons for the current scanner and check if 12 of them are the same as the ones we've already aligned
        private static bool TestRotation(Dictionary<Location, Location> allBeaconDifferences, HashSet<Location> thisScannerBeacons, Func<Location, Location> rotation, out Location howToMove)
        {
            int counter = 0;
            foreach (var firstBeacon in thisScannerBeacons)
            {
                Location firstBeaconRotated = rotation(firstBeacon);
                foreach (var secondBeacon in thisScannerBeacons)
                {
                    if (firstBeacon != secondBeacon)
                    {
                        Location secondBeaconRotated = rotation(secondBeacon);
                        Location difference = firstBeaconRotated.Difference(secondBeaconRotated);
                        // If the differences between the two beacons for this scanner is the same as one of the differences
                        // we've found previously, add 1 to counter. If we have 12 similar differences, we'll use this rotation.
                        if (allBeaconDifferences.ContainsKey(difference) && ++counter == 11)
                        {
                            howToMove = firstBeaconRotated.Difference(allBeaconDifferences[difference]);
                            return true; // We found a match
                        }
                    }
                }
            }
            // There wasn't 12 similar distances. Move on to next scanner and look at this later. 
            howToMove = null;
            return false;
        }

        // When we know how to rotate the scanner to align with the first scanner, 
        // this function rotates all beacons the same way
        private static HashSet<Location> RotateBeacons(HashSet<Location> beacons, Func<Location, Location> scannerRotation)
        {
            HashSet<Location> rotatedBeacons = new();
            foreach (Location beacon in beacons)
            {
                rotatedBeacons.Add(scannerRotation(beacon));
            }
            return rotatedBeacons;
        }

        // How to move from the first scanner to this one is used to align all beacons with the first scanner
        // This way, we know where all beacons are in relation to the first scanner
        private static HashSet<Location> AlignBeacons(HashSet<Location> beacons, Location howToMove)
        {
            HashSet<Location> alignedBeacons = new();
            foreach (Location beacon in beacons)
            {
                alignedBeacons.Add(beacon.AddDifference(howToMove));
            }
            return alignedBeacons;
        }

        // This is all the ways a scanner can rotate (24 combinations are not possible in the 3D space and x,y,z is the default)
        private static IEnumerable<Func<Location, Location>> GetRotations()
        {
            yield return v => new(v.X, -v.Z, v.Y);
            yield return v => new(v.X, -v.Y, -v.Z);
            yield return v => new(v.X, v.Z, -v.Y);

            yield return v => new(-v.Y, v.X, v.Z);
            yield return v => new(v.Z, v.X, v.Y);
            yield return v => new(v.Y, v.X, -v.Z);
            yield return v => new(-v.Z, v.X, -v.Y);

            yield return v => new(-v.X, -v.Y, v.Z);
            yield return v => new(-v.X, -v.Z, -v.Y);
            yield return v => new(-v.X, v.Y, -v.Z);
            yield return v => new(-v.X, v.Z, v.Y);

            yield return v => new(v.Y, -v.X, v.Z);
            yield return v => new(v.Z, -v.X, -v.Y);
            yield return v => new(-v.Y, -v.X, -v.Z);
            yield return v => new(-v.Z, -v.X, v.Y);

            yield return v => new(-v.Z, v.Y, v.X);
            yield return v => new(v.Y, v.Z, v.X);
            yield return v => new(v.Z, -v.Y, v.X);
            yield return v => new(-v.Y, -v.Z, v.X);

            yield return v => new(-v.Z, -v.Y, -v.X);
            yield return v => new(-v.Y, v.Z, -v.X);
            yield return v => new(v.Z, v.Y, -v.X);
            yield return v => new(v.Y, -v.Z, -v.X);
        }
    }

    // Beacons and scanners have a location, but the difference between two object is also a represented this way (x.diff, etc.)
    record Location(int X, int Y, int Z)
    {
        // Calculate the Manhattan distance between two objects
        public int ManhattanDistance(Location beacon) => Math.Abs(beacon.X - X) + Math.Abs(beacon.Y - Y) + Math.Abs(beacon.Z - Z);

        // How to travel from one object to another
        public Location Difference(Location beacon) => new(beacon.X - X, beacon.Y - Y, beacon.Z - Z);

        // Using the difference to calculate the position
        internal Location AddDifference(Location diff) => new(X + diff.X, Y + diff.Y, Z + diff.Z);
    }

}

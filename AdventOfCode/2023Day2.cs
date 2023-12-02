using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day2_2023
    {
        public static void calculate()
        {
            int answer = 0;
            int answer2 = 0;
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2023day2.txt"))
            {
                // Max number of balls according to the assignment
                int redLimit = 12;
                int greenLimit = 13;
                int blueLimit = 14;

                List<Game> games = ParseInputString(line);

                foreach (var game in games)
                {
                    // Each game is possible until proven otherwise
                    bool gamePossible = true;
                    // Used for part 2 where we want to identify the least number of balls
                    int minimumRed = 0;
                    int minimumBlue = 0;
                    int minimumGreen = 0;

                    foreach (var round in game.Rounds)
                    {
                        foreach (var item in round.Items)
                        {
                            if (item.Color == "red")
                            {
                                if(item.Quantity > redLimit)
                                {
                                    // Too many
                                    gamePossible = false;
                                } 
                                if(item.Quantity > minimumRed)
                                {
                                    // New minimum
                                    minimumRed = item.Quantity;
                                }
                            }
                            if (item.Color == "blue") 
                            {
                                if(item.Quantity > blueLimit)
                                {
                                    // Too many
                                    gamePossible = false;
                                }
                                if (item.Quantity > minimumBlue)
                                {
                                    // New minimum
                                    minimumBlue = item.Quantity;
                                }
                            }
                            if (item.Color == "green")
                            {
                                if (item.Quantity > greenLimit)
                                {
                                    // Too many
                                    gamePossible = false;
                                }
                                if (item.Quantity > minimumGreen)
                                {
                                    // New minimum
                                    minimumGreen = item.Quantity;
                                }
                            }
                        }
                    }
                    if (gamePossible)
                    {
                        // For part 1 we add the ID of the games that are possible
                        answer += game.Number;
                    }
                    // For part 2 we add the product of the least number of required balls for each game
                    answer2 += (minimumRed * minimumBlue * minimumGreen);
                }
            }
            Console.WriteLine("Answer: " + answer + " and part 2: " + answer2);
        }
        static List<Game> ParseInputString(string input)
        {
            List<Game> games = new List<Game>();

            // Use regular expression to match each game and its rounds
            MatchCollection gameMatches = Regex.Matches(input, @"Game (\d+):([^;]+(?:;[^;]+)*)");

            foreach (Match gameMatch in gameMatches)
            {
                int gameNumber = int.Parse(gameMatch.Groups[1].Value);
                string roundsString = gameMatch.Groups[2].Value.Trim();

                Game game = new Game { Number = gameNumber, Rounds = ParseRounds(roundsString) };
                games.Add(game);
            }

            return games;
        }

        static List<Round> ParseRounds(string roundsString)
        {
            List<Round> rounds = new List<Round>();

            // Use regular expression to match each round in the roundsString
            MatchCollection roundMatches = Regex.Matches(roundsString, @"(\d+ [a-zA-Z]+(?:, \d+ [a-zA-Z]+)*)");

            int roundNumber = 1;
            foreach (Match roundMatch in roundMatches)
            {
                string itemsString = roundMatch.Groups[1].Value;
                Round round = new Round { Number = roundNumber++, Items = ParseItems(itemsString) };
                rounds.Add(round);
            }

            return rounds;
        }

        static List<Item> ParseItems(string itemsString)
        {
            List<Item> items = new List<Item>();

            // Use regular expression to match each item in the itemsString
            MatchCollection itemMatches = Regex.Matches(itemsString, @"(\d+) ([a-zA-Z]+)");

            foreach (Match itemMatch in itemMatches)
            {
                int quantity = int.Parse(itemMatch.Groups[1].Value);
                string color = itemMatch.Groups[2].Value;

                Item item = new Item { Quantity = quantity, Color = color };
                items.Add(item);
            }

            return items;
        }
    }

    class Game
    {
        public int Number { get; set; }
        public List<Round> Rounds { get; set; }
    }

    class Round
    {
        public int Number { get; set; }
        public List<Item> Items { get; set; }
    }

    class Item
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
    }

}
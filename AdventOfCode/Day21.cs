using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode
{
    class Day21
    {

        BigInteger player1_wins;
        BigInteger player2_wins;

        Dictionary<int, int> oddsForNext3throws;

        public Day21()
        {

            int player1Position = 8;
            int player2Position = 10;

            int player1score = 0;
            int player2score = 0;

            int dice = 0;
            int diceThrows = 0;

            int answer1 = 0;
            BigInteger answer2 = 0;

            // Part 1
            while (player1score < 1000 && player2score < 1000)
            {
                for (int i = 0; i < 3; i++)
                {
                    diceThrows++;
                    if (dice < 100)
                    {
                        dice++;
                    }
                    else
                    {
                        dice = 1;
                    }
                    player1Position = (player1Position + dice) % 10;
                    if (player1Position == 0)
                        player1Position = 10;
                }
                player1score += player1Position;
                if (player1score >= 1000)
                    answer1 = diceThrows * player2score;
                for (int i = 0; i < 3; i++)
                {
                    diceThrows++;
                    if (dice < 100)
                    {
                        dice++;
                    }
                    else
                    {
                        dice = 1;
                    }
                    player2Position = (player2Position + dice) % 10;
                    if (player2Position == 0)
                        player2Position = 10;
                }
                player2score += player2Position;
                if (player2score >= 1000)
                    answer1 = diceThrows * player1score;
            }

            // Part 2
            player1Position = 8;
            player2Position = 10;

            player1_wins = 0;
            player2_wins = 0;

            /**
             * +3 = 1/27
             * +4 = 3/27
             * +5 = 6/27
             * +6 = 7/27
             * +7 = 6/27
             * +8 = 3/27
             * +9 = 1/27
             */
            oddsForNext3throws = new();
            oddsForNext3throws.Add(3, 1);
            oddsForNext3throws.Add(4, 3);
            oddsForNext3throws.Add(5, 6);
            oddsForNext3throws.Add(6, 7);
            oddsForNext3throws.Add(7, 6);
            oddsForNext3throws.Add(8, 3);
            oddsForNext3throws.Add(9, 1);

            //makeRound(player1setup, player2setup, true, 1, 1);
            makeRound2(player1Position, player2Position, 0, 0, 1, true);

            answer2 = player1_wins > player2_wins ? player1_wins : player2_wins;

            System.Console.WriteLine("Answer: " + answer1 + ", and " + answer2);
        }


        public void makeRound2(int player1position, int player2position, int player1score, int player2score, BigInteger occurences, bool player1)
        {
            foreach (int nextSteps in oddsForNext3throws.Keys)
            {
                if (player1)
                {
                    int landOnTile = (player1position + nextSteps) % 10;
                    if (landOnTile == 0)
                        landOnTile = 10;
                    int score = player1score + landOnTile;
                    BigInteger probability = occurences * oddsForNext3throws[nextSteps];
                    // If the player wins, add those numbers. 
                    if (score >= 21)
                    {
                        player1_wins += probability;
                    }
                    else
                    {
                        makeRound2(landOnTile, player2position, score, player2score, probability, false);
                    }
                }
                else
                {
                    int landOnTile = (player2position + nextSteps) % 10;
                    if (landOnTile == 0)
                        landOnTile = 10;
                    int score = player2score + landOnTile;
                    BigInteger probability = occurences * oddsForNext3throws[nextSteps];
                    // If the player wins, add those numbers. 
                    if (score >= 21)
                    {
                        player2_wins += probability;
                    }
                    else
                    {
                        makeRound2(player1position, landOnTile, player1score, score, probability, true);
                    }
                }
            }
        }
    }
}

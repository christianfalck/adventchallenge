using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode
{
    class Day21
    {

        BigInteger player1_wins;
        BigInteger player2_wins;

        Dictionary<int, int> oddsForNext3throws;

        Dictionary<(int, int), BigInteger> check; // Key: Player, round. Value: Number of wins that round

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

            check = new();

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
            List<RoundStatistics> player1setup = new();
            List<RoundStatistics> player2setup = new();
            player1setup.Add(new RoundStatistics(player1Position, 0, 1));
            player2setup.Add(new RoundStatistics(player2Position, 0, 1));

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
            makeRound2(player1Position, player2Position, 0, 0, 1, true, 1);

            answer2 = player1_wins > player2_wins ? player1_wins : player2_wins;

            System.Console.WriteLine("Answer: " + answer1 + ", and " + answer2);
        }


        public void makeRound2(int player1position, int player2position, int player1score, int player2score, BigInteger occurences, bool player1, int round)
        {
            foreach (int nextSteps in oddsForNext3throws.Keys)
            {
                if (player1)
                {
                    int landOnTile = (player1position + nextSteps)%10;
                    if (landOnTile == 0)
                        landOnTile = 10;
                    int score = player1score + landOnTile;
                    BigInteger probability = occurences * oddsForNext3throws[nextSteps];
                    // If the player wins, add those numbers. 
                    if (score >= 21)
                    {
                        player1_wins += probability;
                        // These 4 rows can be removed when it works
                        if (check.ContainsKey((1, round)))
                            check[(1, round)] += probability;
                        else
                            check.Add((1, round), probability);
                    }
                    else
                    {
                        makeRound2(landOnTile, player2position, score, player2score, probability, false, round + 1);
                    }
                }
                else
                {
                    int landOnTile = (player2position + nextSteps)%10;
                    if (landOnTile == 0)
                        landOnTile = 10;
                    int score = player2score + landOnTile;
                    BigInteger probability = occurences * oddsForNext3throws[nextSteps];
                    // If the player wins, add those numbers. 
                    if (score >= 21)
                    {
                        player2_wins += probability;
                        // These 4 rows can be removed when it works
                        if (check.ContainsKey((2, round)))
                            check[(2, round)] += probability;
                        else
                            check.Add((2, round), probability);
                    }
                    else
                    {
                        makeRound2(player1position, landOnTile, player1score, score, probability, true, round + 1);
                    }
                }

            }
        }

        public void makeRound(List<RoundStatistics> previousRoundThisPlayer, List<RoundStatistics> previousRoundOtherPlayer, bool player1, BigInteger oddsOtherPlayerDidntWinLastround, int round)
        {
            foreach (RoundStatistics roundStatistic in previousRoundThisPlayer)
            {
                // All starting positions for next round
                List<RoundStatistics> thisRound = new();
                BigInteger didntWin = 0;
                // Example: Position 1, score 1, nbr = 1
                // Another example: Position 7, score 12, nbr 872312
                foreach (int nextSteps in oddsForNext3throws.Keys)
                {
                    // We know how many occurences that lead us here. Then we'll continue with the statistics for the following 3 throws. 
                    int landOnTile = (roundStatistic.position + nextSteps) % 10;
                    if (landOnTile == 0)
                        landOnTile = 10;
                    int score = roundStatistic.score + landOnTile;
                    BigInteger probability = roundStatistic.numberOfOccurences * oddsForNext3throws[nextSteps] * oddsOtherPlayerDidntWinLastround;
                    // If the player wins, add those numbers. 
                    if (score >= 21)
                    {
                        if (player1)
                        {
                            player1_wins += probability;
                            if (check.ContainsKey((1, round)))
                                check[(1, round)] += probability;
                            else
                                check.Add((1, round), probability);
                        }
                        else
                        {
                            player2_wins += probability;
                            if (check.ContainsKey((2, round)))
                                check[(2, round)] += probability;
                            else
                                check.Add((2, round), probability);
                        }
                    }
                    else // Add this outcome to the list for next iteration and all those outcomes also result in next round for other player
                    {
                        thisRound.Add(new RoundStatistics(landOnTile, score, probability));
                        didntWin += oddsForNext3throws[nextSteps]; // how many of the 27 different outcomes result in that this player doesn't win
                    }
                }
                if (didntWin > 0)
                    makeRound(previousRoundOtherPlayer, thisRound, !player1, didntWin, round++);
            }
        }
    }

    class RoundStatistics
    {
        public RoundStatistics(int position, int score, BigInteger numberOfOccurences)
        {
            this.position = position;
            this.score = score;
            this.numberOfOccurences = numberOfOccurences;
        }
        public int position;
        public int score;
        public BigInteger numberOfOccurences;
    }

}

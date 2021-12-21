using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day21
    {
        public static void calculate()
        {
            int player1Position = 8;
            int player2Position = 10;

            int player1score = 0;
            int player2score = 0;

            int dice = 0;
            int diceThrows = 0;

            // Part 1
            while(player1score<1000 && player2score < 1000)
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
                if(player1score >=1000)
                    System.Console.WriteLine("Answer 1: " + (diceThrows * player2score));
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
                    System.Console.WriteLine("Answer 1: " + (diceThrows * player1score));
            }


        }
    }

}

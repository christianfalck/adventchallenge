using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day7_2023
    {
        public static void calculate()
        {
            BigInteger answer = 0;
            List<pokerHand> hands = new List<pokerHand>();
            foreach (string line in System.IO.File.ReadLines("./../../../inputfiles/2023day7.txt"))
            {
                String[] handandvalue = line.Split(" ");
                pokerHand hand = new pokerHand();
                hand.cards = handandvalue[0];
                hand.bid = int.Parse(handandvalue[1]);
                CalculateValue(hand);
                hands.Add(hand);
            }
            hands.Sort((hand1, hand2) => CompareHands(hand1, hand2));
            int i = hands.Count;
            foreach (pokerHand hand in hands)
            {
                answer += hand.bid * i;
                i--;
            }
            System.Console.WriteLine("Answer: " + answer + ", and " + 252657399);
        }

        // returns a negative number if hand1 is larger than hand2,
        // a positive number if hand 2 is larger and 0 if they are the same
        static int CompareHands(pokerHand hand1, pokerHand hand2)
        {
            if (hand1.value != hand2.value)
            {
                // One has three of a kind, the other two pairs for instance
                return hand2.value - hand1.value;
            }
            // Below is for when they had the same value, then we go on to compare individual cards
            for (int a = 0; a <= 3; a++)
            {
                if (ValueOfCard(hand1.cards[a]) != ValueOfCard(hand2.cards[a]))
                {
                    return ValueOfCard(hand2.cards[a]) - ValueOfCard(hand1.cards[a]);
                }
            }
            return ValueOfCard(hand2.cards[4]) - ValueOfCard(hand1.cards[4]);
        }

        static int ValueOfCard(char card)
        {
            if (card == 'A')
                return 14;
            if (card == 'K')
                return 13;
            if (card == 'Q')
                return 12;
            if (card == 'J')
                return 11;
            if (card == 'T')
                return 10;
            return card - '0';
        }

        // Five of a kind = 7p, Four of a kind = 6p, Full house = 5p, 
        // three of a kind = 4p, two pairs = 3p, pair = 2p, just cards = 1p
        static void CalculateValue(pokerHand hand)
        {
            Dictionary<char, int> cardCount = new Dictionary<char, int>();
            foreach (char c in hand.cards)
            {
                if (!cardCount.ContainsKey(c))
                {
                    cardCount[c] = 1;
                }
                else
                {
                    cardCount[c]++;
                }
            }
            if (cardCount.Keys.Count == 1)
            {
                // Five of a kind
                hand.value = 7;
                return;
            }
            else if (cardCount.Keys.Count == 2)
            {
                if (cardCount.First().Value == 4 || cardCount.First().Value == 1)
                {
                    // Four of a kind
                    hand.value = 6;
                    return;
                }
                else
                {
                    // Full house
                    hand.value = 5;
                    return;
                }
            }
            else if (cardCount.Keys.Count == 3)
            {
                if (cardCount.First().Value == 3 || cardCount.ElementAt(1).Value == 3 || cardCount.ElementAt(2).Value == 3)
                {
                    // three of a kind
                    hand.value = 4;
                    return;
                }
                else
                {
                    // two pairs
                    hand.value = 3;
                    return;
                }
            }
            else if (cardCount.Keys.Count == 5)
            {
                // Nothing, just separate cards
                hand.value = 1;
                return;
            }
            else
            {
                // if nothing else, it's obviously a pair
                hand.value = 2;
                return;
            }
        }
    }
    class pokerHand
    {
        public string cards { get; set; }
        public int value { get; set; }
        public int bid { get; set; }
    }

}

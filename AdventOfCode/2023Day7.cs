using System;
using System.Collections.Generic;
using System.Text;
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
                return hand2.value - hand1.value;
            }
            // TODO write a while loop where I increase i=0 with one each loop
            if (ValueOfCard(hand1.cards[0]) != ValueOfCard(hand2.cards[0]))
            {
                return ValueOfCard(hand2.cards[0]) - ValueOfCard(hand1.cards[0]);
            }
            if (ValueOfCard(hand1.cards[1]) != ValueOfCard(hand2.cards[1]))
            {
                return ValueOfCard(hand2.cards[1]) - ValueOfCard(hand1.cards[1]);
            }
            if (ValueOfCard(hand1.cards[2]) != ValueOfCard(hand2.cards[2]))
            {
                return ValueOfCard(hand2.cards[2]) - ValueOfCard(hand1.cards[2]);
            }
            if (ValueOfCard(hand1.cards[3]) != ValueOfCard(hand2.cards[3]))
            {
                return ValueOfCard(hand2.cards[3]) - ValueOfCard(hand1.cards[3]);
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
            if (card == '9')
                return 9;
            if (card == '8')
                return 8;
            if (card == '7')
                return 7;
            if (card == '6')
                return 6;
            if (card == '5')
                return 5;
            if (card == '4')
                return 4;
            if (card == '3')
                return 3;
            if (card == '2')
                return 2;
            // Should never get here
            return 0;
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

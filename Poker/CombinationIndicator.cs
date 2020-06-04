using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Poker
{
    public class CombinationIndicator
    {
        public static bool IsFlush(List<Card> hand)
        {
            int clubs = 0, diamonds = 0, hearts = 0, spades = 0;
            foreach (Card card in hand)
            {
                if (card.Suit == Suit.Clubs)
                {
                    clubs++;
                }
                if (card.Suit == Suit.Diamonds)
                {
                    diamonds++;
                }
                if (card.Suit == Suit.Hearts)
                {
                    hearts++;
                }
                if (card.Suit == Suit.Spades)
                {
                    spades++;
                }
            }

            return clubs >= 5 || diamonds >= 5 || hearts >= 5 || spades >= 5;
        }

        public static bool IsStraight(List<Card> hand)
        {
            hand = hand.OrderBy(item => (int)item.Rank).ToList<Card>();

            int nextRank = (int)hand[0].Rank + 1;
            int sequances = 0;

            for (int i = 1; i < hand.Count(); i++)
            {
                if ((int)hand[i].Rank == nextRank)
                {
                    
                    nextRank++;
                    sequances++;
                    if(sequances == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    nextRank = (int)hand[i].Rank + 1;
                    sequances = 0;
                }
            }

            return false; ;
        }

        public static bool IsStraightFlush(List<Card> hand)
        {
            hand = hand.OrderBy(item => (int)item.Rank).ToList<Card>();

            int nextRank = (int)hand[0].Rank + 1;
            int nextSuit = (int)hand[0].Suit;
            int sequances = 0;

            for (int i = 1; i < hand.Count(); i++)
            {
                if ((int)hand[i].Rank == nextRank && ((int)hand[i].Suit == nextSuit))
                {
                    nextRank++;
                    sequances++;
                    if(sequances == 4)
                    {
                        return true;
                    }

                }
                else
                {
                    nextRank = (int)hand[i].Rank + 1;
                    nextSuit = (int)hand[i].Suit;
                    sequances = 0;
                }
            }

            return false;
        }

        public static bool IsRoyalFlush(List<Card> hand)
        {
            return IsStraightFlush(hand) && hand[6].Rank == Rank.Ace;
        }

        public static bool IsFullHouse(List<Card> hand)
        {
            hand = hand.OrderBy(item => (int)item.Rank).ToList<Card>();

            bool lowFullHouse = (int)hand[0].Rank == (int)hand[1].Rank &&
                                (int)hand[1].Rank == (int)hand[2].Rank &&
                                (int)hand[3].Rank == (int)hand[4].Rank;

            bool middleFullHouse = (int)hand[1].Rank == (int)hand[2].Rank &&
                                (int)hand[2].Rank == (int)hand[3].Rank &&
                                (int)hand[4].Rank == (int)hand[5].Rank;

            bool highFullHouse = (int)hand[2].Rank == (int)hand[3].Rank &&
                                (int)hand[3].Rank == (int)hand[4].Rank &&
                                (int)hand[5].Rank == (int)hand[6].Rank;

            bool reLowFullHouse = (int)hand[6].Rank == (int)hand[5].Rank &&
                                (int)hand[5].Rank == (int)hand[4].Rank &&
                                (int)hand[3].Rank == (int)hand[2].Rank;

            bool reMiddleFullHouse = (int)hand[5].Rank == (int)hand[4].Rank &&
                                (int)hand[4].Rank == (int)hand[3].Rank &&
                                (int)hand[2].Rank == (int)hand[1].Rank;

            bool reHighHouse = (int)hand[4].Rank == (int)hand[3].Rank &&
                                (int)hand[3].Rank == (int)hand[2].Rank &&
                                (int)hand[1].Rank == (int)hand[0].Rank;

            return lowFullHouse || middleFullHouse || highFullHouse ||
                   reLowFullHouse || reMiddleFullHouse || reHighHouse;

        }

        public static bool IsFourOfKind(List<Card> hand)
        {
            int[] amountOfRanks = new int[13];

            for(int i = 0; i < 13; i++)
            {
                amountOfRanks[i] = 0;
            }

            foreach (Card card in hand)
            {
                amountOfRanks[(int)card.Rank]+=1;
            }

            foreach (int el in amountOfRanks)
            {
                if (el == 4)
                {
                    return true;
                }
            }
            return false;

        }

        public static bool IsThreeOfAKind(List<Card> hand)
        {
            if (IsFourOfKind(hand) || IsFullHouse(hand))
            {
                return false;
            }

            int[] amountOfRanks = new int[13];

            foreach (Card card in hand)
            {
                amountOfRanks[(int)card.Rank]++;
            }

            foreach (int el in amountOfRanks)
            {
                if (el == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsTwoPair(List<Card> hand)
        {
            if(IsThreeOfAKind(hand) || IsFourOfKind(hand) || IsFullHouse(hand))
            {
                return false;
            }

            int[] amountOfRanks = new int[13];
            int amountOfPairs = 0;
            foreach (Card card in hand)
            {
                amountOfRanks[(int)card.Rank]++;
            }

            foreach (int el in amountOfRanks)
            {
                if (el == 2)
                {
                    amountOfPairs++;
                }
            }
            return amountOfPairs==2;
        }

        public static bool isPair(List<Card> hand)
        {
            if(IsTwoPair(hand) || IsThreeOfAKind(hand) || IsFourOfKind(hand) || IsFullHouse(hand))
            {
                return false;
            }

            int[] amountOfRanks = new int[13];

            foreach (Card card in hand)
            {
                amountOfRanks[(int)card.Rank]++;
            }

            foreach (int el in amountOfRanks)
            {
                if (el == 2)
                {
                    return true;
                }
            }
            return false;
        }

        public static Combination getCombination(List<Card> hand)
        {
            if (IsRoyalFlush(hand))
                return Combination.RoyalFlush;
            if (IsStraightFlush(hand))
                return Combination.StraightFlush;
            if (IsFourOfKind(hand))
                return Combination.FourOfAKind;
            if (IsFullHouse(hand))
                return Combination.FullHouse;
            if (IsFlush(hand))
                return Combination.Flush;
            if (IsStraight(hand))
                return Combination.Straight;
            if (IsThreeOfAKind(hand))
                return Combination.ThreeOfAKind;
            if (IsTwoPair(hand))
                return Combination.TwoPairs;
            if (isPair(hand))
                return Combination.Pair;
            return Combination.HighCard;
        }
    }
    public enum Combination
    {
        HighCard = 0,
        Pair,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

}

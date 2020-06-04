using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    public enum Rank
    {
        Two = 0,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Quin,
        King,
        Ace
    }

    public enum Suit
    {
        Diamonds = 1,
        Hearts,
        Spades,
        Clubs
    }

    public class Card
    {
        public Rank Rank;
        public Suit Suit;

        public Card(Rank rank,Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public string showCard()
        {
            return (Rank.ToString() + " of " + Suit.ToString() + " ; ");
        }
    }
}

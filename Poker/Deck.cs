using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Deck
    {
        private int Size = 52;
        public List<Card> deck = new List<Card>();
        
        public Deck()
        {
            foreach(Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach(Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    Card card = new Card(rank, suit);
                    deck.Add(card);
                }
            }
        }
    }
}

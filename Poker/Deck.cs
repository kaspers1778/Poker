using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Deck
    {
        private int Size = 52;
        public List<Card> deck;
        
        public Deck()
        {
            foreach(Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach(Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    deck.Add(new Card(rank, suit));
                }
            }
        }
    }
}

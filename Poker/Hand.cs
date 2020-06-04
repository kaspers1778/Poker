using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Poker
{
    public class Hand
    {
        public List<Card> hand = new List<Card>();
        public List<Card> preFlup =new  List<Card>();
        private int Size = 7;
        public int highCard {get;set;}
        public CombinationIndicator comb;

        public void takeCards(List<Card> pre_flup)
        {
            preFlup.AddRange(pre_flup);
            hand.AddRange(pre_flup);
        }
        public void lookOnTable(List<Card> table)
        {
            hand.AddRange(table);
        }

        public int getHighCard()
        {
            var sortedHand = preFlup.OrderBy(item => (int)item.Rank).ToList<Card>();
            return (int)sortedHand.Last<Card>().Rank;
        }

        public string showPreFlup()
        {
            string output = "";
            foreach(Card card in preFlup)
            {
               output += card.showCard();
            }
            return output;
        }
        


    }
}

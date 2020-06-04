using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Table
    {
        public List<IPlayer> Players = new List<IPlayer>();
        public IDealer Dealer { get; set; }
        public int Bank { get; set; }
        public int SmallBlind { get; set; }
        public int SmallBlindIndex { get; set; }
        public int currentBet { get; set; }
        public List<Card> CardsOnTable = new List<Card>();

        public Table(List<IPlayer> players,IDealer dealer,int smallBlind)
        {
            Players.AddRange(players);
            Dealer = dealer;
            Bank = 0;
            SmallBlind = smallBlind;
            SmallBlindIndex = 0;
            dealer.GameTable = this;
        }
    }
}

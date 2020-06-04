using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Table
    {
        public List<IPlayer> Players = new List<IPlayer>();
        public List<IPlayer> PassedPlayers = new List<IPlayer>();
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

        public void PlayerPassed(IPlayer pl)
        {
            PassedPlayers.Add(pl);
            Players.Remove(pl);
            int i = Players.Count;
            int j = PassedPlayers.Count;

        }

        public void ReturnPassedPlayers()
        {
            if (PassedPlayers.Count > 0)
            {
                for(int i = 0; i < PassedPlayers.Count; i++)
                {
                    if (PassedPlayers[i].PokerChips > SmallBlind * 2)
                    {
                        Players.Add(PassedPlayers[i]);
                        PassedPlayers.Remove(PassedPlayers[i]);
                    }
                    else
                    {
                        PassedPlayers.Remove(PassedPlayers[i]);
                    }
                }
            }         
        }
    }
}

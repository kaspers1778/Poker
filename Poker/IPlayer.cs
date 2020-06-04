using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    public interface IPlayer
    {
        string Name { get; set; }
        int PokerChips { get; set; }
        int lastBet { get; set; }
        Hand Hand { get; set; }
        int Bet(int betPrice);
        int Call(int callPrice);
        int Raise(int raisePrice);
        int Fold();
        void Win(int gain);
    }
}

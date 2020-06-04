using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    interface IDealer
    {
        Table GameTable { get; set; }
        Card DealCard();
        void DealFlup();
        void DealTurn();
        void DealRiver();
        void DealPreFlup();
        void CollectBets();
        void EndGame();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    public class Bot : IPlayer
    {
        public string Name { get; set; }
        public int StartPokerChips { get; set; }
        public int PokerChips { get; set; }
        public Hand Hand { get; set; }
        public int lastBet { get; set; }

        public Bot(int pokerChips)
        {
            var botNames = Enum.GetValues(typeof(BotNames));
            Random rand = new Random();
            Name = botNames.GetValue(rand.Next(botNames.Length)).ToString();
            StartPokerChips = pokerChips;
            PokerChips = pokerChips;
            Hand = new Hand();
        }

        public int Bet(int betPrice)
        {
            if (PokerChips > StartPokerChips)
            {
                return Raise(betPrice);
            }
            else
            {
                return Call(betPrice);
            }
        }

        public int Call(int callPrice)
        {
            if (PokerChips >= callPrice)
            {
                PokerChips -= callPrice;
                lastBet = callPrice;
                Console.WriteLine(Name + " is Called " + callPrice.ToString());
                return callPrice;
            }
            else
            {
                return Fold();   
            }
        }

        public int Raise(int raisePrice)
        {
            PokerChips -= (int)(PokerChips * 0.1);
            lastBet = (int)(PokerChips * 0.1);
            Console.WriteLine(Name + " is Raised " + ((int)(PokerChips * 0.1)).ToString());
            return (raisePrice + (int)(PokerChips * 0.1));
        }

        
        public int Fold()
        {
            Console.WriteLine(Name + " is passed");
            return 0;
        }
        public void Win(int gain)
        {
            PokerChips += gain;
        }

        public enum BotNames
        {
            Wolverine = 0,
            Iron_Man,
            Spider_Man,
            Black_Widow,
            Black_Panthera,
            Naruto,
            Saske,
            Nikita_Pedorenko,
            Tarasyk,
            Taras_Shevchenko,
            Igor_Sikorsky,
            Sub_Zero
        }
    }
}

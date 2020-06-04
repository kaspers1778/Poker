using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Player : IPlayer
    {
        public string Name { get; set; }
        public int PokerChips { get; set; }
        public Hand Hand { get; set; }
        public int lastBet {get;set;}

        public Player(string name, int pokerChips)
        {
            Name = name;
            PokerChips = pokerChips;
            Hand = new Hand();
        }

        public int Bet(int betPrice)
        {
            Console.WriteLine("Write C-to Call , R-to Rise or F - to Fold(Pass)");
            var output = Console.ReadLine();
            if (output == "C")
            {
                return Call(betPrice);
            }
            if (output == "R")
            {
                return Raise(betPrice);
            }
            if(output == "F")
            {
                return Fold();
            }
            else
            {
                return 0;
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

        public int Raise(int CallPrice)
        {
            Console.WriteLine("Your bet :");
            var bet = Convert.ToInt16(Console.ReadLine());
            if (bet > CallPrice)
            {
                if (bet <= PokerChips)
                {
                    PokerChips -= bet;
                    lastBet = bet;
                    Console.WriteLine(Name + " is Raised " + bet.ToString());
                    return bet;
                }
                else
                {
                    return Fold();
                }
            }
            else
            {
                return Fold();
            }
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

    }
}

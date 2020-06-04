using System;
using System.Collections.Generic;

namespace Poker
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Your name :");
            string name = Console.ReadLine();
            Console.WriteLine("Start amount of Poker Chips :");
            int startCash = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Small blind is :");
            int smallBlind = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Amount of bot's:");
            int AmountOfBot = Convert.ToInt16(Console.ReadLine());
            Console.ReadKey();
            Console.Clear();
            Player player = new Player(name,startCash);

            List<IPlayer> players = new List<IPlayer>();
            players.Add(player);
            for(int i = 0; i < AmountOfBot; i++)
            {
                Bot bot = new Bot(startCash);
                players.Add(bot);
            }

            Dealer dealer = new Dealer();

            Table PokerTable = new Table(players,dealer,smallBlind);

            Game Game = new Game(dealer, player, PokerTable, players);

            Game.GameLoop();
            
     
        }
    }
}

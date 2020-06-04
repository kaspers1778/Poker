using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Game
    {
        public Dealer Dealer;
        public IPlayer Player;
        public Table GameTable;
        public List<IPlayer> Players = new List<IPlayer>();

        public Game(Dealer dealer, IPlayer player,Table gameTable,List<IPlayer> players)
        {
            Dealer = dealer;
            Player = player;
            GameTable = gameTable;
            Players = players;
        }

        public void ShowInfo()
        {
            Players = GameTable.Players;
            Console.WriteLine("\n" + "----------TABLE----------");
            foreach (IPlayer pokerist in Players)
            {
                Console.WriteLine(pokerist.Name + " " + pokerist.PokerChips);
            }

            Console.WriteLine("\n");
            Console.WriteLine("Bank: " + GameTable.Bank);
            Console.WriteLine("-------------------------");
            Console.WriteLine("\n" + "Press any key to continue");
            Console.ReadKey();
        }
        public void GameLoop()
        {

            Dealer.Setup();
            ShowInfo();

            Console.Clear();

            Dealer.CollectBlindes();
            ShowInfo();

            Console.Clear();

            Dealer.DealPreFlup();
            Console.WriteLine("Your cards is: " + Player.Hand.showPreFlup());
            ShowInfo();

            Console.Clear();

            Dealer.CollectBets();
            ShowInfo();

            Console.Clear();

            Dealer.DealFlup();
            Console.WriteLine("Cards on the table: " + Dealer.showCardsOnTable());
            Console.WriteLine("Your cards is: " + Player.Hand.showPreFlup());
            ShowInfo();

            Console.Clear();

            Dealer.CollectBets();
            ShowInfo();

            Console.Clear();

            Dealer.DealTurn();
            Console.WriteLine("Cards on the table: " + Dealer.showCardsOnTable());
            Console.WriteLine("Your cards is: " + Player.Hand.showPreFlup());
            ShowInfo();

            Console.Clear();

            Dealer.CollectBets();
            ShowInfo();

            Console.Clear();

            Dealer.DealRiver();
            Console.WriteLine("Cards on the table: " + Dealer.showCardsOnTable());
            Console.WriteLine("Your cards is: " + Player.Hand.showPreFlup());
            ShowInfo();

            Console.Clear();

            Dealer.CollectBets();
            ShowInfo();

            Console.Clear();

            Console.WriteLine("Cards on the table: " + Dealer.showCardsOnTable() + "\n");
            Console.WriteLine("\n" + "----------TABLE----------");
            foreach (IPlayer pokerist in Players)
            {
                Console.WriteLine(pokerist.Name + " " + pokerist.PokerChips + " " + pokerist.Hand.showPreFlup() + " - " + CombinationIndicator.getCombination(pokerist.Hand.hand) + "\n");
            }
            Console.WriteLine("-------------------------");
            Dealer.EndGame();
            Console.WriteLine("\n" + "Press any key to continue");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Restart?(Y/N)");
            var output = Console.ReadLine();
            Console.Clear();
            if(output == "Y")
            {
                GameLoop();
            }
            if(output == "N")
            {
                
            }
        }
    }

}

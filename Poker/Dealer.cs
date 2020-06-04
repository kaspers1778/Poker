using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Dealer: IDealer
    {
        Deck GameDeck = new Deck();
        public Table GameTable { get; set; }
        static Random rand = new Random();

        public void Setup()
        {
            Console.WriteLine("Dealer starting new game...");
            GameTable.Bank = 0;
            GameTable.ReturnPassedPlayers();
            foreach (IPlayer player in GameTable.Players)
            {
                player.lastBet = 0;
                player.Hand.hand = new List<Card>();
                player.Hand.preFlup = new List<Card>();
                
            }
            GameTable.currentBet = 0;
            GameTable.CardsOnTable = new List<Card>();
            GameDeck = new Deck();
            ShuffleDeck();
        }

        public void ShuffleDeck()
        {
            for(int i = 0;i<GameDeck.deck.Count-1;i++)
            {
                int CardIndex = rand.Next(0, 52);
                Card tmp = GameDeck.deck[i];
                GameDeck.deck[i] = GameDeck.deck[CardIndex];
                GameDeck.deck[CardIndex] = tmp;
            }
        }

        public Card DealCard()
        {
            var UpperCard = GameDeck.deck[GameDeck.deck.Count - 1];
            GameDeck.deck.RemoveAt(GameDeck.deck.Count - 1);
            return UpperCard;
        }

        public void DealPreFlup()
        {
            Console.WriteLine("Dealer dealing Pre-Flup...");
            foreach(IPlayer player in GameTable.Players)
            {
                List<Card> preflup = new List<Card>();
                preflup.Add(DealCard());
                preflup.Add(DealCard());
                player.Hand.takeCards(preflup);      
            }
        }

        public void DealFlup()
        {
            Console.WriteLine("Dealer dealing Flup...");
            GameTable.CardsOnTable.Add(DealCard());
            GameTable.CardsOnTable.Add(DealCard());
            GameTable.CardsOnTable.Add(DealCard());
        }

        public void DealTurn()
        {
            Console.WriteLine("Dealer dealing Turn...");
            GameTable.CardsOnTable.Add(DealCard());
        }

        public void DealRiver()
        {
            Console.WriteLine("Dealer dealing River...");
            GameTable.CardsOnTable.Add(DealCard());
            foreach(IPlayer player in GameTable.Players)
            {
                player.Hand.lookOnTable(GameTable.CardsOnTable);
            }
        }

        public void CollectBlindes()
        {
            Console.WriteLine("Dealer collecting Small and Big Blindes...");
            if(GameTable.SmallBlindIndex > GameTable.Players.Count - 1)
            {
                GameTable.SmallBlindIndex = 0;
            }
            if (GameTable.SmallBlindIndex < GameTable.Players.Count-1)
            {
                GameTable.Bank+=GameTable.Players[GameTable.SmallBlindIndex].Call(GameTable.SmallBlind);
                GameTable.Bank+=GameTable.Players[GameTable.SmallBlindIndex + 1].Call(GameTable.SmallBlind * 2);
                GameTable.currentBet = GameTable.SmallBlind * 2;
                GameTable.SmallBlindIndex++;
            }
            else
            {
                GameTable.Bank+= GameTable.Players[GameTable.SmallBlindIndex].Call(GameTable.SmallBlind);
                GameTable.Bank+=GameTable.Players[0].Call(GameTable.SmallBlind * 2);
                GameTable.currentBet = GameTable.SmallBlind * 2;
                GameTable.SmallBlindIndex = 0;
            }
        }

        public void CollectBets()
        {
            Console.WriteLine("Dealer Collecting Bets...");
            int currentIndex = GameTable.SmallBlindIndex + 1;
            int startBankOnTurn = 0;
            if (GameTable.currentBet == 10)
            {
                 startBankOnTurn = GameTable.Bank;
            }
            while((GameTable.Bank - startBankOnTurn)!= GameTable.currentBet * GameTable.Players.Count && GameTable.Bank != 0)
            {
                if (GameTable.Players.Count == 1)
                {
                    break;
                }

                if (currentIndex <= GameTable.Players.Count - 1)
                {
                    int playersLastBet = GameTable.Players[currentIndex].lastBet;
                    int bet = GameTable.Players[currentIndex].Bet(GameTable.currentBet - GameTable.Players[currentIndex].lastBet);
                    if (bet == 0 && GameTable.currentBet - GameTable.Players[currentIndex].lastBet != 0)
                    {
                        GameTable.PlayerPassed(GameTable.Players[currentIndex]);
                    }
                    else
                    {
                        GameTable.currentBet = bet + playersLastBet;
                        GameTable.Players[currentIndex].lastBet = bet;
                        GameTable.Bank += bet;
                        currentIndex++;
                    }

                }
                else
                {
                    currentIndex = 0;
                }
                
            }
            foreach(IPlayer player in GameTable.Players)
            {
                player.lastBet = 0;
            }
            GameTable.currentBet = 10;
        }

        public List<IPlayer> getWinners()
        {
            var winner = GameTable.Players[0];
            for (int i = 1; i < GameTable.Players.Count; i++)
            {
                if (CombinationIndicator.getCombination(GameTable.Players[i].Hand.hand) > CombinationIndicator.getCombination(winner.Hand.hand))
                {
                    winner = GameTable.Players[i];
                }
            }
            List<IPlayer> winners = new List<IPlayer>();

            foreach (IPlayer player in GameTable.Players)
            {
                if (CombinationIndicator.getCombination(player.Hand.hand) == CombinationIndicator.getCombination(winner.Hand.hand))
                {
                    winners.Add(player);
                }
            }
            if (winners.Count > 1)
            {
                var winnerByHighCard = winners[0];
                for (int i = 1; i < winners.Count; i++)
                {
                    if (winners[i].Hand.highCard > winnerByHighCard.Hand.highCard)
                    {
                        winnerByHighCard = winners[i];
                    }
                }
                List<IPlayer> TotalWiners = new List<IPlayer>();
                foreach (IPlayer player in winners)
                {
                    if (player.Hand.highCard == winnerByHighCard.Hand.highCard)
                    {
                        TotalWiners.Add(player);
                    }
                }
                return TotalWiners;
            }
            else
            {
                return winners;
            }
        }

        public void EndGame()
        {

            var Winners = getWinners();
            foreach(IPlayer winner in Winners)
            {
                winner.Win(GameTable.Bank / Winners.Count);
                Console.WriteLine("Winner is " + winner.Name.ToString() + " with " + CombinationIndicator.getCombination(winner.Hand.hand).ToString());
            }

        }

        public string showCardsOnTable()
        {
            string output = "[";
            foreach (Card card in GameTable.CardsOnTable)
            {
                output += card.showCard();
                output += ";";
            }
            output += "]";
            return output;
        }
       
    }
}

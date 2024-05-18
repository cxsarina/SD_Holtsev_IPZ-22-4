﻿
using ClassLibrary.CardDeck;
using ClassLibrary.Observer;
using ClassLibrary.PlayerHand;

namespace ClassLibrary.GameTemplate
{
    public class ConsoleBlackjackGame : BlackjackGame
    {
        private BlackjackObserver observer = new BlackjackObserver();
        protected override void InitializeGame()
        {
            deck = new Deck();
            playerHand = new Hand();
            dealerHand = new Hand();

            playerHand.AddObserver(observer);
            dealerHand.AddObserver(observer);
        }

        protected override void DealInitialCards()
        {
            playerHand.AddCard(deck.DrawCard());
            playerHand.AddCard(deck.DrawCard());
            dealerHand.AddCard(deck.DrawCard());
            dealerHand.AddCard(deck.DrawCard());

            // Перевірка, чи є Blackjack у гравця
            observer.Update(playerHand);
        }

        protected override void PlayerTurn()
        {
            if (!observer.СontinueGame)
            {
                return;
            }

            bool playerContinues = true;
            while (playerContinues)
            {
                DisplayHands(true);
                Console.Write("Do you want to hit or stand? (h/s): ");
                string input = Console.ReadLine();
                Console.WriteLine();
                if (input.ToLower() == "h")
                {
                    playerHand.AddCard(deck.DrawCard());
                    observer.Update(playerHand);
                    playerContinues = observer.СontinueGame;
                }
                else
                {
                    playerContinues = !observer.СontinueGame;
                }
            }
        }

        protected override void DealerTurn()
        {
            if (!observer.СontinueGame)
            {
                return;
            }

            while (dealerHand.Score < 17)
            {
                dealerHand.AddCard(deck.DrawCard());
            }
        }

        protected override void DetermineWinner()
        {
            DisplayHands();

            if (playerHand.IsBusted())
            {
                Console.WriteLine("You lose!");
            }
            else if (dealerHand.IsBusted() || playerHand.Score > dealerHand.Score)
            {
                Console.WriteLine("You win!");
            }
            else if (playerHand.Score < dealerHand.Score)
            {
                Console.WriteLine("Dealer wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
        private void DisplayHands(bool isDealerCardHidden=false)
        {
            const int padding = 30;
            string playerHandString = "Your hand: " + string.Join(", ", playerHand);
            string dealerHandString;

            Console.WriteLine(playerHandString.PadRight(Console.WindowWidth - padding) + "|| Score: " + playerHand.Score);

            if (isDealerCardHidden) 
            {
                dealerHandString = "Dealer's visible card: " + dealerHand.GetFirstCard();
                Console.WriteLine(dealerHandString.PadRight(Console.WindowWidth - padding));
            }
            else
            {
                dealerHandString = "Dealer's hand: " + string.Join(", ", dealerHand);
                Console.WriteLine(dealerHandString.PadRight(Console.WindowWidth - padding) + "|| Score: " + dealerHand.Score);
            }                    
            Console.WriteLine();
        }
    }
}

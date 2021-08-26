using System;
using System.Collections.Generic;

namespace BlackJackCS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Card myCard = new Card("Ace of Spades", 11);
            // Console.WriteLine($"Welcome to Blackjack! Your card is : {myCard.Name}");

            Console.WriteLine("Welcome to Blackjack!");


            bool continuePlaying = true;
            //main game loop

            while (continuePlaying)
            {
                //create starting deck

                List<Card> deck = generateListDeck();

                //shuffle deck and create a stack for playing

                Stack<Card> playingDeck = shuffleDeck(deck);

                // deal hand for player and dealer

                List<Card> playerHand = new List<Card>() { };
                List<Card> dealerHand = new List<Card>() { };
                int playerHandValue = 0;
                int dealerHandValue = 0;


                playerHand.Insert(0, playingDeck.Pop());
                dealerHand.Insert(0, playingDeck.Pop());
                playerHand.Insert(0, playingDeck.Pop());
                // dealerHand.Insert(0, playingDeck.Pop());


                Console.WriteLine("Your hand:  ");
                foreach (Card card in playerHand)
                {
                    Console.WriteLine($"{card.Name} : {card.Value}");
                    playerHandValue += card.Value;

                }

                Console.WriteLine($"Total hand value: {playerHandValue}");

                // show dealers first card

                Console.WriteLine($"Dealer card is: {dealerHand[0].Name} with value {dealerHand[0].Value}");
                dealerHandValue += dealerHand[0].Value;

                //begin loop

                //ask the player to "Hit" or "Stand" until "Stand" or bust (total hand value ti > 21)
                string playerResponse = "";

                while (!(playerHandValue > 21 || playerResponse == "s"))
                {
                    Console.WriteLine("Would you like to (h)it or(s)tand?");
                    playerResponse = Console.ReadLine();

                    if (playerResponse == "h")
                    {
                        Card dealtCard = playingDeck.Pop();
                        playerHand.Insert(playerHand.Count, dealtCard);
                        playerHandValue += dealtCard.Value;
                        Console.WriteLine($"You were dealt a {dealtCard.Name} Your new total hand value is : {playerHandValue}");

                    }
                }
                // dealer reveal hand, and keeps hitting until handValue >= 17
                //Calculate if dealer busts, if not calculate winner
                // declare winner

                //Calculate if the player busted
                if (playerHandValue > 21)
                {
                    Console.WriteLine("Uh-oh! Your hand value is over 21 and you lost.");
                    Console.WriteLine("Would you like to play again? y/n");
                    continuePlaying = (Console.ReadLine() == "y") ? true : false;
                    continue;
                }


                while (!(dealerHandValue >= 17))
                {
                    Card dealtCard = playingDeck.Pop();
                    dealerHand.Insert(dealerHand.Count, dealtCard);
                    dealerHandValue += dealtCard.Value;
                    Console.WriteLine($"You were dealt a {dealtCard.Name} Your new total hand value is : {dealerHandValue}");
                }


                if (dealerHandValue > 21)
                {
                    Console.WriteLine("Yay The dealer hand value is over 21 and you won.");
                    Console.WriteLine("Would you like to play again? y/n");
                    continuePlaying = (Console.ReadLine() == "y") ? true : false;
                    continue;
                }




                //Calculate winner

                if (playerHandValue > dealerHandValue)
                {
                    Console.WriteLine("Success! You've won!");

                }
                else
                {
                    Console.WriteLine("Uh-oh! You've lost");
                }

                //Ask user if they want to play again, and set the continePlaying variable to the response

                Console.WriteLine("Would you like to play again? y/n");
                continuePlaying = (Console.ReadLine() == "y") ? true : false;

            }

        }

        public static Stack<Card> shuffleDeck(List<Card> deck)
        {
            var randomNumberGenerator = new Random();
            int leftIndex;
            Card leftCard;
            Card rightCard;

            for (int rightIndex = deck.Count - 1; rightIndex > 0; rightIndex--)
            {

                leftIndex = randomNumberGenerator.Next(rightIndex + 1);

                //save cards in variables so we don't lost them

                leftCard = deck[leftIndex];
                rightCard = deck[rightIndex];

                //swap

                deck[leftIndex] = rightCard;
                deck[rightIndex] = leftCard;
            }

            return new Stack<Card>(deck);


        }
        public static List<Card> generateListDeck()
        {
            List<Card> deck = new List<Card>() { };
            var suits = new List<string>() { "Clubs", "Diamonds", "Hearts", "Spades" };
            var ranks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            var values = new List<int>() { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };

            for (int i = 0; i < ranks.Count; i++)
            {
                foreach (string suit in suits)
                {
                    deck.Insert(0, new Card(ranks[i] + " of " + suit, values[i]));
                }
            }

            return deck;
        }

    }


    class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Card(string newName, int newValue)
        {
            Name = newName;
            Value = newValue;
        }
    }


}

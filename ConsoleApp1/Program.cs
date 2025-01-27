using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w kasynie! Podaj swoje imię:");
            string playerName = Console.ReadLine();
            int playerBalance = 1000;

            Console.WriteLine($"Witaj, {playerName}! Masz {playerBalance} żetonów.");

            while (true)
            {
                Console.WriteLine("Wybierz grę:");
                Console.WriteLine("1. Blackjack");
                Console.WriteLine("2. Ruletka");
                Console.WriteLine("3. Poker");
                Console.WriteLine("4. Wyjście");
                Console.Write("Wybór: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        PlayBlackjack(playerName, ref playerBalance);
                        break;
                    case "2":
                        Console.Clear();
                        PlayRoulette(playerName, ref playerBalance);
                        break;
                    case "3":
                        Console.Clear();
                        PlayPoker(playerName, ref playerBalance);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Dziękujemy za grę! Do zobaczenia!");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        break;
                }

                Console.WriteLine($"Twoje saldo: {playerBalance} żetonów.");
                if (playerBalance <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Niestety, straciłeś wszystkie żetony. Gra zakończona.");
                    break;
                }
            }
        }

        static void PlayBlackjack(string playerName, ref int playerBalance)
        {
            Console.WriteLine($"{playerName}, zaczynamy grę w Blackjack! Podaj stawkę:");
            int bet = int.Parse(Console.ReadLine());

            if (bet > playerBalance)
            {
                Console.Clear();
                Console.WriteLine("Nie masz wystarczająco dużo żetonów na tę stawkę.");
                return;
            }

            Random random = new Random();
            int playerScore = 0;

            while (true)
            {
                Console.Clear();
                int card = random.Next(1, 12);
                playerScore += card;
                Console.WriteLine($"Otrzymałeś kartę o wartości {card}. Twój wynik to {playerScore}.");

                if (playerScore > 21)
                {
                    Console.Clear();
                    Console.WriteLine("Przegrałeś! Twój wynik przekroczył 21.");
                    playerBalance -= bet;
                    break;
                }

                Console.Write("Czy chcesz dobrać kolejną kartę? (t/n): ");
                string input = Console.ReadLine();

                if (input.ToLower() != "t")
                {
                    int dealerScore = random.Next(17, 23);
                    Console.Clear();
                    Console.WriteLine($"Wynik krupiera: {dealerScore}");
                    if (playerScore > dealerScore || dealerScore > 21)
                    {
                        Console.WriteLine("Wygrałeś!");
                        playerBalance += bet;
                    }
                    else
                    {
                        Console.WriteLine("Przegrałeś.");
                        playerBalance -= bet;
                    }
                    break;
                }
            }
        }

        static void PlayRoulette(string playerName, ref int playerBalance)
        {
            Console.WriteLine($"{playerName}, zaczynamy grę w ruletkę! Podaj stawkę:");
            int bet = int.Parse(Console.ReadLine());

            if (bet > playerBalance)
            {
                Console.Clear();
                Console.WriteLine("Nie masz wystarczająco dużo żetonów na tę stawkę.");
                return;
            }

            Console.WriteLine("Wybierz typ zakładu:");
            Console.WriteLine("1. Pojedynczy numer (0-36)");
            Console.WriteLine("2. Kolor (czerwony/czarny)");
            Console.WriteLine("3. Parzyste/Nieparzyste");
            Console.Write("Wybór: ");
            string betType = Console.ReadLine();

            Random random = new Random();
            int resultNumber = random.Next(0, 37);
            string resultColor = (resultNumber % 2 == 0) ? "czerwony" : "czarny";
            string resultParity = (resultNumber % 2 == 0) ? "parzysty" : "nieparzysty";

            switch (betType)
            {
                case "1":
                    Console.Write("Podaj numer (0-36): ");
                    int chosenNumber = int.Parse(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine($"Wynik ruletki: {resultNumber}");
                    if (resultNumber == chosenNumber)
                    {
                        Console.WriteLine("Gratulacje! Trafiłeś numer!");
                        playerBalance += bet * 35;
                    }
                    else
                    {
                        Console.WriteLine("Przegrałeś.");
                        playerBalance -= bet;
                    }
                    break;

                case "2":
                    Console.Write("Wybierz kolor (czerwony/czarny): ");
                    string chosenColor = Console.ReadLine().ToLower();
                    Console.Clear();
                    Console.WriteLine($"Wynik ruletki: {resultColor}");
                    if (resultColor == chosenColor)
                    {
                        Console.WriteLine("Gratulacje! Trafiłeś kolor!");
                        playerBalance += bet;
                    }
                    else
                    {
                        Console.WriteLine("Przegrałeś.");
                        playerBalance -= bet;
                    }
                    break;

                case "3":
                    Console.Write("Wybierz (parzysty/nieparzysty): ");
                    string chosenParity = Console.ReadLine().ToLower();
                    Console.Clear();
                    Console.WriteLine($"Wynik ruletki: {resultParity}");
                    if (resultParity == chosenParity)
                    {
                        Console.WriteLine("Gratulacje! Trafiłeś parzystość!");
                        playerBalance += bet;
                    }
                    else
                    {
                        Console.WriteLine("Przegrałeś.");
                        playerBalance -= bet;
                    }
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Nieprawidłowy wybór. Powrót do menu.");
                    break;
            }
        }

        static void PlayPoker(string playerName, ref int playerBalance)
        {
            Console.WriteLine($"{playerName}, zaczynamy grę w Pokera! Podaj stawkę:");
            int bet = int.Parse(Console.ReadLine());

            if (bet > playerBalance)
            {
                Console.Clear();
                Console.WriteLine("Nie masz wystarczająco dużo żetonów na tę stawkę.");
                return;
            }

            Random rnd = new Random();
            List<Card> deck = GenerateDeck().OrderBy(x => rnd.Next()).ToList();
            
            List<Card> playerHand = new List<Card>();
            List<Card> dealerHand = new List<Card>();

            for (int i = 0; i < 9; i+=2)
            {
                playerHand.Add(deck[i]);
                dealerHand.Add(deck[i+1]);
            }

            string playerCardsText = string.Empty;
            string dealerCardsText = string.Empty;
            foreach (Card card in playerHand)
            {
                if (string.IsNullOrEmpty(playerCardsText))
                    playerCardsText = card.ToString();
                else
                    playerCardsText += ", " + card.ToString();
            }

            foreach (Card card in dealerHand)
            {
                if (string.IsNullOrEmpty(dealerCardsText))
                    dealerCardsText = card.ToString();
                else
                    dealerCardsText += ", " + card.ToString();
            }

            Console.WriteLine("Twoja ręka: " + playerCardsText);
            Console.WriteLine("Ręka krupiera: " + dealerCardsText);

            int playerScore = EvaluateHand(playerHand);
            int dealerScore = EvaluateHand(dealerHand);

            Console.WriteLine($"Twój wynik: {playerScore}, Wynik krupiera: {dealerScore}");

            if (playerScore > dealerScore)
            {
                Console.WriteLine("Wygrałeś!");
                playerBalance += bet;
            }
            else if (playerScore < dealerScore)
            {
                Console.WriteLine("Przegrałeś.");
                playerBalance -= bet;
            }
            else
            {
                Console.WriteLine("Remis. Stawka zostaje.");
            }
        }

        static List<Card> GenerateDeck()
        {
            List<Card> deck = new List<Card>();
            int[] ranks = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    deck.Add(new Card(suit, rank));
                }
            }

            return deck;
        }

        static int EvaluateHand(List<Card> hand)
        {
            foreach (var card in hand)
            {
            }

            return 0;
        }

        static bool IsStraight(List<int> ranks)
        {
            ranks.Sort();
            for (int i = 0; i < ranks.Count - 1; i++)
            {
                if (ranks[i] != ranks[i + 1] - 1) return false;
            }
            return true;
        }

        static int ConvertRankToValue(string rank)
        {
            switch (rank)
            {
                case "2": return 2;
                case "3": return 3;
                case "4": return 4;
                case "5": return 5;
                case "6": return 6;
                case "7": return 7;
                case "8": return 8;
                case "9": return 9;
                case "10": return 10;
                case "J": return 11;
                case "Q": return 12;
                case "K": return 13;
                case "A": return 14;
                default: return 0;
            }
        }
    }
}

      
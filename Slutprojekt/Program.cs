using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("What is your name?");
            User user = new User(5000, Console.ReadLine());
            Menu(user);
        }
        static void Menu(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome to Linus' blackjack {user.Name}!");
                Console.WriteLine($"Your balance is currently {user.Balance}$.");
                Console.WriteLine("Choose one of the following to continue.");
                Console.WriteLine("1. Play blackjack");
                Console.WriteLine("2. Rules");
                Console.WriteLine("3. Exit");
                switch (ReadPlayerInput(3))
                {
                    case '1':
                        PlayBlackJack(user);
                        break;
                    case '2':
                        Rules();
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("Thank you for playing!");
                        Thread.Sleep(3000);
                        Environment.Exit(1);
                        break;
                }
            }
        }
                //Tar emot en användare och skriver ut namnet, sedan alla alternativ

        static void PlayBlackJack(User user)
        {
            Console.WriteLine("How much would you like to bet?");
            int bet = TryParseInt();

            Deck deck = new Deck();
            deck.ShuffleDeck();
            List<Hand> playerHands = new List<Hand>();
            Hand playerHand = new Hand(false);
            Hand dealerHand = new Hand(true);
            DealCards(playerHand, dealerHand);
            playerHands.Add(playerHand);
            //Förbereder Blackjack spelet

            for (int i = 0; i < playerHands.Count(); i++)
            {
                while (!playerHands[i].HasStayed && !playerHands[i].IsBust)
                {
                    Console.Clear();
                    if (playerHands[i].IsSplit)
                        Console.WriteLine($"Hand {i + 1}:");
                    PrintGame(playerHands, dealerHand, i+1);
                    PrintPlayerOptions(playerHands[i]);
                    int choice = int.Parse(ReadPlayerInput(playerHands[i].PossibleChoices().Count()).ToString()) - 1;
                    Decision(playerHands[i], choice);
                    if (playerHands[i].IsBust)
                    {
                        Console.Clear();
                        PrintGame(playerHands, dealerHand, i+1);
                        Thread.Sleep(3000);
                    }
                }
            }
            //Loopar igenom spelarens händer. 
            //För varje hand skrivs handen ut,
            //och sedan får användaren välja vad de vill göra för varje hand. Kollar även ifall handen går över 21

            dealerHand.HoleHand = false;

            if (playerHands.Any(x => !x.IsBust))
            {
                Console.Clear();
                Console.WriteLine("Reaveling the dealer's hand.");
                Thread.Sleep(3000);

                Console.Clear();
                PrintGame(playerHands, dealerHand, 0);
                Thread.Sleep(3000);
            }
            //Kollar om någon hand är under 21. Isåfall skrivs "dealerns" hand ut.

            while (!dealerHand.HasStayed && !playerHands.Any(x => x.IsBust) && !dealerHand.IsBust && dealerHand.ValueOfHand().Item2 < 17)
            {
                Console.Clear();
                Decision(dealerHand, DealerAction(dealerHand));
                PrintGame(playerHands, dealerHand, 0);
                Thread.Sleep(3000);
            }
            //Låter dealern dra tills vilkoren inte uppfylls

            Console.Clear();
            EndRound(playerHands, dealerHand);
            Console.ReadKey(true);

            //Vem vinner.

            int DealerAction(Hand dhand)
            {
                (int low, int max) = dhand.ValueOfHand();
                if (max < 17 || (max > 21 && low < 17))
                    return 0;
                else
                    return 1;
            }
            //Returnerar ifall dealern ska dra eller inte.

            string WinnerOfHands(Hand pHand, Hand dHand)
            {
                (int lowP, int maxP) = pHand.ValueOfHand();
                (int lowD, int maxD) = dHand.ValueOfHand();
                int playerValue = maxP > 21 ? lowP : maxP;
                int dealerValue = maxD > 21 ? lowD : maxD;
                if (pHand.IsBust)
                    return "Dealer";
                else if (dHand.IsBust)
                    return "Player";
                else if (dealerValue < playerValue)
                    return "Player";
                else if (playerValue < dealerValue)
                    return "Dealer";
                else
                    return "Tie";
            }
            //Returnerar vinnaren.

            void Decision(Hand hand, int choice)
            {
                switch (hand.PossibleChoices()[choice])
                {
                    case "Hit":
                        Hit(hand);
                        break;
                    case "Stay":
                        Stay(hand);
                        break;
                    case "Double":
                        Double(hand);
                        break;
                    case "Split":
                        Split(hand);
                        break;
                }
            }
            //Låter spelaren göra ett val.

            void Hit(Hand hand)
            {
                hand.Hit(deck.DrawCard());
            }
            //Lägger till ett kort till den inskickade handen.

            void Stay(Hand hand)
            {
                hand.HasStayed = true;
            }
            //Bekräftar att handen stannar.

            void Double(Hand hand)
            {
                Hit(hand);
                hand.HasDoubled = true;
                bet *= 2;
            }
            //Dubblar sattsningen och "hittar" för handen.

            void Split(Hand hand)
            {
                playerHands.Remove(hand);
                playerHands.Add(new Hand(false, hand.Cards[0], true));
                playerHands.Add(new Hand(false, hand.Cards[1], true));
            }
            //Tar bort den nuvarande handen från spelaren och delar den gamla handen till två nya.

            void DealCards(Hand pHand, Hand dHand)
            {
                for (int i = 0; i < 2; i++)
                {
                    pHand.Hit(deck.DrawCard());
                    dHand.Hit(deck.DrawCard());
                }
            }
            //Ger två kort till de angivna händerna.

            void PrintGame(List<Hand> pHands, Hand dHand, int d)
            {
                string s = "";
                s += $"Dealer's hand:\n";
                s += dHand.HandUTF8();
                s += $"{user.Name}'s hand{(pHands.Count() == 1 ? "" : "s")}:\n";
                if (d == 0)
                    for (int i = 0; i < pHands.Count(); i++)
                        s += pHands[i].HandUTF8();
                else
                    s += pHands[d - 1].HandUTF8();
                Console.WriteLine(s);
            }
            //Skriver ut korten av "dHand" och listans "pHands".

            void EndRound(List<Hand> pHands, Hand dHand)
            {
                PrintGame(pHands, dHand, 0);
                foreach (Hand item in pHands)
                {
                    string s = "";
                    switch (WinnerOfHands(item, dHand))
                    {
                        case "Dealer":
                            s = $"You lost. {bet}$ has been taken from your balance.";
                            user.Balance -= bet;
                            break;
                        case "Player":
                            s = $"You won. {bet}$ has been added to your balance.";
                            user.Balance += bet;
                            break;
                        case "Tie":
                            s = $"You tied. Your balance remains the same.";
                            break;
                    }
                    Console.WriteLine(s);
                }   
            }
            //Avslutar Blackjack omgången och presenterar resultatet och vinsten/förlusten för spelaren.

            void PrintPlayerOptions(Hand pHand)
            {
                int counter = 0;
                string s = "";
                foreach (string item in pHand.PossibleChoices())
                {
                    s += $"{++counter}. {item}\n";
                }
                Console.WriteLine(s);
            }
            //Skriver ut det giltiga alternativen för handen.
        }
        static void Rules()
        {
            Console.WriteLine("Your goal in black jack is to achieve a hand with a value as close to 21, without surpassing 21. You need to have better cards than the dealer aswell.");
            Console.WriteLine("At the start of a black jack round you will get dealt two cards. All face cards are valued 10. Aces are valued 1 or 11. If you get dealt an ace and a");
            Console.WriteLine("ten-value card you have gotten a blackjack and the payout is 1.5x the bet. Any other win has a payout of 1x the bet. If you exceed a sum of 21 you bust and");
            Console.WriteLine("you lose. If both you and the dealer busts, the dealer wins. If the dealer busts and you do not, you win. If you achieve a sum higher than the dealers, you win.");
            Console.WriteLine("If both you and the dealer achieves a blackjack or the same sum, it is a push and no one wins.");
            Console.WriteLine("");
            Console.WriteLine("Special rules:");
            Console.WriteLine("If these cards add up to 7, 8, 9, 10 or 11 you willget the offer to double your bet. ");
            Console.ReadLine();
        }
        //Skriver ut reglerna för Blackjack.
        static int TryParseInt()
        {
            int integer;
            while (int.TryParse(Console.ReadLine(), out integer ) == false)
            {
                Console.WriteLine("Input an integer");
            }
            return integer;
        }
        //Försker parsa spelar inputen till en int, tills det går. Returnerar denna int.
        static char ReadPlayerInput(int amountOptions)
        {
            while (true)
            {
                char[] validAlternatives = new char[amountOptions];
                for (int i = 0; i < amountOptions; i++)
                    validAlternatives[i] = char.Parse($"{i + 1}");
                char pressedChar = Console.ReadKey(true).KeyChar;
                foreach (char option in validAlternatives)
                {
                    if (pressedChar == option)
                        return pressedChar;
                }
            }
        }
        //"amountOptions" anger det giltiga valen. När användaren anger nåt av dessa alternativ returneras det.
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    //Då färgerna och rangerna för korten är förbestämmt, används enums. 
    //Detta underlättar hanteringern av kortens färg och rang. 
    //Enum för kortens färg.
    enum Suit
    {
        Clubs,
        Spades,
        Hearts,
        Diamonds
    }
    //Enum för kortens rang.
    enum Rank
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine, 
        Ten,
        Jack,
        Queen,
        King 
    }

    //Denna klass är jag mest stolt över.
    //Den gör precis det ett kort bör göra, inget mer, inget mindre. 
    //Koden är även consis och lätt förstårlig.

    //Klass för ett kort.
    class Card
    {
        private Suit cardSuit;
        private Rank cardRank;
        private string[] cardUTF8;
        private string[] cardUTF8Hidden;

        //Constructor för att skapa ett kort utifrån de angivna parametrarna.
        public Card(int suit, int rank)
        {
            this.cardSuit = (Suit)suit;
            this.cardRank = (Rank)rank;
            this.cardUTF8 = CreateCardUTF8();
            this.cardUTF8Hidden = CreateCardUTF8Hidden();
        }


        //Returnerar en array av strängar som innehåller kortets representering i text.
        private string[] CreateCardUTF8()
        {
            
            string suitChar = "";
            switch (this.cardSuit)
            {
                case Suit.Clubs:
                    suitChar = "♣";
                    break;
                case Suit.Spades:
                    suitChar = "♠";
                    break;
                case Suit.Hearts:
                    suitChar = "♥";
                    break;
                case Suit.Diamonds:
                    suitChar = "♦";
                    break;
            }
            string rankChar = "";
            switch (this.cardRank)
            {
                case Rank.Ace:
                    rankChar = "A";
                    break;
                case Rank.Jack:
                    rankChar = "J";
                    break;
                case Rank.Queen:
                    rankChar = "Q";
                    break;
                case Rank.King:
                    rankChar = "K";
                    break;
                default:
                    rankChar = $"{(int)this.cardRank}";
                    break;
            }
            string[] card = { "┌─────────┐",
                $"│{rankChar,-9}│",
                $"│{"",-9}│",
                $"│{"",-9}│",
                $"│{"", 4}{suitChar}{"", 4}│",
                $"│{"",-9}│",
                $"│{"",-9}│",
                $"│{"",-9}│",
                $"│{rankChar,9}│",
                "└─────────┘"};
            return card;
        }

        //Returnerar en array av strängar som innehåller text representationen av ett gömt kort.
        private string[] CreateCardUTF8Hidden()
        {
            string[] card = { "┌─────────┐",
                $"│░░░░░░░░░│",
                $"│░░░░░░░░░│",
                $"│░░░░░░░░░│",
                $"│░░░░░░░░░│",
                $"│░░░░░░░░░│",
                $"│░░░░░░░░░│",
                $"│░░░░░░░░░│",
                $"│░░░░░░░░░│",
                "└─────────┘"};
            return card;
        }

        //Property som returnerar en array av kortets text representation.
        public string[] CardUTF8
        {
            get { return this.cardUTF8; }
        }

        //Property som returnerar en array av kortets gömda text representation.
        public string[] CardUTF8Hidden
        {
            get { return this.cardUTF8Hidden; }
        }

        //Returnerar färgen av kortet, en enum.
        public Suit CardSuit
        {
            get { return this.cardSuit; }
        }

        //Returnerar rangen av kortet, en enum.
        public Rank CardRank
        {
            get { return this.cardRank; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    //Klass för en Blackjack hand.
    class Hand
    {
        private List<Card> cards;
        private bool hasDoubled;
        private bool hasStayed;
        private bool holeHand;
        private bool isBust;
        private bool isSplit;

        public Hand(bool holeHand)
        {
            this.cards = new List<Card>();
            this.holeHand = holeHand;
            this.hasDoubled = false;
            this.hasStayed = false;
            this.isBust = false;
            this.isSplit = false;
        }
        //Constructor som skapar en hand utifrån parametern "holeHand".

        public Hand(bool holeHand, Card card, bool isSplit)
        {
            this.cards = new List<Card>();
            this.cards.Add(card);
            this.hasDoubled = false;
            this.hasStayed = false;
            this.isBust = false;
            this.isSplit = isSplit;
        }
        //Constructor som skapar en hand utifrån parametrarna "holeHand", "card" och "isSplit"

        public void Hit(Card card)
        {
            this.cards.Add(card);
            if (this.ValueOfHand().Item1 > 21)
                this.isBust = true;
        }
        //Lägger till ett kort till handen. Om handens värde går över 21, markeras handen att vara bust.

        public (int, int) ValueOfHand()
        {
            int value = 0;
            foreach (Card item in this.cards)
            {
                if (item.CardRank == Rank.King || item.CardRank == Rank.Queen || item.CardRank == Rank.Jack)
                {
                    value += 10;
                }
                else
                {
                    value += (int)item.CardRank;
                }
            }
            return this.cards.Any(x => x.CardRank == Rank.Ace) ? (value, value + 10) : (value, value);
        }
        //Returnerar en tuple av två ints. Första inten, är minimum värdet för handen, andra är maximum.

        public string HandUTF8()
        {
            string s = "";
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < this.cards.Count(); j++)
                {
                    if (this.holeHand && j == 1)
                        s += this.cards[j].CardUTF8Hidden[i];
                    else
                        s += this.cards[j].CardUTF8[i];
                }
                if (i == 9 && this.holeHand == false)
                {
                    (int low, int max) = ValueOfHand();
                    s += $" Valued at ({low} / {max})";
                }
                s += "\n";
            }
            return s;
        }
        //Returnerar en sträng som innehåller handens kort i text representation.

        public List<string> PossibleChoices()
        {
            int low = ValueOfHand().Item1;
            List<string> choices = new List<string>();
            if (this.hasDoubled)
            {
                choices.Add("Stay");
                return choices;
            }
            choices.Add("Hit");
            choices.Add("Stay");
            if (new[] { 7, 8, 9, 10, 11 }.Contains(low))
                choices.Add("Double");
            if (this.cards.GroupBy(x => x.CardRank).Any(g => g.Count() > 1) && !this.isSplit)
                choices.Add("Split");
            return choices;
        }
        //Returnerar en lista av strängar som innehåller de giltiga alternativen för handen.

        public bool HoleHand
        {
            set { this.holeHand = value; }
        }
        //En property med en set för att sätta "holeHand"

        public bool IsBust
        {
            get { return this.isBust; }
        }
        //En property med en get för om handen är bust.

        public bool HasStayed
        {
            get { return this.hasStayed; }
            set { this.hasStayed = value; }
        }
        //En property med en get och set för om handen har stannat.

        public bool HasDoubled
        {
            get { return this.hasDoubled; }
            set { this.hasDoubled = value; }
        }
        //En property med en get och set för om handen har dubblat.

        public bool IsSplit
        {
            get { return this.isSplit; }
            set { this.isSplit = value; }
        }
        //En property med en get och set för om handen har splitat

        public List<Card> Cards
        {
            get { return this.cards; }
        }
        //En property med en get för handens kort.
    }
}

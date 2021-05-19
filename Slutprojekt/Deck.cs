using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    //Klass för kortleken.
    class Deck
    {
        private List<Card> cardDeck;
        private List<Card> drawnCards;
        private Random random;
        public Deck()
        {
            this.random = new Random();
            this.cardDeck = new List<Card>();
            this.drawnCards = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    this.cardDeck.Add(new Card(i, j));
                }
            }
        }
        //Constructor som skapar en ny kortlek.

        public void ShuffleDeck()
        {
            int n = this.cardDeck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card card = this.cardDeck[k];
                this.cardDeck[k] = this.cardDeck[n];
                this.cardDeck[n] = card;
            }
        }
        //Blandar kortleken med "Fisher-Yates" algoritmen.

        public Card DrawCard()
        {
            Card card = this.cardDeck[cardDeck.Count() - 1];
            drawnCards.Add(card);
            cardDeck.Remove(card);
            return card;
        }
        //Drar ett kort från kortleken. 
        //Lägger till detta kort i dragna kort och tar bort kortet från huvud kortleken. 
        //Detta kort returneras.

        public void ResetDeckAndShuffle()
        {
            this.cardDeck = this.cardDeck.Concat(this.drawnCards).ToList();
            this.ShuffleDeck();
            this.drawnCards.Clear();
        }
        //Återställer kortleken.
        public List<Card> CardDeck
        {
            get {return this.cardDeck; }
        }
        //Property som returnerar korten kvar i kortleken.
    }
}

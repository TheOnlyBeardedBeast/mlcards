using System;
using System.Linq;
using System.Text;

namespace MlCards
{
    public enum Rank
    {
        Seven = 1,
        Eight,
        Nine,
        Ten,
        UnderKnave,
        OberKnave,
        King,
        Ace
    }

    public enum Suit
    {
        Hearts = 1,
        Bells,
        Acorns,
        Leaves,
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public int Number
        {
            get
            {
                return ((int)this.Suit - 1)*8 + (int)this.Rank;
            }
        }

        public string Name
        {
            get
            {
                return $"{this.Suit} {this.Rank}";
            }
        }

        public Card(Suit suit, Rank rank)
        {
            this.Suit = suit;
            this.Rank = rank;
        }

        public static Card NumberToCard(int number)
        {
            if(number < 1 || number > 32)
            {
                throw new Exception("Card is not comatible with this deck");
            }

            int suit = (int)Math.Ceiling((double)number / 8);
            int rank = number - ((suit-1) * 8);

            return new Card((Suit)suit, (Rank)rank);
        }

        public string RenderSuit =>
        this.Suit switch
            {
                Suit.Hearts => "♥",
                Suit.Leaves => "♠",
                Suit.Acorns => "♣",
                Suit.Bells => "♦",
                _ => "♥",
            };

        public string RenderRank =>
        this.Rank switch
        {
            Rank.Seven => "07",
            Rank.Eight => "08",
            Rank.Nine => "09",
            Rank.Ten => "10",
            Rank.UnderKnave => "JJ",
            Rank.OberKnave => "QQ",
            Rank.King => "KK",
            Rank.Ace => "AA",
            _ => "AA",
        };

        public void Render()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("┌───────────┐");
            Console.WriteLine($"│ {this.RenderSuit}         │");
            Console.WriteLine("│           │");
            Console.WriteLine("│           │");
            Console.WriteLine($"│     {this.RenderRank}    │");
            Console.WriteLine("│           │");
            Console.WriteLine("│           │");
            Console.WriteLine($"│         {this.RenderSuit} │");
            Console.WriteLine("└───────────┘");
        }

        public static bool ValidateMove(Card top, Card card, Suit? AskedSuit,int Draw = 0)
        {
            if(card.Rank == Rank.UnderKnave && card.Suit == Suit.Leaves || top.Rank == Rank.UnderKnave && top.Suit == Suit.Leaves)
            {
                return true;
            }

            if(top.Rank == Rank.Seven && Draw > 0)
            {
                return (card.Rank == Rank.Seven) || (card.Rank == Rank.UnderKnave && card.Suit == Suit.Leaves); 
            }

            if(card.Rank == Rank.OberKnave && AskedSuit.HasValue)
            {
                return card.Suit == AskedSuit;
            }

            if(top.Suit == card.Suit || top.Rank == card.Rank)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.Number})";
        }
    }

    public class Move
    {
        public Card[] Hand { get; set; }

        public Card Top { get; set; }

        public void RenderHand()
        {
            
            Console.WriteLine(string.Join(", ",Hand.Select(card => card.ToString())));
            foreach (var card in Hand)
            {
                Console.Write("┌───────────┐");
            }
            Console.WriteLine();

            foreach (var card in Hand)
            {
                Console.Write($"│ {card.RenderSuit}         │");
            }
            Console.WriteLine();

            foreach (var card in Hand)
            {
                Console.Write("│           │");
            }
            Console.WriteLine();

            foreach (var card in Hand)
            {
                Console.Write("│           │");
            }
            Console.WriteLine();

            foreach (var card in Hand)
            {
                Console.Write($"│     {card.RenderRank}    │");
            }
            Console.WriteLine();

            

            foreach (var card in Hand)
            {
                Console.Write("│           │");
            }
            Console.WriteLine();
            foreach (var card in Hand)
            {
                Console.Write("│           │");
            }
            Console.WriteLine();

            foreach (var card in Hand)
            {
                string renderString = card.Number > 9 ? card.Number.ToString() : "0" + card.Number;
                Console.Write($"│ {renderString}      {card.RenderSuit} │");
            }
            Console.WriteLine();

            foreach (var card in Hand)
            {
                Console.Write("└───────────┘");
            }
            Console.WriteLine();
        }

    }

    class Program
    {
        
        static void Main(string[] args)
        {
            var deck = new Card[32];

            for(int i = 1; i <= 32; i++)
            {
                deck[i-1] = Card.NumberToCard(i);
            }

            Random rnd = new Random();
            

            for (int i = 0; i < 10; i++)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠ Round {i} ♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠");

                deck = deck.OrderBy(x => rnd.Next(1000)).ToArray();

                var hand = new Card[33];

                var numberOfCards = 5;// rnd.Next(16) + 2;

                var playerCards = deck.Take(numberOfCards);
                //foreach (var card in playerCards)
                //{
                //    hand[card.Number] = card;
                //    Console.WriteLine(card);
                //    card.Render();
                //}

                var move = new Move { Hand = playerCards.ToArray(), Top = deck.Skip(numberOfCards).Take(1).FirstOrDefault() };
                move.RenderHand();
                Console.WriteLine("##########################");
                move.Top.Render();

                Console.ReadLine();
            }

            
        }
    }
}

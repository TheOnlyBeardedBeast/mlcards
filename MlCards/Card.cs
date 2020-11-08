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
                return ((int)this.Suit - 1) * 8 + (int)this.Rank;
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
            if (number < 1 || number > 32)
            {
                throw new Exception("Card is not comatible with this deck");
            }

            int suit = (int)Math.Ceiling((double)number / 8);
            int rank = number - ((suit - 1) * 8);

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

        public override string ToString()
        {
            return $"{this.Name} ({this.Number})";
        }
    }
}
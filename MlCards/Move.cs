using System;
using System.Linq;
using System.Text;

namespace MlCards
{
    public class Move
    {
        public Card[] Hand { get; set; }

        public Card Top { get; set; }

        public static bool ValidateMove(Card top, Card[] cards, Suit? AskedSuit, int draw = 0)
        {
            // you need to place at least one card if you dont place anything, you draw a card
            if (cards.Length == 0)
            {
                return false;
            }

            // we will chceck the first card in the array against the top card
            var card = cards[0];

            // You can place multiple cards only if all the cards have the same rank
            if (!cards.All(c => c.Rank == card.Rank))
            {
                return false;
            }



            // Leaves UnderKnave can be placed on anything, and anything can be placed on it
            // Cancels forced drawing from previous user (when previous user places Seven of any Suit)
            if (card.Rank == Rank.UnderKnave && card.Suit == Suit.Leaves || top.Rank == Rank.UnderKnave && top.Suit == Suit.Leaves)
            {
                return true;
            }

            // if a Seven of any Suit was placed by a user before, and you dont have a Leaves UnderKnave, only a seven can be placed on it
            // if the Seven was used on the previous user, the drawfactor = 0, so any Seven or any Rank with the same suit can be placed on it
            if (top.Rank == Rank.Seven && draw > 0)
            {
                return (card.Rank == Rank.Seven) || (card.Rank == Rank.UnderKnave && card.Suit == Suit.Leaves);
            }

            // with an OberKnave a user can ask for a Suit, any card can be placed on the oberknave with the asked suit
            if (top.Rank == Rank.OberKnave)
            {
                if (!AskedSuit.HasValue)
                {
                    throw new Exception("An OberKnave needs to ask for a suit.");
                }

                return card.Suit == AskedSuit;
            }

            // If the previous user placed an ace you are out of the round, or you can ovveride his Move with an Ace
            if (top.Rank == Rank.Ace && draw == -1)
            {
                return card.Rank == Rank.Ace;
            }

            // if nothing above happens, the user can place any card with the Same suit or same Rank to the top card
            if (top.Suit == card.Suit || top.Rank == card.Rank)
            {
                return true;
            }

            return false;
        }

        public void RenderHand()
        {

            Console.WriteLine(string.Join(", ", Hand.Select(card => card.ToString())));
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
}
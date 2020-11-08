using System;
using System.Linq;
using System.Text;

namespace MlCards
{
    class Program
    {

        static void Main(string[] args)
        {
            var deck = new Card[32];

            for (int i = 1; i <= 32; i++)
            {
                deck[i - 1] = Card.NumberToCard(i);
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

                var top = deck.Skip(numberOfCards).Take(1).FirstOrDefault();
                var move = new Move { Hand = playerCards.ToArray(), Top = top };
                move.RenderHand();
                Console.WriteLine("##########################");
                Suit? askedSuit = null;
                if (top.Rank == Rank.OberKnave)
                {
                    askedSuit = (Suit)rnd.Next(0, 4);
                    Console.WriteLine($"♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠ Ask {askedSuit.Value.ToString()} ♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠");
                }

                int drawFactor = 0;
                if (top.Rank == Rank.Ace)
                {
                    drawFactor = rnd.Next(-1, 0);
                    Console.WriteLine($"♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠ Stay {drawFactor.ToString()} ♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠");
                }

                if (top.Rank == Rank.Seven)
                {
                    drawFactor = rnd.Next(3);
                    Console.WriteLine($"♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠ Draw {drawFactor * 3} ♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠♥♦♣♠");
                }
                move.Top.Render();

                int result = 0;
                int.TryParse(Console.ReadLine(), out result);

                if (result != 0)
                {



                    Console.WriteLine(Move.ValidateMove(top, new Card[] { Card.NumberToCard(result) }, askedSuit, drawFactor));
                }


            }


        }
    }
}

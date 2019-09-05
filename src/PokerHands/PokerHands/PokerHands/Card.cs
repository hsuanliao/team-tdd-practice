using System.Collections.Generic;

namespace PokerHands
{
    public class Card
    {
        public Card(string value)
        {
            var suitLookup = new Dictionary<char, Suit>
            {
                ['S'] = Suit.Spade,
                ['H'] = Suit.Heart,
                ['D'] = Suit.Diamond,
                ['C'] = Suit.Club
            };
            Number = value[0].ToString();
            Suit = suitLookup[value[1]];
        }

        public Suit Suit { get; set; }

        public string Number { get; set; }

        public int NumberValue
        {
            get
            {
                switch (Number.ToUpper())
                {
                    case "T":
                        return 10;

                    case "J":
                        return 11;

                    case "Q":
                        return 12;

                    case "K":
                        return 13;

                    case "A":
                        return 14;

                    default:
                        return int.Parse(Number);
                }
            }
        }
    }
}
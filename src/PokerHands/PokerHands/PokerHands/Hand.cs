using System.Collections.Generic;
using System.Linq;

namespace PokerHands
{
    internal class Hand
    {
        public Hand(string hand)
        {
            Source = hand;
            Cards = Source.Split(',').Select(c => new Card(c)).ToList();
        }

        public string Source { get; set; }

        public HandCategory Category => GetCategory();

        private HandCategory GetCategory()
        {
            var cardNumberGroups = Cards
                .GroupBy(t => t.Number)
                .ToList();
            if (cardNumberGroups.Count == 2 && cardNumberGroups.Any(t => t.Count() == 4))
            {
                return HandCategory.FourOfAKind;
            }

            if (cardNumberGroups.Count == 2 && cardNumberGroups.Any(t => t.Count() == 3) && cardNumberGroups.Any(t => t.Count() == 2))
            {
                return HandCategory.FullHouse;
            }

            if (cardNumberGroups.Count == 3 && cardNumberGroups.Any(t => t.Count() == 3))
            {
                return HandCategory.ThreeOfAKind;
            }

            if (cardNumberGroups.Count == 3 && cardNumberGroups.Count(t => t.Count() == 2) == 2)
            {
                return HandCategory.TwoPairs;
            }

            if (cardNumberGroups.Count == 4 && cardNumberGroups.Count(t => t.Count() == 2) == 1)
            {
                return HandCategory.Pair;
            }

            var cardSuitGroup = Cards
                .Select(t => t.Suit)
                .GroupBy(s => s)
                .ToList();
            var isFlush = cardSuitGroup.Count() == 1;

            // TD,JS,QH,KD,AS => 10,11,12,13,14
            var cardNumberValueGroup = Cards
                .Select(t => t.NumberValue)
                .OrderBy(o => o)
                .ToList();
            if (cardNumberGroups.Count == 5 && (cardNumberValueGroup.Last() - cardNumberValueGroup.First() == 4 ||
                                      cardNumberValueGroup.Last() - cardNumberValueGroup[3] == 9))
            {
                if (isFlush)
                {
                    return HandCategory.StraightFlush;
                }

                return HandCategory.Straight;
            }

            if (isFlush)
            {
                return HandCategory.Flush;
            }

            return HandCategory.HighCard;
        }

        public IList<Card> Cards { get; set; }
    }
}
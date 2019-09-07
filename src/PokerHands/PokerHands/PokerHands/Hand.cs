using System.Collections.Generic;
using System.Linq;
using PokerHands.HandCategories;

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
            if (Match(new FourOfAKindRule(Cards)))
            {
                return HandCategory.FourOfAKind;
            }

            if (GetCardNumberGroups().Count == 2 && GetCardNumberGroups().Any(t => t.Count() == 3) && GetCardNumberGroups().Any(t => t.Count() == 2))
            {
                return HandCategory.FullHouse;
            }

            if (GetCardNumberGroups().Count == 3 && GetCardNumberGroups().Any(t => t.Count() == 3))
            {
                return HandCategory.ThreeOfAKind;
            }

            if (GetCardNumberGroups().Count == 3 && GetCardNumberGroups().Count(t => t.Count() == 2) == 2)
            {
                return HandCategory.TwoPairs;
            }

            if (GetCardNumberGroups().Count == 4 && GetCardNumberGroups().Count(t => t.Count() == 2) == 1)
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
            if (GetCardNumberGroups().Count == 5 && (cardNumberValueGroup.Last() - cardNumberValueGroup.First() == 4 ||
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

        private bool Match(FourOfAKindRule rule)
        {
            var cardNumberGroups = rule.Cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 2 && cardNumberGroups.Any(t => t.Count() == 4);
        }

        private List<IGrouping<string, Card>> GetCardNumberGroups()
        {
            var cardNumberGroups = Cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups;
        }

        public IList<Card> Cards { get; set; }
    }
}
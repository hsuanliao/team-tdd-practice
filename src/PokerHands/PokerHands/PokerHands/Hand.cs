using System.Collections.Generic;
using System.Linq;
using PokerHands.HandCategoryRules;

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
            var rules = new List<IHandCategoryRule>
            {
                new FourOfAKindRule(Cards),
                new FullHouseRule(Cards),
                new ThreeOfAKindRule(Cards)
            };
            var rule = rules.FirstOrDefault(r => r.Match());
            if (rule != null)
            {
                return rule.HandCategory;
            }
            
            if (CardNumberGroups().Count == 3 && CardNumberGroups().Count(t => t.Count() == 2) == 2)
            {
                return HandCategory.TwoPairs;
            }

            if (CardNumberGroups().Count == 4 && CardNumberGroups().Count(t => t.Count() == 2) == 1)
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
            if (CardNumberGroups().Count == 5 && (cardNumberValueGroup.Last() - cardNumberValueGroup.First() == 4 ||
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

        private List<IGrouping<string, Card>> CardNumberGroups()
        {
            var cardNumberGroups = Cards
                .GroupBy(t => t.Number)
                .ToList();
            return cardNumberGroups;
        }

        public IList<Card> Cards { get; set; }
    }
}
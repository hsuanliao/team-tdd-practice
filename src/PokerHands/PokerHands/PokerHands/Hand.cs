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
                new StraightFlushRule(),
                new FourOfAKindRule(),
                new FullHouseRule(),
                new FlushRule(),
                new StraightRule(),
                new ThreeOfAKindRule(),
                new TwoPairsRule(),
                new PairRule(),
                new HighCardRule()
            };
            return rules.First(r => r.Match(Cards)).HandCategory;
        }

        public IList<Card> Cards { get; }

        public IList<int> KeyCards => GetKeyCards();

        private IList<int> GetKeyCards()
        {
            var keyCardValues = Cards
                .Select(c => c.NumberValue)
                .GroupBy(t => t)
                .Select(s => new
                {
                    KeyCardValue = s.Key,
                    Count = s.Count()
                })
                .OrderByDescending(o => o.Count)
                .ThenByDescending(t => t.KeyCardValue)
                .Select(k => k.KeyCardValue)
                .ToList();

            // HandCategory.Straight: 12345
            if (keyCardValues.Count == 5 && keyCardValues[0] - keyCardValues[1] == 9)
            {
                keyCardValues[0] = 1;
                keyCardValues = keyCardValues.OrderByDescending(o => o).ToList();
            }

            return keyCardValues;
        }
    }
}
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

        public IList<Card> Cards { get; set; }
    }
}
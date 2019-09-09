using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategoryRules
{
    internal class FullHouseRule : IHandCategoryRule
    {
        private readonly IList<Card> _cards;

        public FullHouseRule(IList<Card> cards)
        {
            _cards = cards;
        }

        public HandCategory HandCategory => HandCategory.FullHouse;

        public bool Match()
        {
            var cardNumberGroups = _cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 2 && cardNumberGroups.Any(t => t.Count() == 3) && cardNumberGroups.Any(t => t.Count() == 2);
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategoryRules
{
    internal class ThreeOfAKindRule : IHandCategoryRule
    {
        private readonly IList<Card> _cards;

        public ThreeOfAKindRule(IList<Card> cards)
        {
            _cards = cards;
        }

        public HandCategory HandCategory => HandCategory.ThreeOfAKind;

        public bool Match()
        {
            var cardNumberGroups = _cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 3 && cardNumberGroups.Any(t => t.Count() == 3);
        }
    }
}
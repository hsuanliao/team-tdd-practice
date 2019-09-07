using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategories
{
    internal class FourOfAKindRule : IHandCategoryRule
    {
        private readonly IList<Card> _cards;

        public HandCategory HandCategory => HandCategory.FourOfAKind;

        public FourOfAKindRule(IList<Card> cards)
        {
            _cards = cards;
        }

        public bool Match()
        {
            var cardNumberGroups = _cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 2 && cardNumberGroups.Any(t => t.Count() == 4);
        }
    }
}
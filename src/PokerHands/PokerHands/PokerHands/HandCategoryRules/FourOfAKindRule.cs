using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategoryRules
{
    internal class FourOfAKindRule : IHandCategoryRule
    {
        public HandCategory HandCategory => HandCategory.FourOfAKind;

        public bool Match(IList<Card> cards)
        {
            var cardNumberGroups = cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 2 && cardNumberGroups.Any(t => t.Count() == 4);
        }
    }
}
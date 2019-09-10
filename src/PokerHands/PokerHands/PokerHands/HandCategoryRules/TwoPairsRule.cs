using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategoryRules
{
    internal class TwoPairsRule : IHandCategoryRule
    {
        public HandCategory HandCategory => HandCategory.TwoPairs;

        public bool Match(IList<Card> cards)
        {
            var cardNumberGroups = cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 3 && cardNumberGroups.Count(t => t.Count() == 2) == 2;
        }
    }
}
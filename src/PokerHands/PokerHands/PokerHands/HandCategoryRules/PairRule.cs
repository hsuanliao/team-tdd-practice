using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategoryRules
{
    internal class PairRule : IHandCategoryRule
    {
        public HandCategory HandCategory => HandCategory.Pair;

        public bool Match(IList<Card> cards)
        {
            var cardNumberGroups = cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 4 && cardNumberGroups.Count(t => t.Count() == 2) == 1;
        }
    }
}
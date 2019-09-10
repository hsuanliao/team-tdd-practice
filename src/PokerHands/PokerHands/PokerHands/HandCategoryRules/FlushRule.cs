using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategoryRules
{
    internal class FlushRule : IHandCategoryRule
    {
        public HandCategory HandCategory => HandCategory.Flush;

        public bool Match(IList<Card> cards)
        {
            var cardSuitGroup = cards.GroupBy(s => s.Suit).ToList();
            return cardSuitGroup.Count == 1;
        }
    }
}
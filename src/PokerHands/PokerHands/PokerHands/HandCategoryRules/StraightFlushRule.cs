using System.Collections.Generic;

namespace PokerHands.HandCategoryRules
{
    internal class StraightFlushRule : IHandCategoryRule
    {
        public HandCategory HandCategory => HandCategory.StraightFlush;

        public bool Match(IList<Card> cards)
        {
            var isFlush = new FlushRule().Match(cards);
            var isStraight = new StraightRule().Match(cards);
            return isStraight && isFlush;
        }
    }
}
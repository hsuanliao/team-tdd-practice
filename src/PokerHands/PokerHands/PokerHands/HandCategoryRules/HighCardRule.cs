using System.Collections.Generic;

namespace PokerHands.HandCategoryRules
{
    internal class HighCardRule : IHandCategoryRule
    {
        public HandCategory HandCategory => HandCategory.HighCard;
        public bool Match(IList<Card> cards)
        {
            return true;
        }
    }
}
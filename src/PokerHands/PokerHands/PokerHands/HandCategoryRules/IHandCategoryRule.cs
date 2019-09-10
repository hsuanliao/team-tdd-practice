using System.Collections.Generic;

namespace PokerHands.HandCategoryRules
{
    internal interface IHandCategoryRule
    {
        HandCategory HandCategory { get; }
        bool Match(IList<Card> cards);
    }
}
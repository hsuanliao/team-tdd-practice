using System.Collections.Generic;

namespace PokerHands.HandCategories
{
    internal interface IHandCategoryRule
    {
        HandCategory HandCategory { get; }

        bool Match(IList<Card> cards);
    }
}
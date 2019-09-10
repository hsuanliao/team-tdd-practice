using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategoryRules
{
    internal class StraightRule : IHandCategoryRule
    {
        public HandCategory HandCategory => HandCategory.Straight;

        public bool Match(IList<Card> cards)
        {
            var cardNumberGroups = cards.GroupBy(t => t.Number).ToList();
            var cardSequenceNumberValues = cards
                .Select(t => t.NumberValue)
                .OrderBy(o => o)
                .ToList();
            return cardNumberGroups.Count == 5 &&
                   (cardSequenceNumberValues.Last() - cardSequenceNumberValues.First() == 4 ||
                    cardSequenceNumberValues.Last() - cardSequenceNumberValues[3] == 9);
        }
    }
}
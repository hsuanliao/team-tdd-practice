using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategories
{
    internal class FullHouseRule : IHandCategoryRule
    {
        public HandCategory HandCategory => HandCategory.FullHouse;

        public bool Match(IList<Card> cards)
        {
            var cardNumberGroups = cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 2 && cardNumberGroups.Any(t => t.Count() == 3) && cardNumberGroups.Any(t => t.Count() == 2);
        }
    }
}
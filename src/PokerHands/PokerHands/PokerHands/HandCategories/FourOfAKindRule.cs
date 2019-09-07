using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategories
{
    internal class FourOfAKindRule
    {
        public IList<Card> Cards { get; }
        public HandCategory HandCategory => HandCategory.FourOfAKind;

        public FourOfAKindRule(IList<Card> cards)
        {
            Cards = cards;
        }

        public bool Match()
        {
            var cardNumberGroups = Cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 2 && cardNumberGroups.Any(t => t.Count() == 4);
        }
    }
}
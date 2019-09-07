using System.Collections.Generic;

namespace PokerHands.HandCategories
{
    internal class FourOfAKindRule
    {
        public IList<Card> Cards { get; }

        public FourOfAKindRule(IList<Card> cards)
        {
            Cards = cards;
        }
    }
}
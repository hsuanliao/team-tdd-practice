﻿using System.Collections.Generic;
using System.Linq;

namespace PokerHands.HandCategoryRules
{
    internal class ThreeOfAKindRule : IHandCategoryRule
    {
        public HandCategory HandCategory => HandCategory.ThreeOfAKind;

        public bool Match(IList<Card> cards)
        {
            var cardNumberGroups = cards.GroupBy(t => t.Number).ToList();
            return cardNumberGroups.Count == 3 && cardNumberGroups.Any(t => t.Count() == 3);
        }
    }
}
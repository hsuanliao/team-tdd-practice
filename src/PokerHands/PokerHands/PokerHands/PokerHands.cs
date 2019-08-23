using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHands
{
    public class PokerHands
    {
        private readonly Dictionary<HandCategory, string> _handCategoryLookup = new Dictionary<HandCategory, string>
        {
            [HandCategory.FourOfAKind] = "four of a kind"
        };

        public string Duel(string firstHand, string secondHand)
        {
            var firstCardCombo = GetCardCombo(firstHand);
            var secondCardCombo = GetCardCombo(secondHand);

            if (string.Join(",", firstCardCombo) == string.Join(",", secondCardCombo))
            {
                return "Tie";
            }
            var firstHandCategory = GetHandCategory(firstCardCombo);
            var secondHandCategory = GetHandCategory(secondCardCombo);
            if (firstHandCategory == secondHandCategory && firstHandCategory == HandCategory.FourOfAKind)
            {
                return $"Tom wins. - with {_handCategoryLookup[firstHandCategory]}, key card 8";
            }

            throw new NotImplementedException();
        }

        private HandCategory GetHandCategory(IList<string> cardCombo)
        {
            var groups = cardCombo.GroupBy(t => t).ToList();
            if (groups.Count == 2 && groups.Any(t => t.Count() == 4))
            {
                return HandCategory.FourOfAKind;
            }

            throw new NotImplementedException();
        }

        private static IList<string> GetCardCombo(string firstHand)
        {
            return firstHand.Split(',').Select(t => new Card(t).Number).OrderBy(t => t).ToList();
        }
    }
}
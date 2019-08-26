using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHands
{
    public class PokerHands
    {
        private readonly string _firstPlayerName;
        private readonly string _secondPlayerName;

        private readonly Dictionary<HandCategory, string> _handCategoryLookup = new Dictionary<HandCategory, string>
        {
            [HandCategory.FourOfAKind] = "four of a kind"
        };

        public PokerHands(string firstPlayerName, string secondPlayerName)
        {
            _firstPlayerName = firstPlayerName;
            _secondPlayerName = secondPlayerName;
        }

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
                var firstKeyCardValues = GetKeyCardValues(firstHand);
                var secondKeyCardValues = GetKeyCardValues(secondHand);
                string keyCard = null;
                string playerName = null;
                for (int i = 0; i < firstKeyCardValues.Count; i++)
                {
                    if (firstKeyCardValues[i] != secondKeyCardValues[i])
                    {
                        var winsKeyCardValue = firstKeyCardValues[i] > secondKeyCardValues[i] ? firstKeyCardValues[i] : secondKeyCardValues[i];
                        playerName = firstKeyCardValues[i] > secondKeyCardValues[i]? _firstPlayerName : _secondPlayerName;
                        switch (winsKeyCardValue)
                        {
                            case 10:
                                keyCard = "T";
                                break;
                            case 11:
                                keyCard = "J";
                                break;
                            case 12:
                                keyCard = "Q";
                                break;
                            case 13:
                                keyCard = "K";
                                break;
                            case 14:
                                keyCard = "A";
                                break;
                            default:
                                keyCard = winsKeyCardValue.ToString();
                                break;
                        }

                        break;
                    }
                }
                return $"{playerName} wins. - with {_handCategoryLookup[firstHandCategory]}, key card {keyCard}";
            }

            throw new NotImplementedException();
        }

        private static IList<int> GetKeyCardValues(string firstHand)
        {
            var firstKeyCardValues = firstHand
                .Split(',')
                .Select(t => new Card(t).NumberValue)
                .GroupBy(t => t)
                .Select(s => new
                {
                    KeyCardValue = s.Key,
                    Count = s.Count()
                })
                .OrderByDescending(o => o.Count)
                .ThenByDescending(t => t.KeyCardValue)
                .Select(k =>k.KeyCardValue)
                .ToList();
            return firstKeyCardValues;
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
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
            [HandCategory.FourOfAKind] = "four of a kind",
            [HandCategory.FullHouse] = "full house",
            [HandCategory.ThreeOfAKind] = "three of a kind",
            [HandCategory.TwoPairs] = "two pairs",
            [HandCategory.Straight] = "straight"
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
            var firstHandCategory = GetHandCategory(firstHand);
            var secondHandCategory = GetHandCategory(secondHand);
            if (firstHandCategory == secondHandCategory)
            {
                var compareResult = CompareSameHandCategory(firstHand, secondHand, out var keyCard);
                var playerName = compareResult > 0 ? _firstPlayerName : _secondPlayerName;
                return $"{playerName} wins. - with {_handCategoryLookup[firstHandCategory]}, key card {keyCard}";
            }

            var winner = firstHandCategory > secondHandCategory ? _firstPlayerName : _secondPlayerName;
            var winnerHandCategory = firstHandCategory > secondHandCategory ? firstHandCategory : secondHandCategory;
            return $"{winner} wins. - with {_handCategoryLookup[winnerHandCategory]}";

            throw new NotImplementedException();
        }

        private static int CompareSameHandCategory(string firstHand, string secondHand, out string keyCard)
        {
            keyCard = string.Empty;
            int compareResult = 0;
            var firstKeyCardValues = GetKeyCardValues(firstHand);
            var secondKeyCardValues = GetKeyCardValues(secondHand);

            // 判斷兩手牌, 是否同時為順子, 且一手牌為 TJQKA, 一手牌為 A2345
            // 若是KeyCard比較時.不比較A
            if (firstKeyCardValues.Union(secondKeyCardValues).Count(v => v == 14 || v == 1) == 2)
            {
                firstKeyCardValues = firstKeyCardValues.Except(new[] {1, 14}).ToList();
                secondKeyCardValues = secondKeyCardValues.Except(new[] {1, 14}).ToList();
            }

            for (int i = 0; i < firstKeyCardValues.Count; i++)
            {
                if (firstKeyCardValues[i] != secondKeyCardValues[i])
                {
                    compareResult = firstKeyCardValues[i] > secondKeyCardValues[i] ? 1 : -1;
                    var winsKeyCardValue = compareResult > 0 ? firstKeyCardValues[i] : secondKeyCardValues[i];
                    switch (winsKeyCardValue)
                    {
                        case 10:
                            keyCard = "T";
                            break;
                        case 11:
                            keyCard = "Jack";
                            break;
                        case 12:
                            keyCard = "Q";
                            break;
                        case 13:
                            keyCard = "King";
                            break;
                        case 14:
                            keyCard = "Ace";
                            break;
                        default:
                            keyCard = winsKeyCardValue.ToString();
                            break;
                    }

                    break;
                }
            }

            return compareResult;
        }

        private static IList<int> GetKeyCardValues(string hand)
        {
            var keyCardValues = hand
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

            // HandCategory.Straight: 12345
            if (keyCardValues.Count == 5 && keyCardValues[0] - keyCardValues[1] == 9)
            {
                keyCardValues[0] = 1;
                keyCardValues = keyCardValues.OrderByDescending(o => o).ToList();
            }

            return keyCardValues;
        }

        private static HandCategory GetHandCategory(string hand)
        {
            var groups = GetCardCombo(hand).GroupBy(t => t).ToList();
            if (groups.Count == 2 && groups.Any(t => t.Count() == 4))
            {
                return HandCategory.FourOfAKind;
            }

            if (groups.Count == 2 && groups.Any(t => t.Count() == 3) && groups.Any(t => t.Count() == 2))
            {
                return HandCategory.FullHouse;
            }

            if (groups.Count == 3 && groups.Any(t => t.Count() == 3))
            {
                return HandCategory.ThreeOfAKind;
            }
            if (groups.Count == 3 && groups.Count(t => t.Count() == 2) == 2)
            {
                return HandCategory.TwoPairs;
            }
            // TD,JS,QH,KD,AS => 10,11,12,13,14
            var cardValueCombo = hand
                .Split(',')
                .Select(t => new Card(t).NumberValue)
                .OrderBy(o => o)
                .ToList();
            if (groups.Count == 5 && (cardValueCombo.Last() - cardValueCombo.First() == 4 || cardValueCombo.Last() - cardValueCombo[3] == 9))
            {
                return HandCategory.Straight;
            }

            throw new NotImplementedException();
        }

        private static IList<string> GetCardCombo(string firstHand)
        {
            return firstHand.Split(',').Select(t => new Card(t).Number).OrderBy(t => t).ToList();
        }
    }
}
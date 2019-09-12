using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;

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
            [HandCategory.Straight] = "straight",
            [HandCategory.Flush] = "flush",
            [HandCategory.StraightFlush] = "straight flush",
            [HandCategory.Pair] = "pair",
            [HandCategory.HighCard] = "high card"
        };

        public PokerHands(string firstPlayerName, string secondPlayerName)
        {
            _firstPlayerName = firstPlayerName;
            _secondPlayerName = secondPlayerName;
        }

        public string Duel(string firstHand, string secondHand)
        {
            int compareResult;
            string keyCard = null;

            var firstHandCategory = new Hand(firstHand).Category;
            var secondHandCategory = new Hand(secondHand).Category;
            if (firstHandCategory == secondHandCategory)
            {
                compareResult = CompareSameHandCategory(firstHand, secondHand, out keyCard);
            }
            else
            {
                compareResult = firstHandCategory > secondHandCategory ? 1 : -1;
            }
            var winnerHandCategory = compareResult > 0 ? firstHandCategory : secondHandCategory;

            if (compareResult == 0)
            {
                return "Tie";
            }
            var winner = compareResult > 0 ? _firstPlayerName : _secondPlayerName;

            var result = $"{winner} wins. - with {_handCategoryLookup[winnerHandCategory]}";
            if (!keyCard.IsNullOrEmpty())
            {
                result += $", key card {keyCard}";
            }
            return result;
        }

        private static int CompareSameHandCategory(string firstHand, string secondHand, out string keyCard)
        {
            keyCard = string.Empty;
            int compareResult = 0;
            var firstKeyCardValues = new Hand(firstHand).KeyCards;
            var secondKeyCardValues = new Hand(secondHand).KeyCards;

            // 判斷兩手牌, 是否同時為順子, 且一手牌為 TJQKA, 一手牌為 A2345
            // 若是KeyCard比較時.不比較A
            if (firstKeyCardValues.Union(secondKeyCardValues).Count(v => v == 14 || v == 1) == 2)
            {
                firstKeyCardValues = firstKeyCardValues.Except(new[] { 1, 14 }).ToList();
                secondKeyCardValues = secondKeyCardValues.Except(new[] { 1, 14 }).ToList();
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
                            keyCard = "Ten";
                            break;

                        case 11:
                            keyCard = "Jack";
                            break;

                        case 12:
                            keyCard = "Queen";
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
    }
}
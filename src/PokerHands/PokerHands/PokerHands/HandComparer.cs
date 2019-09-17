using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHands
{
    internal class HandComparer : IComparer<Hand>
    {
        public int WinsKeyCardValue { get; private set; }
        public HandCategory WinnerHandCategory { get; private set; }

        public int Compare(Hand firstHand, Hand secondHand)
        {
            var compareResult = 0;
            if (firstHand == null)
            {
                throw new ArgumentNullException(nameof(firstHand));
            }
            if (secondHand == null)
            {
                throw new ArgumentNullException(nameof(secondHand));
            }

            var firstHandCategory = firstHand.Category;
            var secondHandCategory = secondHand.Category;
            if (firstHandCategory == secondHandCategory)
            {
                var firstKeyCardValues = firstHand.KeyCards;
                var secondKeyCardValues = secondHand.KeyCards;

                // 判斷兩手牌, 是否同時為順子, 且一手牌為 TJQKA, 一手牌為 A2345
                // 若是KeyCard比較時.不比較A
                if (firstKeyCardValues.Union(secondKeyCardValues).Count(v => v == 14 || v == 1) == 2)
                {
                    firstKeyCardValues = firstKeyCardValues.Except(new[] { 1, 14 }).ToList();
                    secondKeyCardValues = secondKeyCardValues.Except(new[] { 1, 14 }).ToList();
                }

                for (var i = 0; i < firstKeyCardValues.Count; i++)
                {
                    if (firstKeyCardValues[i] == secondKeyCardValues[i])
                    {
                        continue;
                    }

                    compareResult = firstKeyCardValues[i] > secondKeyCardValues[i] ? 1 : -1;
                    WinsKeyCardValue = compareResult > 0 ? firstKeyCardValues[i] : secondKeyCardValues[i];

                    break;
                }
            }
            else
            {
                compareResult = firstHandCategory > secondHandCategory ? 1 : -1;
            }

            WinnerHandCategory = compareResult > 0 ? firstHandCategory : secondHandCategory;

            return compareResult;
        }
    }
}
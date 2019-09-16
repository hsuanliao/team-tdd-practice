using System.Linq;

namespace PokerHands
{
    internal class HandComparer
    {
        private readonly Hand _secondHand;
        private readonly Hand _firstHand;
        public int WinsKeyCardValue { get; private set; }
        public HandCategory WinnerHandCategory { get; private set; }

        public HandComparer(Hand firstHand, Hand secondHand)
        {
            _firstHand = firstHand;
            _secondHand = secondHand;
        }

        public int Compare()
        {
            int compareResult;
            var firstHandCategory = _firstHand.Category;
            var secondHandCategory = _secondHand.Category;
            if (firstHandCategory == secondHandCategory)
            {
                int compareResult1 = 0;
                var firstKeyCardValues = _firstHand.KeyCards;
                var secondKeyCardValues = _secondHand.KeyCards;

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
                        compareResult1 = firstKeyCardValues[i] > secondKeyCardValues[i] ? 1 : -1;
                        WinsKeyCardValue = compareResult1 > 0 ? firstKeyCardValues[i] : secondKeyCardValues[i];

                        break;
                    }
                }

                compareResult = compareResult1;
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
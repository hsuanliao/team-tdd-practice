using System.Collections.Generic;

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
            var handComparer = new HandComparer();
            var compareResult = handComparer.Compare(new Hand(firstHand), new Hand(secondHand));
            return compareResult == 0
                ? "Tie"
                : $"{GetWinner(compareResult)} wins. - with {_handCategoryLookup[handComparer.WinnerHandCategory]}{GetKeyCardInfo(handComparer)}";
        }

        private string GetWinner(int compareResult)
        {
            var winner = compareResult > 0 ? _firstPlayerName : _secondPlayerName;
            return winner;
        }

        private static string GetKeyCardInfo(HandComparer handComparer)
        {
            return handComparer.WinsKeyCardValue <= 0 ? string.Empty : $", key card {KeyCardDisplay(handComparer.WinsKeyCardValue)}";
        }

        private static string KeyCardDisplay(int keyCardValue)
        {
            string winnerKeyCard = null;
            switch (keyCardValue)
            {
                case 10:
                    winnerKeyCard = "Ten";
                    break;

                case 11:
                    winnerKeyCard = "Jack";
                    break;

                case 12:
                    winnerKeyCard = "Queen";
                    break;

                case 13:
                    winnerKeyCard = "King";
                    break;

                case 14:
                    winnerKeyCard = "Ace";
                    break;

                default:
                    winnerKeyCard = keyCardValue.ToString();
                    break;
            }

            return winnerKeyCard;
        }
    }
}
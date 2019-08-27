using NUnit.Framework;

namespace PokerHands
{
    [TestFixture]
    public class PokerHandsTest
    {
        private PokerHands _pokerHands;

        [Test]
        public void S01_FourOfAKind_88885_88885_Tie()
        {
            DuelResultShouldBe("8D,8C,8H,8S,5D", "8D,8C,8H,8S,5D", "Tie");
        }

        [Test]
        public void S02_FourOfAKind_88885_88885_DifferentSuit_Tie()
        {
            DuelResultShouldBe("8D,8C,8H,8S,5D", "8D,8C,8H,8S,5C", "Tie");
        }

        [Test]
        public void S03_FourOfAKind_88885_44442_FirstWin_KeyCard_8()
        {
            DuelResultShouldBe("8D,8C,8H,8S,5C", "4D,4S,2D,4H,4C", "Tom wins. - with four of a kind, key card 8");
        }

        [Test]
        public void S04_FourOfAKind_44442_44449_SecondWin_KeyCard_9()
        {
            DuelResultShouldBe("4D,4S,2D,4H,4C", "9S,4C,4H,4S,4D", "Joe wins. - with four of a kind, key card 9");
        }

        [Test]
        public void S05_FourOfAKind_FullHouse_FirstWin()
        {
            DuelResultShouldBe("5S,5D,5C,7S,5H", "KD,8D,8S,8H,KS", "Tom wins. - with four of a kind");
        }

        [Test]
        public void S06_FullHouse_777JJ_88822_SecondWin_KeyCard_8()
        {
            DuelResultShouldBe("AC,7S,7D,7H,AS", "8D,8C,8H,2S,2D", "Joe wins. - with full house, key card 8");
        }

        [Test]
        public void S07_FullHouse_777JJ_77744_FirstWin_KeyCard_Jack()
        {
            DuelResultShouldBe("JC,7S,7D,7H,JS", "7D,7H,4D,4S,7S", "Tom wins. - with full house, key card Jack");
        }



        [SetUp]
        public void Setup()
        {
            _pokerHands = new PokerHands("Tom", "Joe");
        }

        private void DuelResultShouldBe(string firstHand, string secondHand, string expected)
        {
            var actual = _pokerHands.Duel(firstHand, secondHand);

            Assert.AreEqual(expected, actual);
        }
    }
}
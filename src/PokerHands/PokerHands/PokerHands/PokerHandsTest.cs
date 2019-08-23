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

        [SetUp]
        public void Setup()
        {
            _pokerHands = new PokerHands();
        }

        private void DuelResultShouldBe(string firstHand, string secondHand, string expected)
        {
            var actual = _pokerHands.Duel(firstHand, secondHand);

            Assert.AreEqual(expected, actual);
        }
    }
}
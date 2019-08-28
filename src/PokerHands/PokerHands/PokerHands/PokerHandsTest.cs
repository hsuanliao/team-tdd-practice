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

        [Test]
        public void S08_FullHouse_ThreeOfAKind_SecondWin()
        {
            DuelResultShouldBe("4H,4S,4D,2C,5S", "2D,4S,2S,4D,4H", "Joe wins. - with full house");
        }

        [Test]
        public void S09_ThreeOfAKind_3332A_44425_SecondWin_KeyCard_4()
        {
            DuelResultShouldBe("3C,3D,3H,AC,2S", "4H,4D,4S,2C,5S", "Joe wins. - with three of a kind, key card 4");
        }

        [Test]
        public void S10_ThreeOfAKind_3332A_33324_FirstWin_KeyCard_Ace()
        {
            DuelResultShouldBe("3C,3D,3H,AC,2S", "3C,3D,3H,2D,4S", "Tom wins. - with three of a kind, key card Ace");
        }

        [Test]
        public void S11_ThreeOfAKind_3335A_3332A_FirstWin_KeyCard_5()
        {
            DuelResultShouldBe("3C,3D,3H,AC,5S", "3C,3D,3H,AC,2S", "Tom wins. - with three of a kind, key card 5");
        }
        [Test]
        public void S12_ThreeOfAKind_TwoPairs_FirstWin()
        {
            DuelResultShouldBe("3D,AC,3C,5S,3H", "KS,KC,2S,5D,2H", "Tom wins. - with three of a kind");
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
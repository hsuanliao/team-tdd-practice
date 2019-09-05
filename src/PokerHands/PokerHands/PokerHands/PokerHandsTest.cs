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

        [Test]
        public void S13_TwoPairs_22JJ5_55883_FirstWin_KeyCard_Jack()
        {
            DuelResultShouldBe("5C,JS,JH,2S,2C", "8S,8C,5D,5S,3S", "Tom wins. - with two pairs, key card Jack");
        }

        [Test]
        public void S14_TwoPairs_22JJ5_55JJ3_SecondWin_KeyCard_5()
        {
            DuelResultShouldBe("5C,JS,JH,2S,2C", "5C,JD,JS,5D,3H", "Joe wins. - with two pairs, key card 5");
        }

        [Test]
        public void S15_TwoPairs_55JJA_55JJ3_FirstWin_KeyCard_Ace()
        {
            DuelResultShouldBe("5D,5S,AD,JS,JD", "5C,JD,JS,5D,3H", "Tom wins. - with two pairs, key card Ace");
        }

        [Test]
        public void S16_Straight_ThreeOfAKind_FirstWin()
        {
            DuelResultShouldBe("TD,JS,QH,KD,AS", "4H,4S,4D,2C,5S", "Tom wins. - with straight");
        }

        [Test]
        public void S17_Straight_TJQKA_A2345_FirstWin_KeyCard_King()
        {
            DuelResultShouldBe("TD,JS,QH,KD,AS", "2D,5S,4C,3C,AC", "Tom wins. - with straight, key card King");
        }

        [Test]
        public void S18_Straight_TJQKA_45678_FirstWin_KeyCard_Ace()
        {
            DuelResultShouldBe("TD,JS,QH,KD,AS", "8D,6S,4S,5C,7D", "Tom wins. - with straight, key card Ace");
        }

        [Test]
        public void S19_Straight_A2345_45678_SecondWin_KeyCard_8()
        {
            DuelResultShouldBe("2D,5S,4C,3C,AC", "8D,6S,4S,5C,7D", "Joe wins. - with straight, key card 8");
        }

        [Test]
        public void S20_Flush_Straight_FirstWin()
        {
            DuelResultShouldBe("4D,6D,8D,2D,AD", "TC,8D,QS,9S,JS", "Tom wins. - with flush");
        }

        [Test]
        public void S21_Flush_259QA_35TJA_FirstWin_KeyCard_Queen()
        {
            DuelResultShouldBe("2S,9S,QS,5S,AS", "3H,5H,AH,TH,JH", "Tom wins. - with flush, key card Queen");
        }

        [Test]
        public void S22_Flush_StraightFlush_SecondWin()
        {
            DuelResultShouldBe("2C,6C,8C,4C,TC", "3C,4C,5C,6C,7C", "Joe wins. - with straight flush");
        }

        [Test]
        public void S23_Pair_TwoPairs_SecondWin()
        {
            DuelResultShouldBe("5S,JH,6S,JC,AS", "5D,7D,6S,6C,5H", "Joe wins. - with two pairs");
        }

        [Test]
        public void S24_HighCard_Pair_SecondWin()
        {
            DuelResultShouldBe("JD,7D,KS,6S,3C", "KD,AS,4C,AH,6D", "Joe wins. - with pair");
        }

        [Test]
        public void S25_HighCard_3479K_248TK_SecondWin_KeyCard_Ten()
        {
            DuelResultShouldBe("KS,7D,3C,9H,4D", "2D,4S,KD,TS,8S", "Joe wins. - with high card, key card Ten");
        }

        [Test]
        public void S26_Flush_HighCard_SamePoint_FirstWin()
        {
            DuelResultShouldBe("2C,6C,8C,4C,TC", "TC,8D,4H,6S,2C", "Tom wins. - with flush");
        }

        [Test]
        public void S27_HighCard_367JA_367JA_Tie()
        {
            DuelResultShouldBe("JD,7D,AS,6S,3C", "AS,JD,7D,3C,6S", "Tie");
        }

        [Test]
        public void S28_StraightFlush_FourOfAKind_FirstWin()
        {
            DuelResultShouldBe("3D,5D,7D,4D,6D", "AS,AH,AD,AC,KD", "Tom wins. - with straight flush");
        }

        [Test]
        public void S29_Flush_FullHouse_SecondWin()
        {
            DuelResultShouldBe("8S,4S,TS,KS,AS", "2D,4S,2S,4D,4H", "Joe wins. - with full house");
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
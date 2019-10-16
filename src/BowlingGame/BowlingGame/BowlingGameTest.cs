using NUnit.Framework;

namespace BowlingGame
{
    [TestFixture]
    public class BowlingGameTest
    {
        [Test]
        public void A01_SingleFrame_InvalidInput()
        {
            ScoreShouldBe("57", "Invalid!!");
        }

        [Test]
        public void A02_SingleFrame_InputIs54()
        {
            ScoreShouldBe("54", "9");
        }

        [Test]
        public void A03_MultiFrames_InputIs5454()
        {
            ScoreShouldBe("54,54", "9-18");
        }

        [Test]
        public void A04_MultiFrames_InputIs545480()
        {
            ScoreShouldBe("54,54,80", "9-18-26");
        }

        [Test]
        public void A05_MultiFrames_InputIs54548()
        {
            ScoreShouldBe("54,54,8", "9-18-");
        }

        [Test]
        public void A06_SingleFrame_InputIs5S()
        {
            ScoreShouldBe("5/", "");
        }

        [Test]
        public void A07_MultiFrames_InputIs5S5()
        {
            ScoreShouldBe("5/,5", "15-");
        }

        private static void ScoreShouldBe(string input, string expected)
        {
            var bowlingGame = new BowlingGame();
            var actual = bowlingGame.Score(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
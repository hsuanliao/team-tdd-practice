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

        [Test]
        public void A08_MultiFrames_InputIs5S51()
        {
            ScoreShouldBe("5/,51", "15-21");
        }

        [Test]
        public void A09_MultiFrames_InputIs5S5S51()
        {
            ScoreShouldBe("5/,5/,51", "15-30-36");
        }

        [Test]
        public void A10_MultiFrames_InputIsXX51()
        {
            ScoreShouldBe("X,X,51", "25-41-47");
        }

        private static void ScoreShouldBe(string input, string expected)
        {
            var bowlingGame = new BowlingGame();
            var actual = bowlingGame.Score(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
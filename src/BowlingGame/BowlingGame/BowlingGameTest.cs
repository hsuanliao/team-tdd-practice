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

        private static void ScoreShouldBe(string input, string expected)
        {
            var bowlingGame = new BowlingGame();
            var actual = bowlingGame.Score(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
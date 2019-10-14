using NUnit.Framework;

namespace BowlingGame
{
    [TestFixture]
    public class BowlingGameTest
    {
        [Test]
        public void A01_SingleFrame_InvalidInput()
        {
            var input = "57";
            var bowlingGame = new BowlingGame();
            var actual = bowlingGame.Score(input);
            Assert.AreEqual("Invalid!!", actual);
        }
    }
}
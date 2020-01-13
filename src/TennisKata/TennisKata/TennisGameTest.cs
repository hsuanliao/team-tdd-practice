using NUnit.Framework;

namespace TennisKata
{
    [TestFixture]
    public class TennisGameTest
    {
        private TennisGame _tennisGame;

        [Test]
        public void A01_LoveAll()
        {
            ScoreShouldBe("Love All");
        }

        [Test]
        public void A02_FifteenLove()
        {
            _tennisGame.Player1Score();
            ScoreShouldBe("Fifteen Love");
        }

        [SetUp]
        public void Setup()
        {
            _tennisGame = new TennisGame();
        }

        private void ScoreShouldBe(string expected)
        {
            var actual = _tennisGame.Score();
            Assert.AreEqual(expected, actual);
        }
    }
}
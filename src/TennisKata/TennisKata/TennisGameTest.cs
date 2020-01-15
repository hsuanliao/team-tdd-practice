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
            GivePlayer1Score(1);
            ScoreShouldBe("Fifteen Love");
        }

        [Test]
        public void A03_ThirtyLove()
        {
            GivePlayer1Score(2);
            ScoreShouldBe("Thirty Love");
        }

        [Test]
        public void A04_FortyLove()
        {
            GivePlayer1Score(3);
            ScoreShouldBe("Forty Love");
        }

        [Test]
        public void A05_LoveFifteen()
        {
            GivePlayer2Score(1);
            ScoreShouldBe("Love Fifteen");
        }

        [Test]
        public void A06_LoveThirty()
        {
            GivePlayer2Score(2);
            ScoreShouldBe("Love Thirty");
        }

        [Test]
        public void A07_LoveForty()
        {
            GivePlayer2Score(3);
            ScoreShouldBe("Love Forty");
        }

        [Test]
        public void A08_FifteenAll()
        {
            GivePlayer1Score(1);
            GivePlayer2Score(1);
            ScoreShouldBe("Fifteen All");
        }

        [Test]
        public void A09_ThirtyAll()
        {
            GivePlayer1Score(2);
            GivePlayer2Score(2);
            ScoreShouldBe("Thirty All");
        }

        [Test]
        public void A10_Deuce_3X3()
        {
            GivePlayer1Score(3);
            GivePlayer2Score(3);
            ScoreShouldBe("Deuce");
        }

        [Test]
        public void A11_Deuce_4X4()
        {
            GivePlayer1Score(4);
            GivePlayer2Score(4);
            ScoreShouldBe("Deuce");
        }

        [SetUp]
        public void Setup()
        {
            _tennisGame = new TennisGame();
        }

        private void GivePlayer1Score(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _tennisGame.Player1Score();
            }
        }

        private void GivePlayer2Score(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _tennisGame.Player2Score();
            }
        }

        private void ScoreShouldBe(string expected)
        {
            var actual = _tennisGame.Score();
            Assert.AreEqual(expected, actual);
        }
    }
}
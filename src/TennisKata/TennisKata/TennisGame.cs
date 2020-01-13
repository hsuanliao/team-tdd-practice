namespace TennisKata
{
    public class TennisGame
    {
        private int _player1ScoreValue;

        public void Player1Score()
        {
            _player1ScoreValue++;
        }

        public string Score()
        {
            if (_player1ScoreValue == 1)
            {
                return "Fifteen Love";
            }

            return "Love All";
        }
    }
}
using System.Collections.Generic;

namespace TennisKata
{
    public class TennisGame
    {
        private int _player1ScoreValue;
        private int _player2ScoreValue;

        public void Player1Score()
        {
            _player1ScoreValue++;
        }

        public void Player2Score()
        {
            _player2ScoreValue++;
        }

        public string Score()
        {
            var lookup = new Dictionary<int, string>
            {
                {0, "Love"},
                {1, "Fifteen"},
                {2, "Thirty"},
                {3, "Forty"},
            };

            if (_player1ScoreValue > 0 || _player2ScoreValue > 0)
            {
                return $"{lookup[_player1ScoreValue]} {lookup[_player2ScoreValue]}";
            }

            return "Love All";
        }
    }
}
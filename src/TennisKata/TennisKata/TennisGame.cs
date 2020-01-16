using System;
using System.Collections.Generic;

namespace TennisKata
{
    public class TennisGame
    {
        private readonly string _firstPlayerName;
        private readonly string _secondPlayerName;
        private int _player1ScoreValue;
        private int _player2ScoreValue;

        public TennisGame(string firstPlayerName, string secondPlayerName)
        {
            _firstPlayerName = firstPlayerName;
            _secondPlayerName = secondPlayerName;
        }

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

            if (_player1ScoreValue == _player2ScoreValue)
            {
                if (_player1ScoreValue >= 3)
                {
                    return "Deuce";
                }

                return $"{lookup[_player1ScoreValue]} All";
            }

            if (_player1ScoreValue > 3 || _player2ScoreValue > 3)
            {
                var advPlayerName = _player1ScoreValue > _player2ScoreValue ? _firstPlayerName : _secondPlayerName;
                if (Math.Abs(_player1ScoreValue - _player2ScoreValue) == 1)
                {
                    return $"Advantage {advPlayerName}";
                }

                return $"Win for {advPlayerName}";
            }

            return $"{lookup[_player1ScoreValue]} {lookup[_player2ScoreValue]}";
        }
    }
}
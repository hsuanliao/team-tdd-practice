using System.Collections.Generic;
using System.Linq;

namespace BowlingGame
{
    public class Frame
    {
        public Frame(string source)
        {
            var ball1 = ParseBall(source, 0);
            var ball2 = ParseBall(source, 1);

            if (ball1.Equals("X"))
            {
                FrameType = FrameType.Strike;
                CurrentBalls.Add(10);
            }
            else if (string.IsNullOrEmpty(ball2))
            {
                FrameType = FrameType.NotComplete;
            }
            else if (ball2.Equals("/"))
            {
                FrameType = FrameType.Spare;
                CurrentBalls.Add(10 - CurrentBalls.FirstOrDefault());
            }
        }

        private string ParseBall(string source, int index)
        {
            if (source.Length <= index)
            {
                return string.Empty;
            }

            var ball = source[index].ToString();

            if (int.TryParse(ball, out var ball1Value))
            {
                CurrentBalls.Add(ball1Value);
            }

            return ball;
        }

        private FrameType FrameType { get; }

        public bool IsValid
        {
            get
            {
                return FrameType != FrameType.Default || CurrentScore <= 9;
            }
        }

        private int CurrentScore => CurrentBalls.Sum();

        public int? Score
        {
            get
            {
                if (FrameType == FrameType.Strike)
                {
                    if (AfterBalls.Count < 2)
                    {
                        return null;
                    }

                    return 10 + AfterBalls.Take(2).Sum();
                }

                if (FrameType == FrameType.NotComplete)
                {
                    return null;
                }

                if (FrameType == FrameType.Spare)
                {
                    if (AfterBalls.Count == 0)
                    {
                        return null;
                    }

                    return 10 + AfterBalls.First();
                }

                return CurrentScore;
            }
        }

        public IList<int> CurrentBalls { get; } = new List<int>();

        public IList<int> AfterBalls { get; set; } = new List<int>();
    }

    public enum FrameType
    {
        Default,
        NotComplete,
        Spare,
        Strike
    }
}
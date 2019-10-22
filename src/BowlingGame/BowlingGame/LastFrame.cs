using System.Linq;

namespace BowlingGame
{
    public class LastFrame : Frame
    {
        public LastFrame(string source)
        {
            FrameType = FrameType.LastFrame;

            var ball1 = ParseBall(source, 0);
            if (ball1.Equals("X"))
            {
                CurrentBalls.Add(10);
            }

            var ball2 = ParseBall(source, 1);
            if (ball2.Equals("X"))
            {
                CurrentBalls.Add(10);
            }
            else if (ball2.Equals("/"))
            {
                CurrentBalls.Add(10 - CurrentBalls.FirstOrDefault());
            }

            var ball3 = ParseBall(source, 2);
            if (string.IsNullOrEmpty(ball3) && CurrentBalls.Take(2).Sum() > 10)
            {
                FrameType = FrameType.NotComplete;
            }
            else if (ball3.Equals("X"))
            {
                CurrentBalls.Add(10);
            }
            else if (ball3.Equals("/"))
            {
                CurrentBalls.Add(10 - CurrentBalls.Skip(1).Take(1).FirstOrDefault());
            }
        }
    }
}
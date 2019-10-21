namespace BowlingGame
{
    public class LastFrame : Frame
    {
        public LastFrame(string source)
            : base(source)
        {
            var ball2 = ParseBall(source, 1);
            var ball3 = ParseBall(source, 2);
            FrameType = FrameType.LastFrame;

            if (ball2.Equals("X"))
            {
                CurrentBalls.Add(10);
            }
            if (ball3.Equals("X"))
            {
                CurrentBalls.Add(10);
            }
        }
    }
}
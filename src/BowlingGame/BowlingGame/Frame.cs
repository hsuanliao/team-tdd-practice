namespace BowlingGame
{
    public class Frame
    {
        public int Ball1Value => int.TryParse(Ball1, out var tmp) ? tmp : 0;

        public int Ball2Value => int.TryParse(Ball2, out var tmp) ? tmp : 0;

        public Frame(string source)
        {
            Ball1 = source[0].ToString();
            Ball2 = source.Length == 1 ? string.Empty : source[1].ToString();
            if (Ball1.Equals("X"))
            {
                FrameType = FrameType.Strike;
            }
            else if (string.IsNullOrEmpty(Ball2))
            {
                FrameType = FrameType.NotComplete;
            }
            else if (Ball2.Equals("/"))
            {
                FrameType = FrameType.Spare;
            }
        }

        private FrameType FrameType { get; }

        public bool IsValid
        {
            get
            {
                return !Score.HasValue || CurrentScore <= 9;
            }
        }

        private int CurrentScore => Ball1Value + Ball2Value;

        public int? Score
        {
            get
            {
                if (FrameType == FrameType.Strike)
                {
                    // TODO: Calculate next two ball value
                }

                if (FrameType == FrameType.NotComplete)
                {
                    return null;
                }

                if (FrameType == FrameType.Spare)
                {
                    if (NextFrame == null)
                    {
                        return null;
                    }
                    return 10 + NextFrame?.Ball1Value ?? 0;
                }

                return CurrentScore;
            }
        }

        public string Ball2 { get; }

        public string Ball1 { get; }
        public Frame NextFrame { get; set; }
    }

    public enum FrameType
    {
        Default,
        NotComplete,
        Spare,
        Strike
    }
}
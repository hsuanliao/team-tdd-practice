namespace BowlingGame
{
    public class Frame
    {
        public int Ball1Value => int.Parse(Ball1);
        public int Ball2Value => int.Parse(Ball2);

        public Frame(string source)
        {
            Ball1 = source[0].ToString();
            Ball2 = source.Length == 1 ? string.Empty : source[1].ToString();
            if (string.IsNullOrEmpty(Ball2))
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
                // TODO: solve this problem
                return !Score.HasValue || Score <= 9;
            }
        }

        public int? Score
        {
            get
            {
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

                return Ball1Value + Ball2Value;
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
        Spare
    }
}
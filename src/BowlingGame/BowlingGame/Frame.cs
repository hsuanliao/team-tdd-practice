namespace BowlingGame
{
    public class Frame
    {
        public Frame(string source)
        {
            Ball1 = source[0].ToString();
            Ball2 = source[1].ToString();
        }

        public bool IsValid
        {
            get
            {
                var ball1Value = int.Parse(Ball1);
                var ball2Value = int.Parse(Ball2);
                return ball1Value + ball2Value <= 9;
            }
        }

        public string Ball2 { get; }

        public string Ball1 { get; }
    }
}
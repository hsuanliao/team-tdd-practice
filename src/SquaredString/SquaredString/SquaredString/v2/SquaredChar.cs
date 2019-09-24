namespace SquaredString.v2
{
    public class SquaredChar
    {
        public char Value { get; set; }
        public int PointX { get; set; }
        public int PointY { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
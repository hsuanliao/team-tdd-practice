namespace SquaredString.v2
{
    public class Rot90ClockStrategy
    {
        public SquaredCharCollection Transform(SquaredCharCollection source)
        {
            var result = new SquaredCharCollection();

            var maxPointY = source.MaxPointY;

            foreach (var squaredChar in source)
            {
                var target = new SquaredChar
                {
                    Value = squaredChar.Value,
                    PointX = squaredChar.PointY,
                    PointY = maxPointY - squaredChar.PointX
                };
                result.Add(target);
            }

            return result;
        }
    }
}
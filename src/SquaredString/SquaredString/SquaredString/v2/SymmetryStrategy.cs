namespace SquaredString.v2
{
    public class SymmetryStrategy
    {
        public SquaredCharCollection Transform(SquaredCharCollection source)
        {
            var result = new SquaredCharCollection();

            foreach (var squaredChar in source)
            {
                var target = new SquaredChar
                {
                    Value = squaredChar.Value,
                    PointX = squaredChar.PointY,
                    PointY = squaredChar.PointX
                };
                result.Add(target);
            }

            return result;
        }
    }
}
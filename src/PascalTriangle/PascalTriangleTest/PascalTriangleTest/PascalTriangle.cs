using System.Collections.Generic;
using System.Linq;

namespace PascalTriangleTest
{
    public class PascalTriangle
    {
        private readonly List<TriangleElement> _elements = new List<TriangleElement>();

        public string Transform(int layerCount)
        {
            BuildTriangle(layerCount);

            return Output();
        }

        private string Output()
        {
            var joinLine = _elements.GroupBy(x => x.PointY)
                .Select(g => string.Join("-", g.ToList()));

            return string.Join(",", joinLine);
        }

        private void BuildTriangle(int layerCount)
        {
            for (var i = 0; i < layerCount; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    AppendElement(i, j);
                }
            }
        }

        private void AppendElement(int pointY, int pointX)
        {
            var element = new TriangleElement
            {
                PointX = pointX,
                PointY = pointY,
                Value = Calculate(pointX, pointY)
            };
            _elements.Add(element);
        }

        private int Calculate(int pointX, int pointY)
        {
            if (_elements.Count == 0)
            {
                return 1;
            }

            return GetValue(pointX, pointY - 1) + GetValue(pointX - 1, pointY - 1);
        }

        private int GetValue(int pointX, int pointY)
        {
            var element = _elements.SingleOrDefault(e => e.PointX == pointX && e.PointY == pointY);

            return element?.Value ?? 0;
        }
    }

    public class TriangleElement
    {
        public int PointX { get; set; }
        public int PointY { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
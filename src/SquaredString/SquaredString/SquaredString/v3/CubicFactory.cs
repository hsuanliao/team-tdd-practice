using System;
using System.Collections.Generic;
using System.Linq;

namespace SquaredString.v3
{
    public class CubicFactory
    {
        private List<CubeElement> _cubeElementList;

        public CubicFactory(string input)
        {
            _cubeElementList = new List<CubeElement>();
            var cubeRows = input.Split('\n');
            for (int i = 0; i < cubeRows.Length; i++)
            {
                for (int j = 0; j < cubeRows[i].Length; j++)
                {
                    _cubeElementList.Add(new CubeElement
                    {
                        Value = cubeRows[i][j],
                        PointX = i,
                        PointY = j
                    });
                }
            }
        }

        public string DiagInvert()
        {
            var invertedCubic = _cubeElementList.Select(x => new CubeElement()
            {
                PointY = x.PointX,
                PointX = x.PointY,
                Value = x.Value
            });

            return ConvertToResultString(invertedCubic);
        }

        private static string ConvertToResultString(IEnumerable<CubeElement> cubeElementList)
        {
            string result = string.Empty;
            foreach (var group in cubeElementList.GroupBy(x => x.PointX))
            {
                var list = @group.OrderBy(t => t.PointY);
                foreach (var cubeElement in list)
                {
                    result += cubeElement.Value.ToString();
                }

                result += "\n";
            }

            return result.TrimEnd('\n');
        }

        public string Rotate90Clockwise()
        {
            var rowCount = _cubeElementList.Max(x => x.PointY);
            var invertedCubic = _cubeElementList.Select(x => new CubeElement()
            {
                PointY = rowCount - x.PointX,
                PointX = x.PointY,
                Value = x.Value
            });

            return ConvertToResultString(invertedCubic);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SquaredString.v2
{
    public class SquaredCharCollection : IEnumerable<SquaredChar>
    {
        private List<SquaredChar> _squaredChars;

        public SquaredCharCollection(string input)
        {
            var inputArr = input.Split('\n');
            _squaredChars = new List<SquaredChar>();
            for (var i = 0; i < inputArr.Length; i++)
            {
                for (var j = 0; j < inputArr[i].Length; j++)
                {
                    _squaredChars.Add(new SquaredChar
                    {
                        Value = inputArr[i][j],
                        PointX = i,
                        PointY = j
                    });
                }
            }
        }

        public SquaredCharCollection()
        {
            _squaredChars = new List<SquaredChar>();
        }

        public int MaxPointY => _squaredChars.Max(item => item.PointY);

        public string Output()
        {
            var lines = ConvertToLineArray(_squaredChars);

            return string.Join("\n", lines);
        }

        private IEnumerable<string> ConvertToLineArray(IEnumerable<SquaredChar> squaredChars)
        {
            var lines = squaredChars.GroupBy(item => item.PointX)
                .Select(g => string.Join("", g.OrderBy(i => i.PointY)));
            return lines;
        }

        public IEnumerator<SquaredChar> GetEnumerator()
        {
            return _squaredChars.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(SquaredChar target)
        {
            _squaredChars.Add(target);
        }

        public string LineJoin(SquaredCharCollection anotherCollection)
        {
            var selfie = ConvertToLineArray(_squaredChars);
            var another = ConvertToLineArray(anotherCollection.SquaredChars);

            return string.Join("\n", selfie.Zip(another, (f, s) => string.Concat(f, "|", s)));
        }

        internal IEnumerable<SquaredChar> SquaredChars => _squaredChars;
    }
}
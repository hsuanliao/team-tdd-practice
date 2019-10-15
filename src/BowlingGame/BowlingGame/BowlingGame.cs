using System;
using System.Linq;

namespace BowlingGame
{
    public class BowlingGame
    {
        public string Score(string input)
        {
            var frameSet = input.Split(',')
                .Select(x => new Frame(x));

            if (frameSet.Any(x => !x.IsValid))
            {
                return "Invalid!!";
            }

            //string.Join("-", frameSet.Select(f => f.Score));
            //return frameSet.Aggregate(string.Empty,
            //    (result, next) => string.Concat(result, next.Score.ToString(), "-"))
            //    .TrimEnd('-');
            return frameSet.First().Score.ToString();
        }
    }
}
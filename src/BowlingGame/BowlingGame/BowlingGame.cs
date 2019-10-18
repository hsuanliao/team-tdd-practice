using System.Linq;

namespace BowlingGame
{
    public class BowlingGame
    {
        private int _totalScore;

        public string Score(string input)
        {
            var frameSet = input.Split(',')
                .Select(x => new Frame(x))
                .ToList();


            //frameSet.ForEach(
            //    frame =>
            //    {
            //        var index = frameSet.IndexOf(frame);
            //        frame.AfterBalls = frameSet.Skip(index + 1).SelectMany(f => f.CurrentBalls).ToList();
            //    });
            for (var i = 0; i < frameSet.Count - 1; i++)
            {
                frameSet[i].AfterBalls = frameSet.Skip(i + 1).SelectMany(f => f.CurrentBalls).ToList();
            }
            //for (var i = 0; i < frameSet.Count - 1; i++)
            //{
            //    frameSet[i].NextFrame = frameSet[i + 1];
            //    //frameSet[i].NextNextFrame = frameSet[i + 1+1];
            //}

            if (frameSet.Any(x => !x.IsValid))
            {
                return "Invalid!!";
            }

            return string.Join("-", frameSet.Select(f => CalculateTotalScore(f.Score)));
            //return frameSet.Aggregate(string.Empty,
            //    (result, next) => string.Concat(result, next.Score.ToString(), "-"))
            //    .TrimEnd('-');
            //return frameSet.First().Score.ToString();
        }

        private string CalculateTotalScore(int? frameScore)
        {
            if (!frameScore.HasValue)
            {
                return string.Empty;
            }
            _totalScore += frameScore.Value;
            return _totalScore.ToString();
        }
    }
}
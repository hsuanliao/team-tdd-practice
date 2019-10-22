using System.Collections.Generic;
using System.Linq;

namespace BowlingGame
{
    public class BowlingGame
    {
        private int _totalScore;

        public string Score(string input)
        {
            var frameSet = input.Split(',')
                .Select((s, i) => i < 9 ? new Frame(s) : new LastFrame(s))
                //.Select(x => new Frame(x))
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

            return DisplayScore(frameSet);
            //return frameSet.Aggregate(string.Empty,
            //    (result, next) => string.Concat(result, next.Score.ToString(), "-"))
            //    .TrimEnd('-');
            //return frameSet.First().Score.ToString();
        }

        private string DisplayScore(List<Frame> frameSet)
        {
            var frameScore = string.Join("-", frameSet.Select(f => CalculateTotalScore(f.Score)));
            if (frameSet.Count == 10 && frameSet.All(x => x.FrameType != FrameType.NotComplete))
            {
                frameScore += $" (total: {_totalScore})";
            }

            return frameScore;
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
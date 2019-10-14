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

            throw new NotImplementedException();
        }
    }
}
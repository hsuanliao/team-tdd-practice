namespace SquaredString
{
    public class SquaredFactory
    {
        private readonly string _input;

        public SquaredFactory(string input)
        {
            _input = input;
        }

        public string Symmetry()
        {
            var charArr = new char[2, 2];
            var sourceArr = _input.Split('\n');

            for (var i = 0; i < charArr.GetLength(0); i++)
            {
                for (var j = 0; j < charArr.GetLength(1); j++)
                {
                    charArr[i, j] = sourceArr[i][j];
                }
            }

            for (var i = 0; i < charArr.GetLength(0) - 1; i++)
            {
                for (var j = i + 1; j < charArr.GetLength(1); j++)
                {
                    Swap(ref charArr[i, j], ref charArr[j, i]);
                }
            }
            //for (var i = 0; i < charArr.GetLength(0); i++)
            //{
            //    for (var j = 0; j < charArr.GetLength(1); j++)
            //    {
            //        if (i <= j)
            //        {
            //            continue;
            //        }
            //        Swap(ref charArr[i, j], ref charArr[j, i]);
            //    }
            //}

            var output = string.Empty;
            for (var i = 0; i < charArr.GetLength(0); i++)
            {
                for (var j = 0; j < charArr.GetLength(1); j++)
                {
                    output += charArr[i, j];
                }

                output += '\n';
            }

            return output.TrimEnd('\n');
        }

        public void Swap(ref char a, ref char b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
}
using System.Linq;
using System.Net.Mail;

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
            var charArr = ConvertToArray();

            SymmetryArray(charArr);

            return ConvertToString(charArr);
        }

        private void SymmetryArray(char[,] charArr)
        {
            for (var i = 0; i < charArr.GetLength(0) - 1; i++)
            {
                for (var j = i + 1; j < charArr.GetLength(1); j++)
                {
                    Swap(ref charArr[i, j], ref charArr[j, i]);
                }
            }
        }

        private static string ConvertToString(char[,] charArr)
        {
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

        private char[,] ConvertToArray()
        {
            var sourceArr = _input.Split('\n');
            var charArr = new char[sourceArr.Length, sourceArr.Length];

            for (var i = 0; i < charArr.GetLength(0); i++)
            {
                for (var j = 0; j < charArr.GetLength(1); j++)
                {
                    charArr[i, j] = sourceArr[i][j];
                }
            }

            return charArr;
        }

        public void Swap(ref char a, ref char b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public string Rot_90_Clock()
        {
            var source = Symmetry();

            var sourceArr = source.Split('\n');

            for (var i = 0; i < sourceArr.Length; i++)
            {
                sourceArr[i] = string.Join("", sourceArr[i].Reverse());
            }

            return string.Join("\n", sourceArr);
        }

        public string Selfie_And_Symmetry()
        {
            var selfieArr = _input.Split('\n');
            var symmetryArr = Symmetry().Split('\n');

            var resultArr = selfieArr.Zip(symmetryArr, (first, second) => string.Concat(first, "|", second));
            return string.Join("\n", resultArr);
        }
    }
}
using NUnit.Framework;

namespace SquaredString.v3
{
    [TestFixture]
    public class CubicStringTest
    {
        [Test]
        public void DiagInvert_2x2()
        {
            var input = "ab\ncd";
            var cubic = new CubicFactory(input);

            var output = cubic.DiagInvert();

            Assert.AreEqual("ac\nbd", output);
        }

        [Test]
        public void Symmetry_3x3()
        {
            var input = "abc\ndef\nghi";
            var cubic = new CubicFactory(input);

            var output = cubic.DiagInvert();

            Assert.AreEqual("adg\nbeh\ncfi", output);
        }

        [Test]
        public void Symmetry_4x4()
        {
            var input = "abcd\nefgh\nijkl\nmnop";
            var cubic = new CubicFactory(input);

            var output = cubic.DiagInvert();

            Assert.AreEqual("aeim\nbfjn\ncgko\ndhlp", output);
        }

        [Test]
        public void Rotate90Clockwise_4x4()
        {
            var input = "abcd\nefgh\nijkl\nmnop";
            var cubic = new CubicFactory(input);

            var output = cubic.Rotate90Clockwise();

            Assert.AreEqual("miea\nnjfb\nokgc\nplhd", output);
        }
    }

    public class CubeElement
    {
        public char Value { get; set; }
        public int PointX { get; set; }
        public int PointY { get; set; }
    }
}
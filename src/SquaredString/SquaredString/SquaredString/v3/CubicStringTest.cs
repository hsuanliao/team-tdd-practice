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
    }

    public class CubeElement
    {
        public char Value { get; set; }
        public int PointX { get; set; }
        public int PointY { get; set; }
    }
}
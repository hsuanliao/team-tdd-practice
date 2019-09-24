using NUnit.Framework;

namespace SquaredString.v2
{
    [TestFixture]
    public class SquaredStringTest
    {
        [Test]
        public void Symmetry_2x2()
        {
            var input = "ab\ncd";
            var service = new SquaredStringService(input);

            var output = service.Symmetry();

            Assert.AreEqual("ac\nbd", output);
        }

        [Test]
        public void Symmetry_3x3()
        {
            var input = "abc\ndef\nghi";
            var service = new SquaredStringService(input);

            var output = service.Symmetry();

            Assert.AreEqual("adg\nbeh\ncfi", output);
        }

        [Test]
        public void Symmetry_4x4()
        {
            var input = "abcd\nefgh\nijkl\nmnop";
            var service = new SquaredStringService(input);

            var output = service.Symmetry();

            Assert.AreEqual("aeim\nbfjn\ncgko\ndhlp", output);
        }

        [Test]
        public void Rot_90_Clock_4x4()
        {
            var input = "abcd\nefgh\nijkl\nmnop";
            var service = new SquaredStringService(input);

            var output = service.Rot90Clock();

            Assert.AreEqual("miea\nnjfb\nokgc\nplhd", output);
        }

        [Test]
        public void Selfie_And_Symmetry_2x2()
        {
            var input = "ab\ncd";
            var service = new SquaredStringService(input);

            var output = service.SelfieAndSymmetry();

            Assert.AreEqual("ab|ac\ncd|bd", output);
        }
    }
}
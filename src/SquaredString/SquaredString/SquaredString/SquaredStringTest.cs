using System;
using NUnit.Framework;

namespace SquaredString
{
    [TestFixture]
    public class SquaredStringTest
    {
        [Test]
        public void Symmetry_2x2()
        {
            var input = "ab\ncd";
            var squaredFactory = new SquaredFactory(input);

            var output = squaredFactory.Symmetry();

            Assert.AreEqual("ac\nbd", output);
        }

        [Test]
        public void Symmetry_3x3()
        {
            var input = "abc\ndef\nghi";
            var squaredFactory = new SquaredFactory(input);

            var output = squaredFactory.Symmetry();

            Assert.AreEqual("adg\nbeh\ncfi", output);
        }

        [Test]
        public void Symmetry_4x4()
        {
            var input = "abcd\nefgh\nijkl\nmnop";
            var squaredFactory = new SquaredFactory(input);

            var output = squaredFactory.Symmetry();

            Assert.AreEqual("aeim\nbfjn\ncgko\ndhlp", output);
        }

        [Test]
        public void Rot_90_Clock_2x2()
        {
            var input = "ab\ncd";
            var squaredFactory = new SquaredFactory(input);

            var output = squaredFactory.Rot_90_Clock();

            Assert.AreEqual("ca\ndb", output);
        }

        [Test]
        public void Rot_90_Clock_3x3()
        {
            var input = "abc\ndef\nghi";
            var squaredFactory = new SquaredFactory(input);

            var output = squaredFactory.Rot_90_Clock();

            Assert.AreEqual("gda\nheb\nifc", output);
        }

        [Test]
        public void Rot_90_Clock_4x4()
        {
            var input = "abcd\nefgh\nijkl\nmnop";
            var squaredFactory = new SquaredFactory(input);

            var output = squaredFactory.Rot_90_Clock();

            Assert.AreEqual("miea\nnjfb\nokgc\nplhd", output);
        }

        [Test]
        public void Selfie_And_Symmetry_2x2()
        {
            var input = "ab\ncd";
            var squaredFactory = new SquaredFactory(input);

            var output = squaredFactory.Selfie_And_Symmetry();

            Assert.AreEqual("ab|ac\ncd|bd", output);
        }
    }
}
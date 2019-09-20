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
    }
}
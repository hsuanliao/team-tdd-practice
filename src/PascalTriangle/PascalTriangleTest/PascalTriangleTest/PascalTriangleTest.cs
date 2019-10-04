using NUnit.Framework;

namespace PascalTriangleTest
{
    [TestFixture]
    public class PascalTriangleTest
    {
        private PascalTriangle _pascalTriangle;

        [SetUp]
        public void Setup()
        {
            _pascalTriangle = new PascalTriangle();
        }

        [Test]
        public void LayerCount_1()
        {
            var result = _pascalTriangle.Transform(1);
            Assert.AreEqual("1", result);
        }

        [Test]
        public void LayerCount_2()
        {
            var result = _pascalTriangle.Transform(2);
            Assert.AreEqual("1,1-1", result);
        }

        [Test]
        public void LayerCount_3()
        {
            var result = _pascalTriangle.Transform(3);
            Assert.AreEqual("1,1-1,1-2-1", result);
        }

        [Test]
        public void LayerCount_8()
        {
            var result = _pascalTriangle.Transform(8);
            Assert.AreEqual("1,1-1,1-2-1,1-3-3-1,1-4-6-4-1,1-5-10-10-5-1,1-6-15-20-15-6-1,1-7-21-35-35-21-7-1", result);
        }
    }
}
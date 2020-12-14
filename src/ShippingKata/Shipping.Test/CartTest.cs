using System;
using FluentAssertions;
using NUnit.Framework;
using Shipping.Lib;

namespace Shipping.Test
{
    [TestFixture]
    public class CartTest
    {
        private const string BlackCat = "black cat";
        private const string HsinChu = "hsinchu";
        private const string PostOffice = "post office";
        private Cart _cart;

        [SetUp]
        public void _00_SetUp()
        {
            _cart = new Cart();
        }

        [Test]
        public void A01_ShippingFee_BlackCatWithLightWeight()
        {
            ShippingFeeShouldBe(BlackCat, 30, 20, 10, 5, 150);
        }

        [Test]
        public void A02_ShippingFee_BlackCatWithHeavyWeight()
        {
            ShippingFeeShouldBe(BlackCat, 30, 20, 10, 50, 500);
        }

        [Test]
        public void A03_ShippingFee_HsinChuWithSmallSize()
        {
            ShippingFeeShouldBe(HsinChu, 30, 20, 10, 50, 144);
        }

        [Test]
        public void A04_ShippingFee_HsinChuWithHugeSize()
        {
            ShippingFeeShouldBe(HsinChu, 100, 20, 10, 50, 480);
        }

        [Test]
        public void A05_ShippingFee_PostOfficeByWeight()
        {
            ShippingFeeShouldBe(PostOffice, 100, 20, 10, 3, 110);
        }

        [Test]
        public void A06_ShippingFee_PostOfficeBySize()
        {
            ShippingFeeShouldBe(PostOffice, 100, 20, 10, 300, 440);
        }

        [Test]
        public void A07_ShippingFee_ShipperNotExists()
        {
            Action action = () => _cart.ShippingFee("test", 10, 10, 10, 10);

            action.Should().Throw<ArgumentException>()
                .WithMessage("shipper not exist");
        }

        private void ShippingFeeShouldBe(string shipper, double length, double width, double height, int weight,
            double expected)
        {
            var actual = _cart.ShippingFee(shipper, length, width, height, weight);
            Assert.AreEqual(expected, actual);
        }
    }
}
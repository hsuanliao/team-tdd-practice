using System;
using NUnit.Framework;

namespace BudgetTest
{
    [TestFixture]
    public class BudgetServiceTest
    {
        private BudgetService _budgetService;

        [Test]
        public void A01_IllegalDateRange()
        {
            AmountShouldBe(new DateTime(2020, 1, 2), new DateTime(2020, 1, 1), 0m);
        }

        [SetUp]
        public void SetUp()
        {
            _budgetService = new BudgetService();
        }

        private void AmountShouldBe(DateTime startDateTime, DateTime endDateTime, decimal expected)
        {
            var actual = _budgetService.Query(
                startDateTime,
                endDateTime);
            Assert.AreEqual(expected, actual);
        }
    }
}
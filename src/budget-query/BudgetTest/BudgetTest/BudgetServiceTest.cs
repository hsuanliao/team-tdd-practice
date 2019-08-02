using System;
using NUnit.Framework;

namespace BudgetTest
{
    [TestFixture]
    public class BudgetServiceTest
    {
        private BudgetService _budgetService;

        [Test]
        public void QueryDateRangeInvalidate()
        {
            BudgetShouldBe(new DateTime(2019, 1, 5), new DateTime(2019, 1, 1), 0m);
        }

        [SetUp]
        public void Setup()
        {
            _budgetService = new BudgetService();
        }

        private void BudgetShouldBe(DateTime beginDate, DateTime endDate, decimal expected)
        {
            var actual = _budgetService.Query(beginDate, endDate);
            Assert.AreEqual(expected, actual);
        }
    }
}
using System;
using NUnit.Framework;

namespace BudgetTest
{
    [TestFixture]
    public class BudgetServiceTest
    {
        [Test]
        public void QueryDateRangeInvalidate()
        {
            var budgetService = new BudgetService();
            var actual = budgetService.Query(new DateTime(2019, 1, 5), new DateTime(2019, 1, 1));
            Assert.AreEqual(0, actual);
        }
    }
}
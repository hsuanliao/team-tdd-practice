using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BudgetTest
{
    [TestFixture]
    public class BudgetServiceTest
    {
        private IBudgetRepository _budgetRepository;
        private BudgetService _budgetService;

        [Test]
        public void Query_FullMonthRepoHasNoBudget_Return0()
        {
            _budgetRepository.GetAll().Returns(new List<Budget>());

            BudgetShouldBe(new DateTime(2019, 1, 1), new DateTime(2019, 1, 31), 0m);
        }

        [Test]
        public void QueryDateRangeInvalidate()
        {
            BudgetShouldBe(new DateTime(2019, 1, 5), new DateTime(2019, 1, 1), 0m);
        }

        [SetUp]
        public void Setup()
        {
            _budgetRepository = NSubstitute.Substitute.For<IBudgetRepository>();
            _budgetService = new BudgetService(_budgetRepository);
        }

        private void BudgetShouldBe(DateTime beginDate, DateTime endDate, decimal expected)
        {
            var actual = _budgetService.Query(beginDate, endDate);
            Assert.AreEqual(expected, actual);
        }
    }
}
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
        public void A01_IllegalDateRange()
        {
            AmountShouldBe(new DateTime(2020, 1, 2), new DateTime(2020, 1, 1), 0m);
        }

        [Test]
        public void A02_NoRepositoryData()
        {
            _budgetRepository.GetAll().ReturnsForAnyArgs(info => new List<Budget>());
            AmountShouldBe(new DateTime(2020, 1, 1), new DateTime(2020, 1, 2), 0m);
        }

        [Test]
        public void A03_QueryOneFullMonth()
        {
            _budgetRepository.GetAll().ReturnsForAnyArgs(info => new List<Budget>()
            {
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2020, 1, 1), new DateTime(2020, 1, 31), 310);
        }

        [SetUp]
        public void SetUp()
        {
            _budgetRepository = Substitute.For<IBudgetRepository>();
            _budgetService = new BudgetService(_budgetRepository);
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
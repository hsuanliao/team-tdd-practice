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
            RepositoryReturnsFullData(new List<Budget>());
            AmountShouldBe(new DateTime(2020, 1, 1), new DateTime(2020, 1, 2), 0m);
        }

        [Test]
        public void A03_QueryOneFullMonth()
        {
            RepositoryReturnsFullData(new List<Budget>()
            {
                new Budget() {YearMonth = "201912", Amount = 31m},
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2020, 1, 1), new DateTime(2020, 1, 31), 310);
        }

        [Test]
        public void A04_QueryMultipleFullMonths()
        {
            RepositoryReturnsFullData(new List<Budget>()
            {
                new Budget() {YearMonth = "201912", Amount = 31m},
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2020, 1, 1), new DateTime(2020, 2, 29), 3210);
        }

        [Test]
        public void A05_QueryMultipleCrossYearFullMonths()
        {
            RepositoryReturnsFullData(new List<Budget>()
            {
                new Budget() {YearMonth = "201912", Amount = 31m},
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2019, 12, 1), new DateTime(2020, 2, 29), 3241);
        }

        [Test]
        public void A06_QueryPartialMonthDateRangeFrom20200125To20200131()
        {
            RepositoryReturnsFullData(new List<Budget>()
            {
                new Budget() {YearMonth = "201912", Amount = 31m},
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2020, 1, 25), new DateTime(2020, 1, 31), 70);
        }

        [Test]
        public void A07_QueryPartialMonthDateRangeFrom20190125To20200131()
        {
            RepositoryReturnsFullData(new List<Budget>()
            {
                new Budget() {YearMonth = "201912", Amount = 31m},
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2019, 1, 25), new DateTime(2020, 1, 31), 341);
        }

        [Test]
        public void A08_QueryPartialMonthDateRangeFrom20200101To20200125()
        {
            RepositoryReturnsFullData(new List<Budget>()
            {
                new Budget() {YearMonth = "201912", Amount = 31m},
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2020, 1, 1), new DateTime(2020, 1, 25), 250);
        }

        [Test]
        public void A09_QueryPartialMonthDateRangeFrom20200101To20200325()
        {
            RepositoryReturnsFullData(new List<Budget>()
            {
                new Budget() {YearMonth = "201912", Amount = 31m},
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2020, 1, 1), new DateTime(2020, 3, 25), 3210);
        }

        [Test]
        public void A10_QueryPartialMonthDateRangeFrom20200125To20200225()
        {
            RepositoryReturnsFullData(new List<Budget>()
            {
                new Budget() {YearMonth = "201912", Amount = 31m},
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2020, 1, 25), new DateTime(2020, 2, 25), 2570);
        }

        [Test]
        public void A11_QueryPartialMonthDateRangeFrom20190125To20200325()
        {
            RepositoryReturnsFullData(new List<Budget>()
            {
                new Budget() {YearMonth = "201912", Amount = 31m},
                new Budget() {YearMonth = "202001", Amount = 310m},
                new Budget() {YearMonth = "202002", Amount = 2900m}
            });
            AmountShouldBe(new DateTime(2019, 1, 25), new DateTime(2020, 3, 25), 3241);
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

        private void RepositoryReturnsFullData(List<Budget> budgets)
        {
            _budgetRepository.GetAll().ReturnsForAnyArgs(info => budgets);
        }
    }
}
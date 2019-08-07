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
        public void Query_CrossMultipleMonthRepoHasBudget_ReturnAmount()
        {
            _budgetRepository.GetAll().Returns(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201901",
                    Amount = 310m
                },
                new Budget
                {
                    YearMonth = "201902",
                    Amount = 28m
                },
                new Budget
                {
                    YearMonth = "201903",
                    Amount = 3100m
                }
            });

            BudgetShouldBe(new DateTime(2019, 1, 25), new DateTime(2019, 3, 5), 598m);
        }

        [Test]
        public void Query_CrossTwoMonthRepoHasBudget_ReturnAmount()
        {
            _budgetRepository.GetAll().Returns(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201901",
                    Amount = 310m
                },
                new Budget
                {
                    YearMonth = "201902",
                    Amount = 28m
                }
            });

            BudgetShouldBe(new DateTime(2019, 1, 25), new DateTime(2019, 2, 5), 75m);
        }

        [Test]
        public void Query_CrossYearAndMultipleMonthRepoHasBudget_ReturnAmount()
        {
            _budgetRepository.GetAll().Returns(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201811",
                    Amount = 300m
                },
                new Budget
                {
                    YearMonth = "201902",
                    Amount = 28m
                },
                new Budget
                {
                    YearMonth = "201903",
                    Amount = 3100m
                }
            });

            BudgetShouldBe(new DateTime(2018, 11, 30), new DateTime(2019, 3, 1), 138m);
        }

        [Test]
        public void Query_DateRangeInvalid_Return0()
        {
            BudgetShouldBe(new DateTime(2019, 1, 5), new DateTime(2019, 1, 1), 0m);
        }

        [Test]
        public void Query_FullMonthRepoHasBudget_ReturnAmount()
        {
            _budgetRepository.GetAll().Returns(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201901",
                    Amount = 310m
                }
            });

            BudgetShouldBe(new DateTime(2019, 1, 1), new DateTime(2019, 1, 31), 310m);
        }

        [Test]
        public void Query_FullMonthRepoHasNoBudget_Return0()
        {
            _budgetRepository.GetAll().Returns(new List<Budget>());

            BudgetShouldBe(new DateTime(2019, 1, 1), new DateTime(2019, 1, 31), 0m);
        }

        [Test]
        public void Query_PartialMonthRepoHasBudget_ReturnAmount()
        {
            _budgetRepository.GetAll().Returns(new List<Budget>
            {
                new Budget
                {
                    YearMonth = "201901",
                    Amount = 310m
                }
            });

            BudgetShouldBe(new DateTime(2019, 1, 1), new DateTime(2019, 1, 15), 150m);
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
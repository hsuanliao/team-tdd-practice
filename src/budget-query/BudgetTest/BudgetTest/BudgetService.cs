﻿using System;
using System.Linq;

namespace BudgetTest
{
    public class BudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public decimal Query(DateTime beginDate, DateTime endDate)
        {
            if (beginDate > endDate)
            {
                return 0;
            }

            var budgets = _budgetRepository.GetAll();
            if (IsSameMonth(beginDate, endDate))
            {
                var budget = budgets.FirstOrDefault(d => d.YearMonth.Equals(beginDate.ToString("yyyyMM")));
                if (budget == null)
                {
                    return 0;
                }
                return budget.DailyAmount() * Period.DayCount(beginDate, endDate);
            }

            var totalBudget = 0m;
            var yearInterval = endDate.Year - beginDate.Year;
            var monthInterval = endDate.Month - beginDate.Month;
            var midMonthInterval = yearInterval * 12 + monthInterval;
            for (var i = 0; i <= midMonthInterval; i++)
            {
                var currentDate = beginDate.AddMonths(i);

                var currentBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(currentDate.ToString("yyyyMM")));
                if (currentBudget == null)
                {
                    continue;
                }

                var effectiveDays = new Period(beginDate, endDate).OverlappingDays(new Period(currentBudget.FirstDay(), currentBudget.LastDay()));
                totalBudget += currentBudget.DailyAmount() * effectiveDays;
            }

            return totalBudget;
        }

        private static bool IsSameMonth(DateTime beginDate, DateTime endDate)
        {
            return beginDate.ToString("yyyyMM").Equals(endDate.ToString("yyyyMM"));
        }
    }
}
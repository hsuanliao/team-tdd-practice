using System;
using System.Collections.Generic;
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
                var intervalDays = endDate.Day - beginDate.Day + 1;
                return CalculateBudgetAmount(beginDate, budgets, intervalDays);
            }

            var totalBudget = 0m;
            var firstMonthBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(beginDate.ToString("yyyyMM")));
            if (firstMonthBudget != null)
            {
                totalBudget += firstMonthBudget.DailyAmount() * EffectiveDays(beginDate, firstMonthBudget.LastDay());
            }

            var lastIntervalDays = endDate.Day;
            totalBudget += CalculateBudgetAmount(endDate, budgets, lastIntervalDays);

            var yearInterval = endDate.Year - beginDate.Year;
            var monthInterval = endDate.Month - beginDate.Month;
            var midMonthInterval = yearInterval * 12 + monthInterval - 1;
            for (var i = 1; i <= midMonthInterval; i++)
            {
                var addMonths = beginDate.AddMonths(i);
                totalBudget += CalculateBudgetAmount(addMonths, budgets, DateTime.DaysInMonth(addMonths.Year, addMonths.Month));
            }

            return totalBudget;
        }

        private static decimal CalculateBudgetAmount(DateTime queryDate, IList<Budget> budgets, int intervalDays)
        {
            var budget = budgets.FirstOrDefault(d => d.YearMonth.Equals(queryDate.ToString("yyyyMM")));
            if (budget == null)
            {
                return 0;
            }

            return budget.DailyAmount() * intervalDays;
        }

        private static int EffectiveDays(DateTime beginDate, DateTime endDate)
        {
            return (endDate - beginDate).Days + 1;
        }

        private static bool IsSameMonth(DateTime beginDate, DateTime endDate)
        {
            return beginDate.ToString("yyyyMM").Equals(endDate.ToString("yyyyMM"));
        }
    }
}
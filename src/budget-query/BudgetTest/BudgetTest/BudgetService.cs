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
            if (beginDate.ToString("yyyyMM").Equals(endDate.ToString("yyyyMM")))
            {
                var intervalDays = endDate.Day - beginDate.Day + 1;
                return CalculateBudgetAmount(beginDate, budgets, intervalDays);
            }

            var totalBudget = 0m;
            var firstBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(beginDate.ToString("yyyyMM")));
            if (firstBudget != null)
            {
                totalBudget += firstBudget.DailyAmount() * EffectiveDays(beginDate, firstBudget.LastDay());
            }

            var lastBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(endDate.ToString("yyyyMM")));
            if (lastBudget != null)
            {
                totalBudget += lastBudget.DailyAmount() * EffectiveDays(lastBudget.FirstDay(), endDate);
            }

            var yearInterval = endDate.Year - beginDate.Year;
            var monthInterval = endDate.Month - beginDate.Month;
            var midMonthInterval = yearInterval * 12 + monthInterval - 1;
            for (var i = 1; i <= midMonthInterval; i++)
            {
                var currentDate = beginDate.AddMonths(i);
                var currentBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(currentDate.ToString("yyyyMM")));
                if (currentBudget != null)
                {
                    totalBudget += currentBudget.DailyAmount() * EffectiveDays(currentBudget.FirstDay(), currentBudget.LastDay());
                }
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

        private static int EffectiveDays(DateTime beginDate, DateTime lastDay)
        {
            return (lastDay - beginDate).Days + 1;
        }
    }
}
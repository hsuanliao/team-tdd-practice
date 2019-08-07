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
                totalBudget += EffectiveDays(beginDate, firstMonthBudget.LastDay()) * firstMonthBudget.DailyAmount();
            }

            var lastMonthBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(endDate.ToString("yyyyMM")));
            if (lastMonthBudget != null)
            {
                totalBudget += EffectiveDays(lastMonthBudget.FirstDay(), endDate) * lastMonthBudget.DailyAmount();
            }

            var yearInterval = endDate.Year - beginDate.Year;
            var monthInterval = endDate.Month - beginDate.Month;
            var midMonthInterval = yearInterval * 12 + monthInterval - 1;
            for (var i = 1; i <= midMonthInterval; i++)
            {
                var addMonths = beginDate.AddMonths(i);
                var currentBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(addMonths.ToString("yyyyMM")));
                if (currentBudget != null)
                {
                    totalBudget += EffectiveDays(currentBudget.FirstDay(), currentBudget.LastDay()) * currentBudget.DailyAmount();
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

            return intervalDays * budget.DailyAmount();
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
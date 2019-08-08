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

                return DayCount(beginDate, endDate) * budget.DailyAmount();
            }

            var totalBudget = 0m;

            var yearInterval = endDate.Year - beginDate.Year;
            var monthInterval = endDate.Month - beginDate.Month;
            var midMonthInterval = yearInterval * 12 + monthInterval + 1;
            for (var i = 0; i <= midMonthInterval; i++)
            {
                var currentDate = beginDate.AddMonths(i);
                var currentBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(currentDate.ToString("yyyyMM")));
                if (currentBudget == null)
                {
                    continue;
                }

                var effectiveDays = OverlappingDayCount(new Period(beginDate, endDate), currentBudget);
                totalBudget += effectiveDays * currentBudget.DailyAmount();
            }

            return totalBudget;
        }

        private static int DayCount(DateTime beginDate, DateTime endDate)
        {
            return (endDate - beginDate).Days + 1;
        }

        private static bool IsSameMonth(DateTime beginDate, DateTime endDate)
        {
            return beginDate.ToString("yyyyMM").Equals(endDate.ToString("yyyyMM"));
        }

        private static int OverlappingDayCount(Period period, Budget currentBudget)
        {
            DateTime effectiveBegin;
            DateTime effectiveEnd;
            if (period.BeginDate.ToString("yyyyMM").Equals(currentBudget.YearMonth))
            {
                effectiveBegin = period.BeginDate;
                effectiveEnd = currentBudget.LastDay();
            }
            else if (period.EndDate.ToString("yyyyMM").Equals(currentBudget.YearMonth))
            {
                effectiveBegin = currentBudget.FirstDay();
                effectiveEnd = period.EndDate;
            }
            else
            {
                effectiveBegin = currentBudget.FirstDay();
                effectiveEnd = currentBudget.LastDay();
            }

            var effectiveDays = DayCount(effectiveBegin, effectiveEnd);
            return effectiveDays;
        }
    }
}
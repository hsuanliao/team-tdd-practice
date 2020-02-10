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

        public decimal Query(DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime > endDateTime)
            {
                return 0;
            }

            var budgets = _budgetRepository.GetAll();
            if (budgets.Count == 0)
            {
                return 0;
            }

            var startRange = int.Parse(startDateTime.ToString("yyyyMM"));
            var endRange = int.Parse(endDateTime.ToString("yyyyMM"));

            var totalBudget = budgets
                .Where(o =>
                    int.Parse(o.YearMonth) >= startRange
                    && int.Parse(o.YearMonth) <= endRange)
                .Sum(o => o.Amount);

            var excludedStartBudget = GetExcludedStartBudget(startDateTime, budgets, startRange);

            return totalBudget - excludedStartBudget;
            throw new NotImplementedException();
        }

        private static decimal GetExcludedStartBudget(DateTime startDateTime, IList<Budget> budgets, int startRange)
        {
            var startMonthBudget = budgets.First(b => int.Parse(b.YearMonth) == startRange).Amount;
            var daysInStartMonth = DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month);
            var excludedStartDays = startDateTime.Day - 1;
            var excludedStartBudget = startMonthBudget / daysInStartMonth * excludedStartDays;
            return excludedStartBudget;
        }
    }
}
using System;
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
            var budgets = _budgetRepository.GetAll();

            var startRange = int.Parse(startDateTime.ToString("yyyyMM"));
            var endRange = int.Parse(endDateTime.ToString("yyyyMM"));
            var inRangeBudgets = budgets
                .Where(o =>
                    int.Parse(o.YearMonth) >= startRange
                    && int.Parse(o.YearMonth) <= endRange)
                .ToList();

            if (!inRangeBudgets.Any())
            {
                return 0;
            }

            var inRangeTotalBudgetAmount = inRangeBudgets
                .Sum(o => o.Amount);

            var excludedBudgetAmount = GetExcludedBudgetAmount(
                inRangeBudgets.First(), startDateTime, endDateTime);

            if (inRangeBudgets.Count > 1)
            {
                excludedBudgetAmount += GetExcludedBudgetAmount(
                    inRangeBudgets.Last(), startDateTime, endDateTime);
            }

            return inRangeTotalBudgetAmount - excludedBudgetAmount;
        }

        private static decimal GetExcludedBudgetAmount(Budget excludedStartBudget, DateTime startDateTime,
            DateTime endDateTime)
        {
            var excludedStartDays = GetExcludedDays(excludedStartBudget, startDateTime, endDateTime);
            var excludedStartBudgetAmount = excludedStartBudget.Amount / excludedStartBudget.GetDaysInMonth() *
                excludedStartDays;
            return excludedStartBudgetAmount;
        }

        private static int GetExcludedDays(Budget budget, DateTime startDateTime, DateTime endDateTime)
        {
            var excludedDays = 0;
            //if (budget.GetFirstDate() > startDateTime && startDateTime < budget.GetEndDate())
            if (budget.YearMonth.Equals(startDateTime.ToString("yyyyMM")))
            {
                excludedDays = (startDateTime - budget.GetFirstDate()).Days;
            }

            if (budget.YearMonth.Equals(endDateTime.ToString("yyyyMM")))
            {
                excludedDays += (budget.GetLastDate() - endDateTime).Days;
            }

            return excludedDays;
        }
    }
}
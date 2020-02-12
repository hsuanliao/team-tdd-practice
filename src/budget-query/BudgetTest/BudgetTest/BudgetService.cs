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

            var excludedStartBudget = budgets
                .FirstOrDefault(b => b.YearMonth.Equals(startDateTime.ToString("yyyyMM")));
            var excludedStartDays = startDateTime.Day - 1;
            var excludedStartBudgetAmount = GetExcludedTargetMonthBudget(excludedStartBudget, excludedStartDays);

            decimal excludedEndBudget;
            var targetEndBudget = budgets.FirstOrDefault(b => int.Parse(b.YearMonth) == endRange);
            if (targetEndBudget == null)
            {
                excludedEndBudget = 0;
            }
            else
            {
                var endMonthBudget = targetEndBudget.Amount;
                var daysInEndMonth = DateTime.DaysInMonth(endDateTime.Year, endDateTime.Month);
                var excludedEndDays = daysInEndMonth - endDateTime.Day;
                excludedEndBudget = endMonthBudget / daysInEndMonth * excludedEndDays;
            }

            return totalBudget - excludedStartBudgetAmount - excludedEndBudget;
        }

        private static decimal GetExcludedTargetMonthBudget(Budget excludedBudget, int excludedDays)
        {
            //decimal excludedBudgetAmount;

            //if (excludedBudget == null)
            //{
            //    excludedBudgetAmount = 0;
            //}
            //else
            //{
            //    var daysInMonth = excludedBudget.GetDaysInMonth();
            //    //var daysInMonth2 = excludedBudget.DaysInMonth();
            //    excludedBudgetAmount = excludedBudget.Amount / daysInMonth * excludedDays;
            //}

            //return excludedBudgetAmount;
            return excludedBudget == null
                ? 0
                : excludedBudget.Amount / excludedBudget.GetDaysInMonth() * excludedDays;
        }
    }
}
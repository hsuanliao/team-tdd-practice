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

                return EffectiveDays(beginDate, endDate) * budget.DailyAmount();
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

                if (IsSameMonth(beginDate, currentDate))
                {
                    var effectiveDays = EffectiveDays(beginDate, currentBudget.LastDay());
                    totalBudget += effectiveDays * currentBudget.DailyAmount();
                }
                else if (IsSameMonth(endDate, currentDate))
                {
                    var effectiveDays = EffectiveDays(currentBudget.FirstDay(), endDate);
                    totalBudget += effectiveDays * currentBudget.DailyAmount();
                }
                else
                {
                    var effectiveDays = EffectiveDays(currentBudget.FirstDay(), currentBudget.LastDay());
                    totalBudget += effectiveDays * currentBudget.DailyAmount();
                }
            }

            return totalBudget;
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
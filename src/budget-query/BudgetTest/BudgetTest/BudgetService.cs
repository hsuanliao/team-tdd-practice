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
            //var firstMonthBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(beginDate.ToString("yyyyMM")));
            //if (firstMonthBudget != null)
            //{
            //    totalBudget += EffectiveDays(beginDate, firstMonthBudget.LastDay()) * firstMonthBudget.DailyAmount();
            //}

            //var lastMonthBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(endDate.ToString("yyyyMM")));
            //if (lastMonthBudget != null)
            //{
            //    totalBudget += EffectiveDays(lastMonthBudget.FirstDay(), endDate) * lastMonthBudget.DailyAmount();
            //}

            var yearInterval = endDate.Year - beginDate.Year;
            var monthInterval = endDate.Month - beginDate.Month;
            var midMonthInterval = yearInterval * 12 + monthInterval + 1;
            for (var i = 0; i <= midMonthInterval; i++)
            {
                var currentMonth = beginDate.AddMonths(i);
                var currentBudget = budgets.FirstOrDefault(d => d.YearMonth.Equals(currentMonth.ToString("yyyyMM")));
                if (currentBudget == null)
                {
                    continue;
                }

                if (IsSameMonth(beginDate, currentMonth))
                {
                    totalBudget += EffectiveDays(beginDate, currentBudget.LastDay()) * currentBudget.DailyAmount();
                }
                else if (IsSameMonth(endDate, currentMonth))
                {
                    totalBudget += EffectiveDays(currentBudget.FirstDay(), endDate) * currentBudget.DailyAmount();
                }
                else
                {
                    totalBudget += EffectiveDays(currentBudget.FirstDay(), currentBudget.LastDay()) *
                                   currentBudget.DailyAmount();
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
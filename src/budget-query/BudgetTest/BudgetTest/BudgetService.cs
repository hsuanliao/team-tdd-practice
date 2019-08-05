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

            if (beginDate.ToString("yyyyMM").Equals(endDate.ToString("yyyyMM")))
            {
                var budgets = _budgetRepository.GetAll();
                var budget = budgets.FirstOrDefault(d => d.YearMonth.Equals(beginDate.ToString("yyyyMM")));
                if (budget == null)
                {
                    return 0;
                }

                var daysInMonth = DateTime.DaysInMonth(beginDate.Year, beginDate.Month);
                var intervalDays = endDate.Day - beginDate.Day + 1;
                if (intervalDays == daysInMonth)
                {
                    return budget.Amount;
                }

                return intervalDays * budget.Amount / daysInMonth;
            }

            throw new NotImplementedException();
        }
    }
}
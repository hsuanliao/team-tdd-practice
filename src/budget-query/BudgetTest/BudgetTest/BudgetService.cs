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

                if (budgets.Count == 0)
                {
                    return 0;
                }

                if (endDate.Day - beginDate.Day + 1 == DateTime.DaysInMonth(beginDate.Year, beginDate.Month))
                {
                    return budgets.FirstOrDefault(d => d.YearMonth.Equals(beginDate.ToString("yyyyMM")))?.Amount ?? 0;
                }
            }

            throw new NotImplementedException();
        }
    }
}
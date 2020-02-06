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

            return budgets
                .Where(o =>
                    o.YearMonth == startDateTime.ToString("yyyyMM") &&
                    o.YearMonth == endDateTime.ToString("yyyyMM"))
                .Sum(o => o.Amount);
            throw new NotImplementedException();
        }
    }
}
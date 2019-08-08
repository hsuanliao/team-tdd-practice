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
            var period = new Period(beginDate, endDate);
            if (period.BeginDate > period.EndDate)
            {
                return 0;
            }

            var budgets = _budgetRepository.GetAll();
            return budgets.Sum(budget => budget.OverlappingAmount(period));
        }
    }
}
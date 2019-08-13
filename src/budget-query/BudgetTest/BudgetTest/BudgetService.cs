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
            return period.IsInvalid()
                ? 0
                : _budgetRepository.GetAll().Sum(currentBudget => currentBudget.OverlappingAmount(period));
        }
    }
}
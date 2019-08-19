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
            if (period.IsValidDateRange())
            {
                return 0;
            }

            return _budgetRepository.GetAll().Sum(budget => budget.OverlappingAmount(period));
        }
    }
}
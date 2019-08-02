using System;

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
            }

            throw new NotImplementedException();
        }
    }
}
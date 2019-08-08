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

            var budgets = _budgetRepository.GetAll();

            var totalBudget = 0m;

            var period = new Period(beginDate, endDate);
            foreach (var budget in budgets)
            {
                totalBudget += OverlappingAmount(budget, period);
            }

            return totalBudget;
        }

        private static decimal OverlappingAmount(Budget budget, Period period)
        {
            return budget.DailyAmount() * period.OverlappingDayCount(budget.GetPeriod());
        }
    }
}
using System;

namespace BudgetTest
{
    public class Period
    {
        public Period(DateTime beginDate, DateTime endDate)
        {
            BeginDate = beginDate;
            EndDate = endDate;
        }

        public DateTime BeginDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public static int DayCount(DateTime beginDate, DateTime endDate)
        {
            return (endDate - beginDate).Days + 1;
        }

        public int OverlappingDays(Budget currentBudget)
        {
            var effectiveBeginDate = BeginDate > currentBudget.FirstDay()
                ? BeginDate
                : currentBudget.FirstDay();
            var effectiveEndDate = EndDate < currentBudget.LastDay()
                ? EndDate
                : currentBudget.LastDay();

            return DayCount(effectiveBeginDate, effectiveEndDate);
        }
    }
}
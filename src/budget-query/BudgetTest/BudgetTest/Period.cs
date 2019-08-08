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

        public int OverlappingDayCount(Budget budget)
        {
            if (EndDate < budget.FirstDay())
            {
                return 0;
            }

            var overlappingBeginDate = BeginDate > budget.FirstDay()
                ? BeginDate
                : budget.FirstDay();

            var overlappingEndDate = EndDate < budget.LastDay()
                ? EndDate
                : budget.LastDay();

            return DayCount(overlappingBeginDate, overlappingEndDate);
        }
    }
}
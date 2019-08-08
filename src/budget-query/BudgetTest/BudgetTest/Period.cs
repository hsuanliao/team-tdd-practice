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
            var firstDay = budget.FirstDay();
            var lastDay = budget.LastDay();
            if (EndDate < firstDay || BeginDate > lastDay)
            {
                return 0;
            }

            var overlappingBeginDate = BeginDate > firstDay
                ? BeginDate
                : firstDay;

            var overlappingEndDate = EndDate < lastDay
                ? EndDate
                : lastDay;

            return DayCount(overlappingBeginDate, overlappingEndDate);
        }
    }
}
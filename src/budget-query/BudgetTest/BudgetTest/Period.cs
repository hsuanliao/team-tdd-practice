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
            var period = new Period(budget.FirstDay(), budget.LastDay());
            if (EndDate < period.BeginDate || BeginDate > period.EndDate)
            {
                return 0;
            }

            var overlappingBeginDate = BeginDate > period.BeginDate
                ? BeginDate
                : period.BeginDate;

            var overlappingEndDate = EndDate < period.EndDate
                ? EndDate
                : period.EndDate;

            return DayCount(overlappingBeginDate, overlappingEndDate);
        }
    }
}
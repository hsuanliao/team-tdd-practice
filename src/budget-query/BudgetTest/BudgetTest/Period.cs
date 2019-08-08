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

        public int OverlappingDayCount(Period another)
        {
            if (EndDate < another.BeginDate || BeginDate > another.EndDate)
            {
                return 0;
            }

            var overlappingBeginDate = BeginDate > another.BeginDate
                ? BeginDate
                : another.BeginDate;

            var overlappingEndDate = EndDate < another.EndDate
                ? EndDate
                : another.EndDate;

            return DayCount(overlappingBeginDate, overlappingEndDate);
        }
    }
}
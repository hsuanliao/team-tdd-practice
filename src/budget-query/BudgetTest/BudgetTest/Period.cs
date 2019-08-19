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

        public bool IsValidDateRange()
        {
            return BeginDate > EndDate;
        }

        public int OverlappingDays(Period another)
        {
            if (EndDate < another.BeginDate || BeginDate > another.EndDate)
            {
                return 0;
            }

            var begin = BeginDate > another.BeginDate ? BeginDate : another.BeginDate;
            var end = EndDate < another.EndDate ? EndDate : another.EndDate;

            return DayCount(begin, end);
        }

        private static int DayCount(DateTime beginDate, DateTime lastDay)
        {
            return (lastDay - beginDate).Days + 1;
        }
    }
}
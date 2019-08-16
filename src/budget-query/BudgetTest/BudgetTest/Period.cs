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

        public static int DayCount(DateTime beginDate, DateTime lastDay)
        {
            return (lastDay - beginDate).Days + 1;
        }

        public int OverlappingDays(Period another)
        {
            var begin = BeginDate > another.BeginDate ? BeginDate : another.BeginDate;
            var end = EndDate < another.EndDate ? EndDate : another.EndDate;

            return DayCount(begin, end);
        }
    }
}
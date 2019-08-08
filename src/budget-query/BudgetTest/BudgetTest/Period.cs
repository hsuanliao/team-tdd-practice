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
            var effectiveBegin = BeginDate > budget.FirstDay()
                ? BeginDate
                : budget.FirstDay();
            var effectiveEnd = EndDate < budget.LastDay()
                ? EndDate
                : budget.LastDay();
            if (BeginDate.ToString("yyyyMM").Equals(budget.YearMonth))
            {
                //effectiveBegin = BeginDate;
                //effectiveEnd = budget.LastDay();
            }
            else if (EndDate.ToString("yyyyMM").Equals(budget.YearMonth))
            {
                //effectiveBegin = budget.FirstDay();
                //effectiveEnd = EndDate;
            }
            else
            {
                //effectiveBegin = budget.FirstDay();
                //effectiveEnd = budget.LastDay();
            }

            var effectiveDays = DayCount(effectiveBegin, effectiveEnd);
            return effectiveDays;
        }
    }
}
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

        public int OverlappingDayCount(Budget currentBudget)
        {
            DateTime effectiveBegin;
            DateTime effectiveEnd;
            if (BeginDate.ToString("yyyyMM").Equals(currentBudget.YearMonth))
            {
                effectiveBegin = BeginDate;
                effectiveEnd = currentBudget.LastDay();
            }
            else if (EndDate.ToString("yyyyMM").Equals(currentBudget.YearMonth))
            {
                effectiveBegin = currentBudget.FirstDay();
                effectiveEnd = EndDate;
            }
            else
            {
                effectiveBegin = currentBudget.FirstDay();
                effectiveEnd = currentBudget.LastDay();
            }

            var effectiveDays = DayCount(effectiveBegin, effectiveEnd);
            return effectiveDays;
        }
    }
}
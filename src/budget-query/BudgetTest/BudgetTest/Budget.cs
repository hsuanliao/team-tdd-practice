using System;

namespace BudgetTest
{
    public class Budget
    {
        public decimal Amount { get; set; }
        public string YearMonth { get; set; }

        public int GetDaysInMonth()
        {
            return DateTime.DaysInMonth(GetFirstDate().Year, GetFirstDate().Month);
        }

        public DateTime GetFirstDate()
        {
            return DateTime.ParseExact($"{YearMonth}01", "yyyyMMdd", null);
        }
    }
}
using System;

namespace BudgetTest
{
    public class Budget
    {
        public decimal Amount { get; set; }
        public string YearMonth { get; set; }

        public decimal DailyAmount()
        {
            return Amount / Days();
        }

        public int Days()
        {
            var firstDay = DateTime.ParseExact($"{YearMonth}01", "yyyyMMdd", null);
            return DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
        }
    }
}
﻿using System;

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
            return DateTime.DaysInMonth(FirstDay().Year, FirstDay().Month);
        }

        public DateTime FirstDay()
        {
            return DateTime.ParseExact($"{YearMonth}01", "yyyyMMdd", null);
        }

        public DateTime LastDay()
        {
            return DateTime.ParseExact($"{YearMonth}{Days()}", "yyyyMMdd", null);
        }
    }
}
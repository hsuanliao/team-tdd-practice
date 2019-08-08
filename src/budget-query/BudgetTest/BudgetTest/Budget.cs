﻿using System;

namespace BudgetTest
{
    public class Budget
    {
        public decimal Amount { get; set; }
        public string YearMonth { get; set; }

        public decimal OverlappingAmount(Period period)
        {
            return DailyAmount() * period.OverlappingDayCount(GetPeriod());
        }

        private decimal DailyAmount()
        {
            return Amount / Days();
        }

        private int Days()
        {
            return DateTime.DaysInMonth(FirstDay().Year, FirstDay().Month);
        }

        private DateTime FirstDay()
        {
            return DateTime.ParseExact($"{YearMonth}01", "yyyyMMdd", null);
        }

        private Period GetPeriod()
        {
            return new Period(FirstDay(), LastDay());
        }

        private DateTime LastDay()
        {
            return DateTime.ParseExact($"{YearMonth}{Days()}", "yyyyMMdd", null);
        }
    }
}
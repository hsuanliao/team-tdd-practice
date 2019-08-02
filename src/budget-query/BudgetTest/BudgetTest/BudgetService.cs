using System;

namespace BudgetTest
{
    public class BudgetService
    {
        public decimal Query(DateTime beginDate, DateTime endDate)
        {
            if (beginDate > endDate)
            {
                return 0;
            }

            throw new NotImplementedException();
        }
    }
}
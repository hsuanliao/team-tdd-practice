using System;
using System.Linq.Expressions;

namespace BudgetTest
{
    public class BudgetService
    {
        public decimal Query(DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime > endDateTime)
            {
                return 0;
            }

            throw new NotImplementedException();
        }
    }
}
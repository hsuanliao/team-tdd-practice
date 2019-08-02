using System.Collections.Generic;

namespace BudgetTest
{
    public interface IBudgetRepository
    {
        IList<Budget> GetAll();
    }
}
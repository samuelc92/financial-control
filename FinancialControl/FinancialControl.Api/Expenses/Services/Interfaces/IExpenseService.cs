using System.Collections.Generic;
using FinancialControl.Api.Expenses.Domain.Models;

namespace FinancialControl.Api.Expenses.Services.Interfaces
{
    public interface IExpenseService
    {
        void Insert(Expense expense);
        void Update(Expense expense);
        IEnumerable<Expense> Get(Dictionary<string, string> filters);
        void Pay(string id);
        void Delete(IEnumerable<string> ids);
    }
}
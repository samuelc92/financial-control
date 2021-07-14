using System.Collections.Generic;
using FinancialControl.Api.Expenses.Domain.Models;

namespace FinancialControl.Api.Expenses.Services.Interfaces
{
    public interface IScheduleExpenseService
    {
        void Insert(ScheduleExpense expense);
        IEnumerable<ScheduleExpense> GetAll();
        void Run();
    }
}
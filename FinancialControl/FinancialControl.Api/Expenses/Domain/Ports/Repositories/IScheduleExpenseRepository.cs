using System.Collections.Generic;
using FinancialControl.Api.Expenses.Domain.Models;

namespace FinancialControl.Api.Expenses.Domain.Ports.Repositories
{
    public interface IScheduleExpenseRepository
    {
        void Insert(ScheduleExpense scheduleExpense);
        IEnumerable<ScheduleExpense> GetAll(); 
        ICollection<ScheduleExpense> GetActives();
    }
}
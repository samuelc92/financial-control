using System;
using FinancialControl.Api.Domain.Expenses.Models;

namespace FinancialControl.Api.Domain.Expenses.ViewModels
{
    public class ScheduleExpenseViewModel
    {
        public string Id { get; set; }
        
        public decimal Amount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        public DateTime? DueDate { get; set; }

        public ScheduleExpense ConvertToScheduleExpense() =>
            new ScheduleExpense(Id, Amount, StartDate, EndDate, DueDate);
    }
}
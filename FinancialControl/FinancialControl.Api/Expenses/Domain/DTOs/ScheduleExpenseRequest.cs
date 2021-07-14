using System;
using FinancialControl.Api.Expenses.Domain.Models;

namespace FinancialControl.Api.Expenses.Domain.DTOs
{
    public class ScheduleExpenseRequest
    {
        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        public int? DueDay { get; set; }

        public ScheduleExpense ConvertToScheduleExpense() =>
            new ScheduleExpense(Amount, Description, StartDate, EndDate, DueDay);
     }
}
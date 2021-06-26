using System;

namespace FinancialControl.Api.Expenses.Domain.DTOs
{
    public class ScheduleExpenseResponse
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
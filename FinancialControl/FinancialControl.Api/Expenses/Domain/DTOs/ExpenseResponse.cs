using System;
using FinancialControl.Api.Expenses.Domain.Models.Enums;

namespace FinancialControl.Api.Expenses.Domain.DTOs
{
    public class ExpenseResponse
    {
        public string Id { get; set; }
        
        public Category Category { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime? DueDate { get; set; }
        
        public ExpenseStatus Status { get; set; }
        
    }
}
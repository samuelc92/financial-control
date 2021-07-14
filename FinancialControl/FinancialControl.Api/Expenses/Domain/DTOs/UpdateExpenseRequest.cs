using System;
using FinancialControl.Api.Expenses.Domain.Models;
using FinancialControl.Api.Expenses.Domain.Models.Enums;

namespace FinancialControl.Api.Expenses.Domain.DTOs
{
    public class UpdateExpenseRequest
    {
        public string Id { get; set; }
        public Category Category { get; set; }

        public double Amount { get; set;}

        public string Description { get; set; }
        
        public ExpenseStatus Status { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime? DueDate { get; set; }
        
        public Expense ConvertToExpense() =>
            new Expense(Id, Category, Amount, TransactionDate, status: Status, description: Description, dueDate: DueDate);
    }
}
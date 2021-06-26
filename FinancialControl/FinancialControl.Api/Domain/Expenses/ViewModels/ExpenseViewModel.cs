using System;
using FinancialControl.Api.Domain.Expenses.Models;
using FinancialControl.Api.Domain.Expenses.Models.Enums;

namespace FinancialControl.Api.Domain.Expenses.ViewModels
{
    public class ExpenseViewModel
    {
        public string Id { get; set; }
        
        public Category Category { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string ScheduleExpenseIdentity { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime? DueDate { get; set; }
        
        public Expense ConvertToExpense() =>
            new Expense(Category, Amount, TransactionDate, Description, DueDate);
    }
}
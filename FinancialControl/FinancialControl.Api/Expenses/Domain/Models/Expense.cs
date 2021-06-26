using System;
using FinancialControl.Api.Expenses.Domain.Models.Enums;
using FinancialControl.Api.Utils;

namespace FinancialControl.Api.Expenses.Domain.Models
{
    public class Expense
    {
        public string Id { get; private set; }
        public Category Category { get; private set; }

        public double Amount { get; private set; }

        public string Description { get; private set; }

        public string ScheduleExpenseIdentity { get; private set; }

        public DateTime TransactionDate { get; private set; }

        public DateTime? DueDate { get; private set; }

        public ExpenseStatus Status { get; private set; }
        
        public DateTime? PaidDate{ get; private set; }

        public bool IsAutoDebit { get; private set; }
        
        public Expense(string id, Category category, double amount, DateTime transactionDate, ExpenseStatus status,
            string description = "", DateTime? dueDate = null, string scheduleExpenseIdentity = null,
            DateTime? paidDate = null, bool isAutoDebit = false)
        {
            Id = id;
            ConstructorDefault(category, amount, transactionDate, status, description, dueDate, scheduleExpenseIdentity, 
                paidDate, isAutoDebit);
        }

        public Expense(Category category, double amount, DateTime transactionDate, ExpenseStatus status = ExpenseStatus.PAID, 
            string description = "", DateTime? dueDate = null, string scheduleExpenseIdentity = null,
            DateTime? paidDate = null, bool isAutoDebit = false)
        {
            ConstructorDefault(category, amount, transactionDate, status, description, dueDate, scheduleExpenseIdentity,
                paidDate, isAutoDebit);
        }
        
        private void ConstructorDefault(Category category, double amount, DateTime transactionDate, ExpenseStatus status, 
            string description, DateTime? dueDate, string scheduleExpenseIdentity, DateTime? paidDate,
            bool isAutoDebit)
        {
            Category = category;
            Amount = amount;
            Description = description;
            TransactionDate = transactionDate.Date;
            DueDate = dueDate.HasValue ? dueDate.Value.Date : dueDate;
            ScheduleExpenseIdentity = scheduleExpenseIdentity;
            Status = status;
            PaidDate = paidDate;
            IsAutoDebit = isAutoDebit;
        }

        public void Pay()
        {
            Status = ExpenseStatus.PAID;
            PaidDate = DateTimeUtils.GetDateTimeNow().Date;
        }
    }
}
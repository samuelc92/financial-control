using System;
using FinancialControl.Api.Expenses.Domain.Models.Enums;
using FinancialControl.Api.Utils;

namespace FinancialControl.Api.Expenses.Domain.Models
{
    public class ScheduleExpense
    {
        public string Id { get; private set; }
        
        public double Amount { get; private set; }

        public string Description { get; private set; } 

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }
        
        public int? DueDay { get; private set; }
        
        public bool IsAutoDebit { get; private set; }

        public ScheduleExpense(double amount, string description, DateTime startDate, DateTime? endDate = null,
            int? dueDay= null, bool isAutoDebit = false)
        {
            Amount = amount;
            Description = description;
            StartDate = startDate.Date;
            EndDate = endDate;
            DueDay = dueDay;
            IsAutoDebit = isAutoDebit;
        }
        
        public ScheduleExpense(string id, double amount, string description, DateTime startDate, DateTime? endDate = null,
            int? dueDay = null, bool isAutoDebit = false)
        {
            Id = id;
            Amount = amount;
            Description = description;
            StartDate = startDate.Date;
            EndDate = endDate;
            DueDay = dueDay;
            IsAutoDebit = isAutoDebit;
        }

        public Expense CreateExpense()
        {
            if (!IsValid()) throw new ArgumentException("Schedule is invalid");
            var expenseStatus = ExpenseStatus.UNPAID;
            return new Expense(Category.BILLS, Amount, DateTime.UtcNow, expenseStatus,  dueDate: BuildDueDate(),
                scheduleExpenseIdentity: Id, description: Description, isAutoDebit: IsAutoDebit);
        }

        private DateTime? BuildDueDate() =>
            DueDay.HasValue
                ? new DateTime(DateTimeUtils.GetCurrentYear(), DateTimeUtils.GetCurrentMonth(), DueDay.Value)
                : (DateTime?)null;
        
        private bool IsValid() =>
            StartDate.Date.CompareTo(DateTimeUtils.GetDateNow()) <= 0 &&
            (!EndDate.HasValue || EndDate.Value.Date.CompareTo(DateTimeUtils.GetDateNow()) > 0);
    }
}
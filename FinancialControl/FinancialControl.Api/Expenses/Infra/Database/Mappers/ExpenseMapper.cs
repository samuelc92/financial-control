using System;
using FinancialControl.Api.Expenses.Domain.Models;
using FinancialControl.Api.Expenses.Domain.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialControl.Api.Expenses.Infra.Database.Mappers
{
    public class ExpenseMapper
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public Category Category { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double Amount { get; set; }

        public string Description { get; set; }
        
        public string ScheduleExpenseIdentity { get; set; }
        
        public DateTime TransactionDate { get; set; }

        public DateTime? DueDate { get; set; }
        
        public ExpenseStatus Status { get; private set; }
        
        public DateTime? PaidDate{ get; private set; }
        
        public bool IsAutoDebit { get; set; }
        
        public ExpenseMapper(string id, Category category, double amount, string description, DateTime transactionDate, ExpenseStatus status,
            DateTime? dueDate, string scheduleExpenseIdentity, DateTime? paidDate, bool isAutoDebit)
        {
            Id = id;
            Category = category;
            Amount = amount;
            Description = description;
            ScheduleExpenseIdentity = scheduleExpenseIdentity;
            TransactionDate = transactionDate;
            DueDate = dueDate;
            Status = status;
            PaidDate = paidDate;
            IsAutoDebit = isAutoDebit;
        }

        public static ExpenseMapper BuildFrom(Expense expense) =>
            new ExpenseMapper(expense.Id, expense.Category, expense.Amount, expense.Description, expense.TransactionDate, expense.Status,
                expense.DueDate, expense.ScheduleExpenseIdentity, expense.PaidDate, expense.IsAutoDebit);

        public Expense ConvertToExpense() =>
            new Expense(Id, Category, Amount, TransactionDate, Status, Description, DueDate, ScheduleExpenseIdentity, PaidDate, IsAutoDebit);
        public static string CollectionName() =>
            "Expense";
    }
}
using System;
using FinancialControl.Api.Expenses.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialControl.Api.Expenses.Infra.Database.Mappers
{
    public class ScheduleExpenseMapper
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } 
        
        public double Amount { get; private set; }

        public string Description { get; private set; } 

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }
        
        public int? DueDay { get; private set; }
        
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsAutoDebit { get; set; }
        
        public ScheduleExpenseMapper(string id, double amount, string description, DateTime startDate, DateTime? endDate = null,
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
        
        public static ScheduleExpenseMapper BuildFrom(ScheduleExpense schedule) =>
            new ScheduleExpenseMapper(schedule.Id, schedule.Amount, schedule.Description ,schedule.StartDate, 
                schedule.EndDate, schedule.DueDay, schedule.IsAutoDebit);

        public ScheduleExpense ConvertToScheduleExpense() =>
            new ScheduleExpense(Id, Amount, Description, StartDate, EndDate, DueDay, IsAutoDebit);
        
        public static string CollectionName() =>
            "ScheduleExpense";
    }
}
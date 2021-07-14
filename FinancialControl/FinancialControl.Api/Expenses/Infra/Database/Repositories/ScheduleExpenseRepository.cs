using System.Collections.Generic;
using System.Linq;
using FinancialControl.Api.Expenses.Domain.Models;
using FinancialControl.Api.Expenses.Domain.Ports.Repositories;
using FinancialControl.Api.Expenses.Infra.Database.Mappers;
using FinancialControl.Api.Utils;
using MongoDB.Driver;

namespace FinancialControl.Api.Expenses.Infra.Database.Repositories
{
    public class ScheduleExpenseRepository : IScheduleExpenseRepository
    {
        private readonly IMongoCollection<ScheduleExpenseMapper> _schedules;
        
        public ScheduleExpenseRepository(IMongoClient mongoClient, IFinanceControlDatabaseSettings settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _schedules = database.GetCollection<ScheduleExpenseMapper>(ScheduleExpenseMapper.CollectionName());
        }

        public void Insert(ScheduleExpense scheduleExpense) =>
            _schedules.InsertOne(ScheduleExpenseMapper.BuildFrom(scheduleExpense));

        public IEnumerable<ScheduleExpense> GetAll() =>
            _schedules.Find(_ => true).ToList().Select(p => p.ConvertToScheduleExpense());
        public ICollection<ScheduleExpense> GetActives()
        {
            /*var filter = Builders<ScheduleExpenseMapper>.Filter;
            var bsonDateTimeNow = new BsonDateTime(DateTimeUtils.GetDateTimeNow().Date);
            filter.And(
            filter.Lte(p => p.StartDate, bsonDateTimeNow),
                        filter.Or(
                            filter.Not(filter.Exists(p => p.EndDate)),
                            filter.Gte(p => p.EndDate, bsonDateTimeNow)));
            */ 
            var schedules = _schedules.Find(p =>
                p.StartDate.CompareTo(DateTimeUtils.GetDateTimeNow().Date) <= 0
                && (!p.EndDate.HasValue || p.EndDate.Value.CompareTo(DateTimeUtils.GetDateTimeNow().Date) >= 0)).ToList();
            return schedules.Select(p => p.ConvertToScheduleExpense()).ToList();
        }
    }
}
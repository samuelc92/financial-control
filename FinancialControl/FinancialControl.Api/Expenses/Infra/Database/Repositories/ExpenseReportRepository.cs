using System;
using FinancialControl.Api.Expenses.Domain.Ports.Repositories;
using FinancialControl.Api.Expenses.Infra.Database.Mappers;
using FinancialControl.Api.Utils;
using MongoDB.Driver;
using System.Linq;

namespace FinancialControl.Api.Expenses.Infra.Database.Repositories
{
    public class ExpenseReportRepository : IExpenseReportRepository
    {
        private readonly IMongoCollection<ExpenseMapper> _expenses;
        
        public ExpenseReportRepository(IMongoClient mongoClient, IFinanceControlDatabaseSettings settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            _expenses = database.GetCollection<ExpenseMapper>(ExpenseMapper.CollectionName());
        }

        public dynamic TotalExpensesYear(int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year, 12, 31);
            var result = _expenses.AsQueryable()
                .Where(p => p.TransactionDate >= startDate && p.TransactionDate <= endDate)
                .Select(p => new {p.TransactionDate, p.Amount})
                .ToList()
                .GroupBy(p => p.TransactionDate.Month,
                    (k, s) => new {Month = k, Total = Math.Round(s.Sum(p => p.Amount), 2)})
                .ToList();
            return result;
        } 

        public dynamic Resume(DateTime startDate, DateTime endDate)
        {
            var totalByCategory= GetTotalByCategoryInPeriod(startDate, endDate); 
            var total = GetTotalAmountInPeriod(startDate, endDate);
            return new
            {
                Resume = totalByCategory,
                Total = Math.Round(total, 2) 
            };
        }
        
        private dynamic GetTotalByCategoryInPeriod(DateTime startDate, DateTime endDate) =>
            _expenses.AsQueryable()
                .Where(p => p.TransactionDate >= startDate && p.TransactionDate <= endDate)
                .Select(p => new {p.Category, p.Amount})
                .GroupBy(p => p.Category,
                    (k, s) => new {Category = k, Total = s.Sum(p => p.Amount)})
                .ToList();

        private double GetTotalAmountInPeriod(DateTime startDate, DateTime endDate) =>
            _expenses.AsQueryable()
                .Where(p => p.TransactionDate >= startDate && p.TransactionDate <= endDate)
                .Sum(p => p.Amount);
    }
}
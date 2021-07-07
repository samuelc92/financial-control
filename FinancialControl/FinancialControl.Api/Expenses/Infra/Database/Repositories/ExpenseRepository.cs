using System;
using System.Collections.Generic;
using System.Linq;
using FinancialControl.Api.Expenses.Domain.Models;
using FinancialControl.Api.Expenses.Domain.Ports.Repositories;
using FinancialControl.Api.Expenses.Infra.Database.Mappers;
using FinancialControl.Api.Infra.Database;
using FinancialControl.Api.Utils;
using MongoDB.Driver;

namespace FinancialControl.Api.Expenses.Infra.Database.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IMongoCollection<ExpenseMapper> _expenses;

        public ExpenseRepository(IMongoClient mongoClient, IFinanceControlDatabaseSettings settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            _expenses = database.GetCollection<ExpenseMapper>(ExpenseMapper.CollectionName());
        }

        public void Insert(Expense expense) =>
            _expenses.InsertOne(ExpenseMapper.BuildFrom(expense));

        public void Update(Expense expense) =>
            _expenses.ReplaceOne(p => p.Id.Equals(expense.Id), ExpenseMapper.BuildFrom(expense));

        public Expense GetById(string id) =>
            _expenses.Find(p => p.Id.Equals(id)).FirstOrDefault()?.ConvertToExpense();

        public void Delete(string id) =>
            _expenses.FindOneAndDelete(p => p.Id.Equals(id));
        
        public void Delete(IEnumerable<string> ids) =>
            _expenses.DeleteMany(p => ids.Contains(p.Id));

        public IEnumerable<Expense> Get(Dictionary<string, string> filters) =>
            _expenses.Find(MongoDynamicFilter.Filter<ExpenseMapper>(typeof(ExpenseMapper), filters))
                .SortByDescending(p => p.TransactionDate)
                .ToList()
                .Select(p => p.ConvertToExpense());

        public ICollection<Expense> GetByMonthAndYear(int month, int year)
        {
            var firstDateOfMonth = new DateTime(year, month, 1);
            var expensesMapper = _expenses.Find(p =>
                p.TransactionDate.CompareTo(firstDateOfMonth) >= 0).ToList();
            return expensesMapper.Select(p => p.ConvertToExpense()).ToList();
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
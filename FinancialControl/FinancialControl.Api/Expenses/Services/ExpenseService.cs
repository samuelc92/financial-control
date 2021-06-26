using System;
using System.Collections.Generic;
using FinancialControl.Api.Expenses.Domain.Models;
using FinancialControl.Api.Expenses.Domain.Ports.Repositories;
using FinancialControl.Api.Expenses.Services.Interfaces;

namespace FinancialControl.Api.Expenses.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;

        public ExpenseService(IExpenseRepository repository)
        {
            _repository = repository;
        }

        public void Insert(Expense expense) =>
            _repository.Insert(expense);

        public void Update(Expense expense) =>
            _repository.Update(expense);
        
        public IEnumerable<Expense> Get(Dictionary<string, string> filters) =>
            _repository.Get(filters);

        public void Pay(string id)
        {
            var expense = _repository.GetById(id);
            expense.Pay();
            _repository.Update(expense);
        }

        public dynamic Resume(DateTime startDate, DateTime endDate) =>
            _repository.Resume(startDate.Date, endDate.Date);

        public void Delete(string id) =>
            _repository.Delete(id);
    }
}
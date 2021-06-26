using System;
using System.Collections.Generic;
using FinancialControl.Api.Expenses.Domain.Models;

namespace FinancialControl.Api.Expenses.Domain.Ports.Repositories
{
    public interface IExpenseRepository
    { 
        void Insert(Expense expense);

        void Update(Expense expense);

        Expense GetById(string id);

        IEnumerable<Expense> Get(Dictionary<string, string> filters);

        ICollection<Expense> GetByMonthAndYear(int month, int year);
        
        dynamic Resume(DateTime startDate, DateTime endDate);
        
        void Delete(string id);
    }
}
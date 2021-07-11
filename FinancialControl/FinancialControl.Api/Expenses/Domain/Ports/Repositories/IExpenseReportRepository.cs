using System;

namespace FinancialControl.Api.Expenses.Domain.Ports.Repositories
{
    public interface IExpenseReportRepository
    {
        dynamic TotalExpensesYear(int year);
        
        dynamic Resume(DateTime startDate, DateTime endDate);
    }
}
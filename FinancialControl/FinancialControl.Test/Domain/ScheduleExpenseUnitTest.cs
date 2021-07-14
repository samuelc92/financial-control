using System;
using FinancialControl.Api.Expenses.Domain.Models;
using FinancialControl.Api.Expenses.Domain.Models.Enums;
using FinancialControl.Api.Utils;
using NUnit.Framework;

namespace FinancialControl.Test.Domain
{
    [TestFixture]
    public class ScheduleExpenseUnitTest
    {

        [Test]
        public void CreateExpense_WithDueDay_ReturnNewExpenseWithDueDate()
        {
            var currentYear = DateTimeUtils.GetCurrentYear();
            var currentMonth = DateTimeUtils.GetCurrentMonth();
            var expectedExpense = new Expense(Category.BILLS, Double.MinValue, DateTime.UtcNow, ExpenseStatus.UNPAID, 
                dueDate: new DateTime(currentYear, currentMonth, 6), scheduleExpenseIdentity: String.Empty); 
            var scheduleExpense = new ScheduleExpense(Double.MinValue, String.Empty, DateTime.UtcNow.Date,
                DateTime.UtcNow.AddDays(1).Date, 6);

            var expense = scheduleExpense.CreateExpense();

            Assert.AreEqual(expense.Status, expectedExpense.Status);
            Assert.AreEqual(expense.DueDate, expectedExpense.DueDate);
        }

        [Test]
        public void CreateExpense_WithInvalidStartDate_ThrowArgumentException()
        {
             var scheduleExpense = new ScheduleExpense(Double.MinValue, String.Empty, DateTime.UtcNow.AddDays(10).Date,
                 DateTime.UtcNow.AddDays(1).Date, 6);
 
             Assert.Throws<ArgumentException>(() => scheduleExpense.CreateExpense());
        }
    }
}
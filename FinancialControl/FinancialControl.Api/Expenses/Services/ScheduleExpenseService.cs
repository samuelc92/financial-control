using System.Collections.Generic;
using System.Linq;
using FinancialControl.Api.Expenses.Domain.Models;
using FinancialControl.Api.Expenses.Domain.Ports.Repositories;
using FinancialControl.Api.Expenses.Services.Interfaces;
using FinancialControl.Api.Utils;

namespace FinancialControl.Api.Expenses.Services
{
    public class ScheduleExpenseService : IScheduleExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IScheduleExpenseRepository _repository;

        public ScheduleExpenseService(IExpenseRepository expenseRepository, IScheduleExpenseRepository repository)
        {
            _expenseRepository = expenseRepository;
            _repository = repository;
        }

        public void Insert(ScheduleExpense schedule) =>
            _repository.Insert(schedule);

        public IEnumerable<ScheduleExpense> GetAll() =>
            _repository.GetAll();

        public void Run()
        {
            var schedulers = _repository.GetActives();
            var expenses = _expenseRepository.GetByMonthAndYear(DateTimeUtils.GetDateTimeNow().Month,
                DateTimeUtils.GetDateTimeNow().Year);
            schedulers = schedulers.Where(p => expenses.Count(e =>
                !string.IsNullOrEmpty(e.ScheduleExpenseIdentity) && e.ScheduleExpenseIdentity.Equals(p.Id)) == 0).ToList();
            foreach (var scheduleExpense in schedulers)
                _expenseRepository.Insert(scheduleExpense.CreateExpense());
        }
    }
}
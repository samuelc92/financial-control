using System;
using FinancialControl.Api.Expenses.Domain.Ports.Repositories;
using FinancialControl.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseReportsController : ControllerBase
    {
        private readonly IExpenseReportRepository _repository;

        public ExpenseReportsController(IExpenseReportRepository expenseReportRepository)
        {
            _repository = expenseReportRepository;
        }
        
        [HttpGet("total_year")]
        public IActionResult Get([FromQuery] int? year) =>
            Ok(_repository.TotalExpensesYear(year ?? DateTime.UtcNow.Year));
        
        [HttpGet("Resume")]
        public IActionResult Resume([FromQuery] DateTime? startDateTime, [FromQuery] DateTime? endDateTime)
        {
            var year = DateTimeUtils.GetCurrentYear();
            var month = DateTimeUtils.GetCurrentMonth();
            startDateTime = startDateTime.HasValue
                ? startDateTime
                : new DateTime(year, month, 1);
            endDateTime = endDateTime.HasValue
                ? endDateTime
                : new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return Ok(_repository.Resume(startDateTime.Value, endDateTime.Value));
        }
    }
}
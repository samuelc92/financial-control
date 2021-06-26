using System;
using System.Collections.Generic;
using FinancialControl.Api.Expenses.Domain.DTOs;
using FinancialControl.Api.Expenses.Services.Interfaces;
using FinancialControl.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinancialControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;
        private ILogger<ExpenseController> _logger;

        public ExpenseController(IExpenseService service,
            ILogger<ExpenseController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Dictionary<string, string> filters)
        {
            return Ok(_service.Get(filters));
        }

        [HttpPost]
        public IActionResult Insert([FromBody] ExpenseRequest expense)
        {
            _logger.LogInformation("Insert Expense: Expense Request {@Expense}", expense);
            _service.Insert(expense.ConvertToExpense());
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] string id, [FromBody] UpdateExpenseRequest expense)
        {
            _logger.LogInformation("Update Expense: Expense Request {@Expense}", expense);
            if (id != expense.Id) return BadRequest("Invalid id");
            _service.Update(expense.ConvertToExpense());
            return NoContent();
        }

        [HttpPut("Pay")]
        public IActionResult Pay([FromBody] string id)
        {
            _service.Pay(id);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            _logger.LogInformation($@"Delete Expense: {id}");
            _service.Delete(id);
            return NoContent();
        }

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
            return Ok(_service.Resume(startDateTime.Value, endDateTime.Value));
        }
    }
}
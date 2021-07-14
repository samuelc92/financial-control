using FinancialControl.Api.Expenses.Domain.DTOs;
using FinancialControl.Api.Expenses.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleExpenseController : ControllerBase
    {
        private readonly IScheduleExpenseService _service;

        public ScheduleExpenseController(IScheduleExpenseService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() =>
            Ok(_service.GetAll());
        
        [HttpPost]
        public IActionResult Insert([FromBody]ScheduleExpenseRequest schedule)
        {
            _service.Insert(schedule.ConvertToScheduleExpense());
            return NoContent();
        }
        
        [HttpPost("run")]
        public IActionResult CreateByScheduler()
        {
            _service.Run();
            return NoContent();
        }
    }
}

using ExpenseService.Api.Ports.Requests;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;

namespace ExpenseService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
  public readonly IAmACommandProcessor _commandProcessor;

  public ExpensesController(IAmACommandProcessor commandProcessor)
  {
    _commandProcessor = commandProcessor;
  }

  [HttpPost(Name = "PostExpense")]
  public async Task<ActionResult<AddExpense>> Post(AddExpense addExpense)
  {
      await _commandProcessor.SendAsync(addExpense);
      return Ok(addExpense);
  }
}
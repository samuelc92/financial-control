
using ExpenseService.Api.Ports.Requests;
using ExpenseService.Api.Ports.Responses;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;

namespace ExpenseService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
  public readonly IAmACommandProcessor _commandProcessor;
  private readonly IQueryProcessor _queryProcessor;

  public ExpensesController(IAmACommandProcessor commandProcessor,IQueryProcessor queryProcessor)
  {
    _commandProcessor = commandProcessor;
    _queryProcessor = queryProcessor;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<FindExpenseResult>> Get(string id)
  {
      var addedExpense = await _queryProcessor.ExecuteAsync(new FindExpenseById(Guid.NewGuid()));

      if (addedExpense is null) return new NotFoundResult();

      return Ok(addedExpense);
  }


  [HttpPost(Name = "PostExpense")]
  public async Task<ActionResult<FindExpenseResult>> Post(AddExpense addExpense)
  {
      await _commandProcessor.SendAsync(addExpense);
      return new NoContentResult();
  }
}

using ExpenseService.Api.Ports.Requests;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;

namespace ExpenseService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
  public readonly IAmACommandProcessor _commandProcessor;
  private readonly IQueryProcessor _queryProcessor;

  public ExpensesController(IAmACommandProcessor commandProcessor,IQueryProcessor queryProcessor)
  {
    _commandProcessor = commandProcessor;
    _queryProcessor = queryProcessor;
  }

  [HttpPost(Name = "PostExpense")]
  public async Task<ActionResult<AddExpense>> Post(AddExpense addExpense)
  {
      await _commandProcessor.SendAsync(addExpense);

      var addedExpense = await _queryProcessor.ExecuteAsync(new FindExpenseByDescription(addExpense.Description));

      if (addedExpense is null) return new NotFoundResult();

      return Ok(addedExpense);
  }
}
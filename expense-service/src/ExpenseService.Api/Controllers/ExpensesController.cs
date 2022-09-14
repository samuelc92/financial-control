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

  [HttpGet]
  public async Task<ActionResult<ICollection<FindExpenseResult>>> Get([FromQuery] string? id)
  {
      var addedExpense = await _queryProcessor.ExecuteAsync(new FindExpenseParameter());

      if (addedExpense is null) return new NotFoundResult();

      return Ok(addedExpense);
  }


  [HttpPost(Name = "PostExpense")]
  public async Task<ActionResult<FindExpenseResult>> Post(AddExpense addExpense)
  {
      await _commandProcessor.SendAsync(addExpense);
      return new NoContentResult();
  }

  [HttpDelete(Name = "DeleteExpense")]
  public async Task<ActionResult> Delete([FromQuery(Name = "id")] string[] expenseIds)
  {
    await _commandProcessor.SendAsync(new DeleteExpense(expenseIds.Select(p => Guid.Parse(p)).ToArray()));
    return new NoContentResult();
  }
}
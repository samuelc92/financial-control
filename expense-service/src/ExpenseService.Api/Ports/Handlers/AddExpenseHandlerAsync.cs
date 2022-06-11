
using ExpenseService.Api.Ports.Requests;
using Paramore.Brighter;

namespace ExpenseService.Api.Ports.Handlers;

public class AddExpenseHandlerAsync: RequestHandlerAsync<AddExpense>
{

  public override async Task<AddExpense> HandleAsync(AddExpense addExpense, CancellationToken cancellationToken = default(CancellationToken))
  {
    Console.WriteLine("Testest");
    return await base.HandleAsync(addExpense, cancellationToken);
  }
}
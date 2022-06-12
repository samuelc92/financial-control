
using ExpenseService.Api.Domain;
using ExpenseService.Api.Infrastructure.DataAccess;
using ExpenseService.Api.Ports.Requests;
using Paramore.Brighter;

namespace ExpenseService.Api.Ports.Handlers;

public class AddExpenseHandlerAsync: RequestHandlerAsync<AddExpense>
{

  private readonly ExpenseDbContext _uow;

  public AddExpenseHandlerAsync(ExpenseDbContext uow)
  {
    _uow = uow;
  }

  public override async Task<AddExpense> HandleAsync(AddExpense addExpense, CancellationToken cancellationToken = default(CancellationToken))
  {
    _uow.Add(new Expense(addExpense.Description, addExpense.Amount));

    await _uow.SaveChangesAsync(cancellationToken);

    return await base.HandleAsync(addExpense, cancellationToken);
  }
}

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
    System.Console.WriteLine($"AddExpense Command Id: {addExpense.Id}");

    _uow.Add(new Expense(addExpense.Category, addExpense.Amount, addExpense.TransactionDate, addExpense.Status, addExpense.Description));

    await _uow.SaveChangesAsync(cancellationToken);

    return await base.HandleAsync(addExpense, cancellationToken);
  }
}
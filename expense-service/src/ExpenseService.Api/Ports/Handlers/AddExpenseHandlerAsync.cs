
using ExpenseService.Api.Domain;
using ExpenseService.Api.Infrastructure.DataAccess;
using ExpenseService.Api.Ports.Requests;
using Paramore.Brighter;

namespace ExpenseService.Api.Ports.Handlers;

public class AddExpenseHandlerAsync: RequestHandlerAsync<AddExpense>
{
  private readonly IAmACommandProcessor _postBox;

  private readonly ExpenseDbContext _uow;

  public AddExpenseHandlerAsync(ExpenseDbContext uow, IAmACommandProcessor postBox)
  {
    _uow = uow;
    _postBox = postBox;
  }

  public override async Task<AddExpense> HandleAsync(AddExpense addExpense, CancellationToken cancellationToken = default(CancellationToken))
  {
    System.Console.WriteLine($"AddExpense Command Id: {addExpense.Id}");

    var posts = new List<Guid>();
    var tx = await _uow.Database.BeginTransactionAsync(cancellationToken);
    try
    {
      var expense = buildExpense(addExpense);
      _uow.Add(expense);
      posts.Add(await _postBox.DepositPostAsync(buildExpenseCreatedEvent(expense)));
      await _uow.SaveChangesAsync(cancellationToken);
      await tx.CommitAsync(cancellationToken);
    }
    catch(Exception)
    {
      await tx.RollbackAsync(cancellationToken);
      return await base.HandleAsync(addExpense, cancellationToken);
    }
    await _postBox.ClearOutboxAsync(posts, cancellationToken: cancellationToken);
    return await base.HandleAsync(addExpense, cancellationToken);
  }

  private Expense buildExpense(AddExpense addExpense) =>
    new Expense(
      addExpense.Id,
      addExpense.Category,
      addExpense.Description,
      addExpense.Amount,
      addExpense.Status,
      addExpense.TransactionDate,
      DueDate: null,
      PaidDate: null);

  private ExpenseCreatedEvent buildExpenseCreatedEvent(Expense expense) =>
    new ExpenseCreatedEvent(
      expense.Id,
      expense.Category.ToString(),
      expense.Amount,
      expense.TransactionDate);
}
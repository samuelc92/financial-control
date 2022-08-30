using ExpenseService.Api.Infrastructure.DataAccess;
using ExpenseService.Api.Ports.Requests;
using Paramore.Brighter;

namespace ExpenseService.Api.Ports.Handlers;

public class DeleteExpenseHandlerAsync: RequestHandlerAsync<DeleteExpense>
{
  private readonly IAmACommandProcessor _postBox;

  private readonly ExpenseDbContext _uow;

  public DeleteExpenseHandlerAsync(IAmACommandProcessor postBox, ExpenseDbContext uow)
  {
    _postBox = postBox;
    _uow = uow;
  }

  public override async Task<DeleteExpense> HandleAsync(DeleteExpense deleteExpense, CancellationToken cancellationToken = default(CancellationToken))
  {
    var tx = await _uow.Database.BeginTransactionAsync(cancellationToken);
    try
    {
      _uow.Expenses.RemoveRange(_uow.Expenses.Where(p => deleteExpense.ExpenseIds.Contains(p.Id)));
      await _uow.SaveChangesAsync(cancellationToken);
      await tx.CommitAsync(cancellationToken);
    }
    catch(Exception)
    {
      await tx.RollbackAsync(cancellationToken);
      return await base.HandleAsync(deleteExpense, cancellationToken);
    }
    return await base.HandleAsync(deleteExpense, cancellationToken);
  }
}
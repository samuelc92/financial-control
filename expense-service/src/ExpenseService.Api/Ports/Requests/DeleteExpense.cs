using Paramore.Brighter;

namespace ExpenseService.Api.Ports.Requests;

public class DeleteExpense : Command
{
  public Guid[] ExpenseIds { get; private set; }

  public DeleteExpense(Guid[] expenseIds) : base(Guid.NewGuid())
  {
    ExpenseIds = expenseIds; 
  } 
}
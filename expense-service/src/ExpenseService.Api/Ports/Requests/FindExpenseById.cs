
using ExpenseService.Api.Ports.Responses;
using Paramore.Darker;

namespace ExpenseService.Api.Ports.Requests;

public class FindExpenseById : IQuery<FindExpenseResult>
{
  public Guid Id { get; }

  public FindExpenseById(Guid id)
  {
    Id = id;
  }
}


using ExpenseService.Api.Ports.Responses;
using Paramore.Darker;

namespace ExpenseService.Api.Ports.Requests;

public class FindExpenseById : IQuery<FindExpenseResult>
{
  public int Id { get; }

  public FindExpenseById(int id)
  {
    Id = id;
  }
}

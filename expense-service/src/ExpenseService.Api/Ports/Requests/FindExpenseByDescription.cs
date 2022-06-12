
using ExpenseService.Api.Ports.Responses;
using Paramore.Darker;

namespace ExpenseService.Api.Ports.Requests;

public class FindExpenseByDescription : IQuery<FindExpenseResult>
{
  public string Description { get; }

  public FindExpenseByDescription(string description)
  {
    Description = description;
  }
}

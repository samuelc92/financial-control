
using ExpenseService.Api.Ports.Responses;
using Paramore.Darker;

namespace ExpenseService.Api.Ports.Requests;

public class FindExpenseParameter : IQuery<ICollection<FindExpenseResult>>
{
  public Guid? Id { get; set;}
}

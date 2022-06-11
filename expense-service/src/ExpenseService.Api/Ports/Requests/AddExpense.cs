using Paramore.Brighter;

namespace ExpenseService.Api.Ports.Requests;

public class AddExpense : Command
{
  public string Description { get; }
  public double Amount { get; }

  public AddExpense(string description, double amount) : base(Guid.NewGuid())
  {
    Description = description;
    Amount = amount;
  }
}
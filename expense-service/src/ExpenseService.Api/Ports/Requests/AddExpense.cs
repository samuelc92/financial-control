using ExpenseService.Api.Domain;
using Paramore.Brighter;

namespace ExpenseService.Api.Ports.Requests;

public class AddExpense : Command
{
  public string? Description { get; private set; }

  public double Amount { get; }

  public Category Category { get; private set; }

  public ExpenseStatus Status { get; private set; }

  public DateTime TransactionDate { get; private set; }

  public AddExpense(string? description, double amount, Category category, ExpenseStatus status, DateTime transactionDate) : base(Guid.NewGuid())
  {
    Description = description;
    Amount = amount;
    Category = category;
    Status = status;
    TransactionDate = transactionDate;
  }
}
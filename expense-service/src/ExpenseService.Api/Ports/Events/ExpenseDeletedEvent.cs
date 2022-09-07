using ExpenseService.Api.Domain;
using Paramore.Brighter;

namespace ExpenseService.Api.Ports.Events;

public class ExpenseDeletedEvent : Event
{
  public ExpenseDeletedEvent(Guid id, string category, double amount, DateTime transactionDate) : base(id)
  {
    Category = category;
    Amount = amount;
    TransactionDate = transactionDate;
  }

  public string Category { get; set; }
	public double Amount { get; set; }
  public DateTime TransactionDate { get; set; }

  public static ExpenseCreatedEvent Of(Expense expense) =>
    new ExpenseCreatedEvent(
      expense.Id,
      expense.Category.ToString(),
      expense.Amount,
      expense.TransactionDate);
}
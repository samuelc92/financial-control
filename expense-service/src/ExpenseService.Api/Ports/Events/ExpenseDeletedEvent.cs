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

  public static ExpenseDeletedEvent Of(Expense expense) =>
    new ExpenseDeletedEvent(
      Guid.NewGuid(),
      expense.Category.ToString(),
      expense.Amount,
      expense.TransactionDate);
}
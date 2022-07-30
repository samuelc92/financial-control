
using ExpenseService.Api.Domain;

namespace ExpenseService.Api.Ports.Responses;

public class FindExpenseResult
{
  public Guid Id { get; set; }
  public string? Description { get; }
  public double Amount { get; set; }
  public Category Category { get; set; }
  public DateTime TransactionDate { get; set; }
  public ExpenseStatus Status { get; private set; }

  public FindExpenseResult(Expense expense)
  {
    Id = expense.Id;
    Description = expense.Description; 
    Amount = expense.Amount;
    Category = expense.Category;
    TransactionDate = expense.TransactionDate;
    Status = expense.Status;
  }
}

using ExpenseService.Api.Domain;

namespace ExpenseService.Api.Ports.Responses;

public class FindExpenseResult
{
  public int Id { get; set; }
  public string Description { get; }
  public double Amount { get; set; }

  public FindExpenseResult(Expense expense)
  {
    Id = expense.Id;
    Description = expense.Description; 
    Amount = expense.Amount;
  }
}
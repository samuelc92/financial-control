
namespace ExpenseService.Api.Domain;

public class Expense
{
  public int Id { get; set; }
  public string Description { get; set; }
  public double Amount { get; set; }
}
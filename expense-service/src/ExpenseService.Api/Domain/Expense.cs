
namespace ExpenseService.Api.Domain;

public class Expense
{
  public int Id { get; set; }
  public string Description { get; set; }
  public double Amount { get; set; }

  public Expense(string description, double amount)
  {
    Description = description;
    Amount = amount;
  }

  public Expense(int id, string description, double amount)
  {
    Id = id;
    Description = description;
    Amount = amount;
  }
}
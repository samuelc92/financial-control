
namespace ExpenseService.Api.Domain;

public class Expense
{
  public int Id { get; private set; }

  public Category Category { get; private set; }

  public string? Description { get; private set; }

  public double Amount { get; private set; }

  public ExpenseStatus Status { get; private set; }

  public DateTime TransactionDate { get; private set; }

  public DateTime? DueDate { get; private set; }

  public DateTime? PaidDate{ get; private set; }

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

  public Expense(int id, Category category, double amount, DateTime transactionDate, ExpenseStatus status,
    string? description = null, DateTime? dueDate = null, DateTime? paidDate = null)
  {
    Id = id;
    ConstructorDefault(category, amount, transactionDate, status, description, dueDate, paidDate);
  }

  public Expense(Category category, double amount, DateTime transactionDate, ExpenseStatus status = ExpenseStatus.PAID, 
      string? description = null, DateTime? dueDate = null, DateTime? paidDate = null, bool isAutoDebit = false)
  {
    ConstructorDefault(category, amount, transactionDate, status, description, dueDate, paidDate);
  }

  private void ConstructorDefault(Category category, double amount, DateTime transactionDate, ExpenseStatus status, 
      string? description, DateTime? dueDate, DateTime? paidDate)
  {
    Category = category;
    Amount = amount;
    Description = description;
    TransactionDate = transactionDate.Date;
    DueDate = dueDate.HasValue ? dueDate.Value.Date : dueDate;
    Status = status;
    PaidDate = paidDate;
  }
}

public enum Category
{
  OTHER,
  BILLS,
  DELIVERY,
  PUB,
  SHOP,
  SUPERMARKET,
  UBER
}

public enum ExpenseStatus
{
  PAID,
  UNPAID 
} 
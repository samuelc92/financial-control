
namespace ExpenseService.Api.Domain;

public record Expense(
  Guid Id, 
  Category Category, 
  string? Description, 
  double Amount,
  ExpenseStatus Status,
  DateTime TransactionDate,
  DateTime? DueDate,
  DateTime? PaidDate
);

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
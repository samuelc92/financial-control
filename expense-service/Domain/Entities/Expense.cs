namespace ExpenseService.Domain.Entities
{
   public record Expense(
     Guid Id,
     Category Category,
     double Amount, 
     string Description, 
     DateTime TransactionDate,
     DateTime? DueDate,
     ExpenseStatus Status
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
}
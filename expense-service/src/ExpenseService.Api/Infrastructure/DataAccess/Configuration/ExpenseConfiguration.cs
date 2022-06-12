
using ExpenseService.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseService.Api.Infrastructure.DataAccess.Configuration;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
  public void Configure(EntityTypeBuilder<Expense> builder)
  {
    builder.HasKey("Id");
    builder.Property(p => p.Id).IsRequired();
    builder.Property(p => p.Category).IsRequired();
    builder.Property(p => p.Amount).IsRequired();
    builder.Property(p => p.Status).IsRequired();
    builder.Property(p => p.TransactionDate).IsRequired();
  }
}
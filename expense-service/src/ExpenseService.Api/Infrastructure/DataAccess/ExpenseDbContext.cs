using ExpenseService.Api.Domain;
using ExpenseService.Api.Infrastructure.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Api.Infrastructure.DataAccess;

public class ExpenseDbContext : DbContext
{
  public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) { }

  public DbSet<Expense> Expenses { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    new ExpenseConfiguration().Configure(modelBuilder.Entity<Expense>());

    base.OnModelCreating(modelBuilder);
  }
}
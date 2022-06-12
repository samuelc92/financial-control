using ExpenseService.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Api.Infrastructure.DataAccess;

public class ExpenseDbContext : DbContext
{
  public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) { }

  public DbSet<Expense> Expenses { get; set; }
}
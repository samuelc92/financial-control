using ExpenseService.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseService.Api.Infrastructure.DataAccess;

public class ExpenseDbContext : DbContext
{
  public DbSet<Expense> Expenses { get; set; }

  public string DbPath { get; set; }
  public ExpenseDbContext()
  {
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    DbPath = System.IO.Path.Join(path, "expenses.db");
  }

  protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}
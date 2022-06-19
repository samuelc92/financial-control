using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Paramore.Brighter.Outbox.Sqlite;

namespace ExpenseService.Api.Infrastructure.DataAccess;

public static class SchemaCreation
{
  private const string OUTBOX_TABLE_NAME = "Outbox";

  public static IHost MigrateDatabase(this IHost webHost)
  {
    using(var scope = webHost.Services.CreateScope())
    {
      var services = scope.ServiceProvider;
      try
      {
        var db = services.GetRequiredService<ExpenseDbContext>();
        db.Database.Migrate();
      }
      catch(Exception ex)
      {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
      }

      return webHost;
    }
  }

  public static IHost CreateOutbox(this IHost webHost)
  {
    try
    {
      using var sqlConnection = new SqliteConnection("Filename=expenses.db;Cache=Shared");
      sqlConnection.Open();

      using var exists = sqlConnection.CreateCommand();
      exists.CommandText = SqliteOutboxBuilder.GetExistsQuery(OUTBOX_TABLE_NAME);
      using var reader = exists.ExecuteReader(CommandBehavior.SingleRow);

      if (reader.HasRows) return webHost;

      using var command = sqlConnection.CreateCommand();
      command.CommandText = SqliteOutboxBuilder.GetDDL(OUTBOX_TABLE_NAME);
      command.ExecuteScalar();
    }
    catch(Exception ex)
    {
      Console.WriteLine($"Issue with creating Outbox table, {ex.Message}");
      throw;
    }

    return webHost;
  }
}
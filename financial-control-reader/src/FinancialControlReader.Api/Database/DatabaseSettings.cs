namespace FinancialControlReader.Api.Database;

public abstract class IDatabaseSettings
{
    public string? ConnectionString { get; set; } 
    public string? DatabaseName { get; set; }
}
public class DatabaseSettings : IDatabaseSettings { }
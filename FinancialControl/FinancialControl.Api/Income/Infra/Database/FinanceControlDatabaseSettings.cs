namespace FinancialControl.Income.Api.Infra.Database
{
    public class FinanceControlDatabaseSettings : IFinanceControlDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    
    public interface IFinanceControlDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
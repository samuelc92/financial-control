namespace FinancialControlReader.Api.Domain.UseCases;

public interface IRegisterAnnualReportUseCase
{
  Task RegisterAsync(string category, double amount, DateTime transactionDate);    
}
namespace FinancialControlReader.Api.Domain.UseCases;

public interface IRegisterAnnualReportUseCase
{
  Task RegisterAsync(double amount, DateTime transactionDate);    
}
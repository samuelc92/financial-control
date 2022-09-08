namespace FinancialControlReader.Api.Domain.UseCases;

public interface IDeductAnnualReportUseCase 
{
  Task RunAsync(double amount, DateTime transactionDate);    
}
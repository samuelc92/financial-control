namespace FinancialControlReader.Api.Domain.UseCases;

public interface IDeductCategoryReportUseCase
{
  Task RunAsync(string category, double amount, DateTime transactionDate);    
}
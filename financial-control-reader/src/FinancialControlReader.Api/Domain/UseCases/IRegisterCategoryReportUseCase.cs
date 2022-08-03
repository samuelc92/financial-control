namespace FinancialControlReader.Api.Domain.UseCases;

public interface IRegisterCategoryReportUseCase
{
    Task RegisterAsync(string category, double amount, DateTime transactionDate);    
}
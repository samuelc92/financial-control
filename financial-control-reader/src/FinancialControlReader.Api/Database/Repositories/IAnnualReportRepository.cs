using FinancialControlReader.Api.Domain.Models;

namespace FinancialControlReader.Api.Database.Repositories;

public interface IAnnualReportRepository
{
	Task<AnnualReport> Get(int id);
  Task Insert(AnnualReport annualReport);
  Task UpdateAsync(AnnualReport annualReport);
}
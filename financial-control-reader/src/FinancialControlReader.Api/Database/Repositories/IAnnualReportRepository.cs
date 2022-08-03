using FinancialControlReader.Api.Domain;

namespace FinancialControlReader.Api.Database.Repositories;

public interface IAnnualReportRepository
{
	Task<AnnualReport> Get(string id);
  Task Insert(AnnualReport annualReport);
  Task Update(AnnualReport annualReport);
}
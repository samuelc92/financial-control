using FinancialControlReader.Api.Domain;

namespace FinancialControlReader.Api.Database.Repositories;

public interface ICategoryReportRepository
{
		Task<CategoryReport> GetCategoryReport(string id);
		Task Insert(CategoryReport categoryReport);
		Task Update(CategoryReport categoryReport);
}
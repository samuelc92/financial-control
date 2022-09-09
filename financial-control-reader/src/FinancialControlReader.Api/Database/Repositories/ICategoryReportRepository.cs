using FinancialControlReader.Api.Domain.Models;

namespace FinancialControlReader.Api.Database.Repositories;

public interface ICategoryReportRepository
{
		Task<CategoryReport> GetCategoryReportAsync(string id);
		Task InsertAsync(CategoryReport categoryReport);
		Task UpdateAsync(CategoryReport categoryReport);
}
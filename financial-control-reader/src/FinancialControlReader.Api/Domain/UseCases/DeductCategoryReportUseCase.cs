using FinancialControlReader.Api.Database.Repositories;
using FinancialControlReader.Api.Domain.Models;

namespace FinancialControlReader.Api.Domain.UseCases;

public class DeductCategoryReportUseCase : IDeductCategoryReportUseCase
{
  public DeductCategoryReportUseCase(ICategoryReportRepository categoryReportRepository)
  {
    _categoryReportRepository = categoryReportRepository; 
  }

  readonly ICategoryReportRepository _categoryReportRepository;

  public async Task RunAsync(string category, double amount, DateTime transactionDate)
  {
    var categoryReportId = $"{transactionDate.Month}{transactionDate.Year}";
    var categoryReport = await GetCategoryReportAsync(categoryReportId);
    var resume = GetCategoryReportResume(categoryReport, category); 
    resume.Total -= amount;
    await _categoryReportRepository.Update(categoryReport);
  }    

  private async Task<CategoryReport> GetCategoryReportAsync(string categoryReportId)
  {
    var categoryReport = await _categoryReportRepository.GetCategoryReport(categoryReportId);
    if (categoryReport is null) throw new Exception($"Category report not found. categoryReportId={categoryReportId}");
    return categoryReport;
  }

  private Resume GetCategoryReportResume(CategoryReport categoryReport, string category)
  {
    var resume = categoryReport.GetResumeByCategory(category);
    if (resume is null) throw new Exception($"Category report resume not found. categoryReportId={categoryReport.Id} category={category}");
    return resume;
  }
}
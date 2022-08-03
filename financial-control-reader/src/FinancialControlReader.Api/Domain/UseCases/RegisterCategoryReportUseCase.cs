using FinancialControlReader.Api.Database.Repositories;
using FinancialControlReader.Api.Domain.Models;

namespace FinancialControlReader.Api.Domain.UseCases;

public class RegisterCategoryReportUseCase : IRegisterCategoryReportUseCase
{
  readonly ICategoryReportRepository _categoryReportRepository;

  public RegisterCategoryReportUseCase(ICategoryReportRepository categoryReportRepository)
  {
    _categoryReportRepository = categoryReportRepository;
  }

  public async Task RegisterAsync(string category, double amount, DateTime transactionDate)
  {
    var categoryReportId = $"{transactionDate.Month}{transactionDate.Year}"; 

    var categoryReport = await _categoryReportRepository.GetCategoryReport(categoryReportId);
    if (categoryReport == null)
      await _categoryReportRepository.Insert(CreateCategoryReport(categoryReportId, category, amount, transactionDate));
    else
    {
      UpdateCategoryReport(categoryReport, category, amount);
      await _categoryReportRepository.Update(categoryReport);
    }
  }

  private void UpdateCategoryReport(CategoryReport categoryReport, string category, double amount)
  {
    var resume = categoryReport.Resume.FirstOrDefault(x => x.Category == category);
    if (resume == null)
      categoryReport.Resume.Add(new Resume { Category = category, Total = amount });
    else
      resume.Total += amount;
  }

  private CategoryReport CreateCategoryReport(string id, string category, double amount, DateTime transactionDate) =>
    new CategoryReport
    {
      Id = id,
      Resume = new List<Resume> 
      { 
        new Resume
        {
          Category = category,
          Total = amount
        }
      }
    };
}
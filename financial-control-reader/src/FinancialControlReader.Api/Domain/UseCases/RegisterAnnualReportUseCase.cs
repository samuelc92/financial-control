using FinancialControlReader.Api.Database.Repositories;
using FinancialControlReader.Api.Domain.Models;

namespace FinancialControlReader.Api.Domain.UseCases;
public class RegisterAnnualReportUseCase : IRegisterAnnualReportUseCase
{
  readonly IAnnualReportRepository _annualReportRepository;

  public RegisterAnnualReportUseCase(IAnnualReportRepository annualReportRepository)
  {
    _annualReportRepository = annualReportRepository;
  }

  public async Task RegisterAsync(double amount, DateTime transactionDate)
  {
    var reportId = transactionDate.Year; 
    var report = await _annualReportRepository.Get(reportId);
    if (report == null)
      await _annualReportRepository.Insert(CreateAnnualReport(reportId, amount, transactionDate));
    else
    {
      UpdateAnnualReport(report, amount, transactionDate);
      await _annualReportRepository.UpdateAsync(report);
    }
  }
  
  private void UpdateAnnualReport(AnnualReport annualReport, double amount, DateTime transactionDate)
  {
    var data = annualReport?.Resume?.FirstOrDefault(x => x.Month == transactionDate.Month);
    if (data == null)
      annualReport?.Resume?.Add(new AnnualReportResume { Month = transactionDate.Month, Total = amount });
    else
      data.Total += amount;
  }

  private AnnualReport CreateAnnualReport(int id, double amount, DateTime transactionDate) =>
    new AnnualReport (
      id,
      new AnnualReportResume
      {
        Month = transactionDate.Month,
        Total = amount
      }
    );
}
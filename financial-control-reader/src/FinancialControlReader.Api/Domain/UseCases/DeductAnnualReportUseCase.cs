using FinancialControlReader.Api.Database.Repositories;
using FinancialControlReader.Api.Domain.Models;

namespace FinancialControlReader.Api.Domain.UseCases;

public class DeductAnnualReportUseCase : IDeductAnnualReportUseCase 
{
  public DeductAnnualReportUseCase(IAnnualReportRepository annualReportRepository, ILogger<DeductAnnualReportUseCase> logger)
	{
	  _annualReportRepository = annualReportRepository;	
    _logger = logger;
	}

  readonly IAnnualReportRepository _annualReportRepository;
  readonly ILogger<DeductAnnualReportUseCase> _logger;
	
  public async Task RunAsync(double amount, DateTime transactionDate)
	{
		var reportId = transactionDate.Year;
    var month = transactionDate.Month;
    try
    {
      var report = await GetAnnualReportAsync(reportId);
      var data = GetAnnualReportData(report, month);
      data.Total -= amount;
      await _annualReportRepository.UpdateAsync(report);
    }
    catch(Exception ex)
    {
      _logger.LogError($"An error has happend. message={ex.Message}");
    }
	}

  private async Task<AnnualReport> GetAnnualReportAsync(int reportId) 
  {
    var report = await _annualReportRepository.Get(reportId);
		if (report is null)
      throw new Exception($"Annual Report not found. reportId={reportId}");
    return report;
  }

  private AnnualReportData GetAnnualReportData(AnnualReport report, int month)
  {
    var data = report.GetAnnualReportDataByMonth(month);
		if (data is null)
      throw new Exception($"Annual Report Data not found. reportId={report?.Id} month={month}");
    return data;
  }
}
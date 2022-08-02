
using FinancialControlReader.Api.Database.Repositories;
using FinancialControlReader.Api.Domain;
using MassTransit;

namespace FinancialControlReader.Api;

public record ExpenseCreatedMessage(Guid Id, string Category, double Amount, DateTime TransactionDate);

public class ExpenseCreatedConsumer : IConsumer<ExpenseCreatedMessage>
{
  readonly ILogger<ExpenseCreatedConsumer> _logger;
  readonly ICategoryReportRepository _categoryReportRepository;

  public ExpenseCreatedConsumer(ILogger<ExpenseCreatedConsumer> logger, ICategoryReportRepository categoryReportRepository)
  {
    _logger = logger;
    _categoryReportRepository = categoryReportRepository;
  }

  public async Task Consume(ConsumeContext<ExpenseCreatedMessage> context)
  {
    _logger.LogInformation($"Received Message: {context.Message}");
    var expense = context.Message;
    var categoryReportId = $"{expense.TransactionDate.Month}{expense.TransactionDate.Year}"; 

    var categoryReport = await _categoryReportRepository.GetCategoryReport(categoryReportId);
    if (categoryReport == null)
      await _categoryReportRepository.Insert(CreateCategoryReport(expense));
    else
    {
      UpdateCategoryReport(categoryReport, expense);
      await _categoryReportRepository.Update(categoryReport);
    }
  }

  private void UpdateCategoryReport(CategoryReport categoryReport, ExpenseCreatedMessage expense)
  {
    var resume = categoryReport.Resume.FirstOrDefault(x => x.Category == expense.Category);
    if (resume == null)
      categoryReport.Resume.Add(new Resume { Category = expense.Category, Total = expense.Amount });
    else
      resume.Total += expense.Amount;
  }

  private CategoryReport CreateCategoryReport(ExpenseCreatedMessage expense) =>
    new CategoryReport
    {
      Id = $"{expense.TransactionDate.Month}{expense.TransactionDate.Year}",
      Resume = new List<Resume> 
      { 
        new Resume
        {
          Category = expense.Category,
          Total = expense.Amount
        }
      }
    };
}
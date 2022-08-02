
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

  public Task Consume(ConsumeContext<ExpenseCreatedMessage> context)
  {
    _logger.LogInformation($"Received Message: {context.Message}");
    var expense = context.Message;
     
    _categoryReportRepository.Insert(new CategoryReport {
      Id = $"{expense.TransactionDate.Month}{expense.TransactionDate.Year}",
      Resume = new List<Resume> { new Resume {
        Category = expense.Category,
        Total = expense.Amount
      }}
    });

    return Task.CompletedTask;
  }
}
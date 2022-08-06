
using FinancialControlReader.Api.Domain.UseCases;
using MassTransit;

namespace FinancialControlReader.Api;

public record ExpenseCreatedMessage(Guid Id, string Category, double Amount, DateTime TransactionDate);

public class ExpenseCreatedConsumer : IConsumer<ExpenseCreatedMessage>
{
  readonly ILogger<ExpenseCreatedConsumer> _logger;
  readonly IRegisterCategoryReportUseCase _registerCategoryReportUseCase;
  readonly IRegisterAnnualReportUseCase _registerAnnualReportUseCase;

  public ExpenseCreatedConsumer(
    ILogger<ExpenseCreatedConsumer> logger,
    IRegisterCategoryReportUseCase registerCategoryReportUseCase,
    IRegisterAnnualReportUseCase registerAnnualReportUseCase)
  {
    _logger = logger;
    _registerCategoryReportUseCase = registerCategoryReportUseCase;
    _registerAnnualReportUseCase = registerAnnualReportUseCase;
  }

  public async Task Consume(ConsumeContext<ExpenseCreatedMessage> context)
  {
    _logger.LogInformation($"Received Message: {context.Message}");
    var expense = context.Message;
    var registerCategoryReportTask = _registerCategoryReportUseCase.RegisterAsync(expense.Category, expense.Amount, expense.TransactionDate);
    var registerAnnualReportTask =_registerAnnualReportUseCase.RegisterAsync(expense.Amount, expense.TransactionDate);
    await registerCategoryReportTask.WaitAsync(TimeSpan.FromMinutes(3));
    await registerAnnualReportTask.WaitAsync(TimeSpan.FromMinutes(3));
  }
}
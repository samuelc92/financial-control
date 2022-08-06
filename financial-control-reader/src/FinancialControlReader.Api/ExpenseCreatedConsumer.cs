
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
    await _registerCategoryReportUseCase.RegisterAsync(expense.Category, expense.Amount, expense.TransactionDate);
    await _registerAnnualReportUseCase.RegisterAsync(expense.Amount, expense.TransactionDate);
  }
}
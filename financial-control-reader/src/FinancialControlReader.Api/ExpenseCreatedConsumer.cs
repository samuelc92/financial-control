
using FinancialControlReader.Api.Domain.UseCases;
using MassTransit;

namespace FinancialControlReader.Api;

public record ExpenseCreatedMessage(Guid Id, string Category, double Amount, DateTime TransactionDate);

public class ExpenseCreatedConsumer : IConsumer<ExpenseCreatedMessage>
{
  readonly ILogger<ExpenseCreatedConsumer> _logger;
  readonly IRegisterCategoryReportUseCase _registerCategoryReportUseCase;

  public ExpenseCreatedConsumer(ILogger<ExpenseCreatedConsumer> logger, IRegisterCategoryReportUseCase registerCategoryReportUseCase)
  {
    _logger = logger;
    _registerCategoryReportUseCase = registerCategoryReportUseCase;
  }

  public async Task Consume(ConsumeContext<ExpenseCreatedMessage> context)
  {
    _logger.LogInformation($"Received Message: {context.Message}");
    var expense = context.Message;
    await _registerCategoryReportUseCase.RegisterAsync(expense.Category, expense.Amount, expense.TransactionDate);
  }
}
using MassTransit;
using FinancialControlReader.Api.Domain.UseCases;

namespace FinancialControlReader.Api;

public record ExpenseDeletedMessage(Guid Id, string Category, double Amount, DateTime TransactionDate);

public class ExpenseDeletedConsumer : IConsumer<ExpenseDeletedMessage>
{
  public ExpenseDeletedConsumer(ILogger<ExpenseDeletedConsumer> logger,
                                IDeductAnnualReportUseCase deductAnnualReportUseCase)
  {
    _logger = logger;
    _deductAnnualReportUseCase =  deductAnnualReportUseCase;
  }

  readonly ILogger<ExpenseDeletedConsumer> _logger;
  readonly IDeductAnnualReportUseCase _deductAnnualReportUseCase;

  public async Task Consume(ConsumeContext<ExpenseDeletedMessage> context)
  {
    _logger.LogInformation($"Received Expense Deleted Message: {context.Message}");
    var expense = context.Message;
    await _deductAnnualReportUseCase.RunAsync(expense.Amount, expense.TransactionDate);
  }
}
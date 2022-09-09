using MassTransit;
using FinancialControlReader.Api.Domain.UseCases;

namespace FinancialControlReader.Api;

public record ExpenseDeletedMessage(Guid Id, string Category, double Amount, DateTime TransactionDate);

public class ExpenseDeletedConsumer : IConsumer<ExpenseDeletedMessage>
{
  public ExpenseDeletedConsumer(ILogger<ExpenseDeletedConsumer> logger,
                                IDeductAnnualReportUseCase deductAnnualReportUseCase,
                                IDeductCategoryReportUseCase deductCategoryReportUseCase)
  {
    _logger = logger;
    _deductAnnualReportUseCase =  deductAnnualReportUseCase;
    _deductCategoryReportUseCase = deductCategoryReportUseCase;
  }

  readonly ILogger<ExpenseDeletedConsumer> _logger;
  readonly IDeductAnnualReportUseCase _deductAnnualReportUseCase;
  readonly IDeductCategoryReportUseCase _deductCategoryReportUseCase;

  public async Task Consume(ConsumeContext<ExpenseDeletedMessage> context)
  {
    _logger.LogInformation($"Received Expense Deleted Message: {context.Message}");
    var expense = context.Message;
    await _deductAnnualReportUseCase.RunAsync(expense.Amount, expense.TransactionDate);
    await _deductCategoryReportUseCase.RunAsync(expense.Category, expense.Amount, expense.TransactionDate);
  }
}
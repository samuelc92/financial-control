
using MassTransit;

namespace FinancialControlReader.Api;

public record ExpenseCreatedMessage(Guid Id, string Category);

public class ExpenseCreatedConsumer : IConsumer<ExpenseCreatedMessage>
{
  readonly ILogger<ExpenseCreatedConsumer> _logger;
  public ExpenseCreatedConsumer(ILogger<ExpenseCreatedConsumer> logger)
  {
    _logger = logger;  
  }

  public Task Consume(ConsumeContext<ExpenseCreatedMessage> context)
  {
    _logger.LogInformation($"Received Message: {context.Message}");
    return Task.CompletedTask;
  }
}
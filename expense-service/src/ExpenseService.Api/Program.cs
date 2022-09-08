using System.Text.Json.Serialization;
using ExpenseService.Api.Infrastructure.DataAccess;
using ExpenseService.Api.Ports.Events;
using ExpenseService.Api.Ports.Handlers;
using Microsoft.EntityFrameworkCore;
using Paramore.Brighter;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Brighter.Extensions.Hosting;
using Paramore.Brighter.MessagingGateway.RMQ;
using Paramore.Brighter.Outbox.Sqlite;
using Paramore.Brighter.Sqlite;
using Paramore.Brighter.Sqlite.EntityFrameworkCore;
using Paramore.Darker.AspNetCore;
using Polly;
using Polly.Registry;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
		policy =>
		{
			policy 
			.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod();
		});
});
builder.Services.AddControllers()
	.AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ExpenseDbContext>(
	builder =>
	{
		builder.UseSqlite("Filename=expenses.db;Cache=Shared");
	}
);

var retryPolicy = Policy.Handle<Exception>()
	.WaitAndRetry(new[] { TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(150) });
var circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreaker(1, TimeSpan.FromMilliseconds(500));
var retryPolicyAsync = Policy.Handle<Exception>()
	.WaitAndRetryAsync(new[] { TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(150) });
var circuitBreakerPolicyAsync = Policy.Handle<Exception>().CircuitBreakerAsync(1, TimeSpan.FromMilliseconds(500));
var policyRegistry = new PolicyRegistry()
{
	{ CommandProcessor.RETRYPOLICY, retryPolicy },
	{ CommandProcessor.CIRCUITBREAKER, circuitBreakerPolicy },
	{ CommandProcessor.RETRYPOLICYASYNC, retryPolicyAsync },
	{ CommandProcessor.CIRCUITBREAKERASYNC, circuitBreakerPolicyAsync }
};

builder.Services
	.AddBrighter(options =>
  {
		options.HandlerLifetime = ServiceLifetime.Scoped;
		options.CommandProcessorLifetime = ServiceLifetime.Scoped;
		options.MapperLifetime = ServiceLifetime.Scoped;
		options.PolicyRegistry = policyRegistry;
  })
  .UseExternalBus(new RmqProducerRegistryFactory(
    new RmqMessagingGatewayConnection
    {
      AmpqUri = new AmqpUriSpecification(new Uri("amqp://guest:guest@localhost:5672")),
      Exchange = new Exchange("financial.control.expenses")
    },
  new RmqPublication[]
	{
    new RmqPublication
    {
      Topic = new RoutingKey(nameof(ExpenseCreatedEvent)),
      MaxOutStandingMessages = 5,
      MaxOutStandingCheckIntervalMilliSeconds = 500,
      WaitForConfirmsTimeOutInMilliseconds = 1000,
      MakeChannels = OnMissingChannel.Create
    },
    new RmqPublication
    {
      Topic = new RoutingKey(nameof(ExpenseDeletedEvent)),
      MaxOutStandingMessages = 5,
      MaxOutStandingCheckIntervalMilliSeconds = 500,
      WaitForConfirmsTimeOutInMilliseconds = 1000,
      MakeChannels = OnMissingChannel.Create
    }
	}).Create())
  .UseSqliteOutbox(new SqliteConfiguration("Filename=expenses.db;Cache=Shared", "Outbox"), typeof(SqliteConnectionProvider), ServiceLifetime.Singleton)
  .UseSqliteTransactionConnectionProvider(typeof(SqliteEntityFrameworkConnectionProvider<ExpenseDbContext>), ServiceLifetime.Scoped)
  .UseOutboxSweeper()
  .AutoFromAssemblies();

builder.Services
  .AddDarker(options =>
	{
		options.HandlerLifetime = ServiceLifetime.Scoped;
		options.QueryProcessorLifetime = ServiceLifetime.Scoped;
	})
  .AddHandlersFromAssemblies(typeof(FindExpenseHandler).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseHttpsRedirection();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.CreateOutbox();

app.Run();
using System.Text.Json.Serialization;
using ExpenseService.Api.Infrastructure.DataAccess;
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

builder.Services
	.AddBrighter(options =>
  {
		options.HandlerLifetime = ServiceLifetime.Scoped;
		options.CommandProcessorLifetime = ServiceLifetime.Scoped;
		options.MapperLifetime = ServiceLifetime.Scoped;
  })
  .UseExternalBus(new RmqProducerRegistryFactory(
    new RmqMessagingGatewayConnection
    {
      AmpqUri = new AmqpUriSpecification(new Uri("amqp://guest:guest@localhost:5672")),
      Exchange = new Exchange("default")
    },
    new RmqPublication[]{
    	new RmqPublication
      {
        Topic = new RoutingKey("ExpenseCreated"),
				MaxOutStandingMessages = 5,
				MaxOutStandingCheckIntervalMilliSeconds = 500,
				WaitForConfirmsTimeOutInMilliseconds = 1000,
				MakeChannels = OnMissingChannel.Create
      }}
    ).Create()
  )
  .UseSqliteOutbox(new SqliteConfiguration("Filename=expenses.db;Cache=Shared", "Outbox"), typeof(SqliteConnectionProvider), ServiceLifetime.Singleton)
  .UseSqliteTransactionConnectionProvider(typeof(SqliteEntityFrameworkConnectionProvider<ExpenseDbContext>), ServiceLifetime.Scoped)
  .UseOutboxSweeper(options =>
  {
    options.TimerInterval = 5;
    options.MinimumMessageAge = 5000;
  })
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
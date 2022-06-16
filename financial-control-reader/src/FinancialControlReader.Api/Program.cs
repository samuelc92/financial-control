using FinancialControlReader.Api;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(options =>
  {
    options.AddConsumer<ExpenseCreatedConsumer>();
    options.UsingRabbitMq((context, cfg) =>
    {
      cfg.ConfigureEndpoints(context);
      cfg.UseRawJsonDeserializer();
    });
  }
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

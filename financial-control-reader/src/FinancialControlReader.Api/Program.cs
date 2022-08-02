using FinancialControlReader.Api;
using FinancialControlReader.Api.Database;
using FinancialControlReader.Api.Database.Repositories;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

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

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
var databaseSettings = builder.Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
builder.Services.AddSingleton<IMongoClient>(new MongoClient(databaseSettings.ConnectionString));
builder.Services.AddScoped<ICategoryReportRepository, CategoryReportRepository>();

var app = builder.Build();

app.MapGet("/reports/categories", async (string? month, string? year, ICategoryReportRepository categoryReportRepository) =>
{
  var input = String.IsNullOrEmpty(month) && String.IsNullOrEmpty(year) ? $"{DateTime.Now.Month}{DateTime.Now.Year}" : $"{month}{year}";
  return Results.Ok(await categoryReportRepository.GetCategoryReport(input));
});

app.Run();

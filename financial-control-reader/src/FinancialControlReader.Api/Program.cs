using FinancialControlReader.Api;
using FinancialControlReader.Api.Database;
using FinancialControlReader.Api.Database.Repositories;
using FinancialControlReader.Api.Domain.UseCases;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var AllowSpecificOrigins = "AllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});

builder.Services.AddMassTransit(options =>
  {
    options.AddConsumer<ExpenseCreatedConsumer>();
    options.AddConsumer<ExpenseDeletedConsumer>();
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
builder.Services.AddScoped<IAnnualReportRepository, AnnualReportRepository>();

builder.Services.AddScoped<IRegisterCategoryReportUseCase, RegisterCategoryReportUseCase>();
builder.Services.AddScoped<IRegisterAnnualReportUseCase, RegisterAnnualReportUseCase>();
builder.Services.AddScoped<IDeductAnnualReportUseCase, DeductAnnualReportUseCase>();

var app = builder.Build();

app.UseCors(AllowSpecificOrigins);

app.MapGet("/api/reports/categories", async (string? month, string? year, ICategoryReportRepository categoryReportRepository) =>
{
  var input = String.IsNullOrEmpty(month) && String.IsNullOrEmpty(year) ? $"{DateTime.Now.Month}{DateTime.Now.Year}" : $"{month}{year}";
  return Results.Ok(await categoryReportRepository.GetCategoryReport(input));
});

app.MapGet("/api/reports/annual", async (int? year, IAnnualReportRepository annualReportRepository) =>
{
  var input = year.HasValue ? year : DateTime.Now.Year;
  return Results.Ok(await annualReportRepository.Get(input.Value));
});

app.Run();

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

app.MapGet("/", async (ICategoryReportRepository categoryReportRepository) => Results.Ok(await categoryReportRepository.GetCategoryReport("72022")));

app.Run();

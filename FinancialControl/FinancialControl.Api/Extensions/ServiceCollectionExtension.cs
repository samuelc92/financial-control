using FinancialControl.Api.BankAccount.Domain.Ports.Repositories;
using FinancialControl.Api.BankAccount.Infra.Database.Repositories;
using FinancialControl.Api.Expenses.Domain.Ports.Repositories;
using FinancialControl.Api.Expenses.Infra.Database.Repositories;
using FinancialControl.Api.Expenses.Services;
using FinancialControl.Api.Expenses.Services.Interfaces;
using FinancialControl.Api.Income.Domain.Ports.Repositories;
using FinancialControl.Api.Income.Infra.Database.Repositories;
using FinancialControl.Api.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FinancialControl.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<FinanceControlDatabaseSettings>(
                configuration.GetSection(nameof(FinanceControlDatabaseSettings)));
            service.AddSingleton<IFinanceControlDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<FinanceControlDatabaseSettings>>().Value);
            var financeControlDatabaseSettings = configuration.GetSection(nameof(FinanceControlDatabaseSettings))
                .Get<FinanceControlDatabaseSettings>();
            service.AddSingleton<IMongoClient>(c => new MongoClient(financeControlDatabaseSettings.ConnectionString));
        }

        public static void AddRepository(this IServiceCollection service)
        {
            service.AddScoped<IExpenseRepository, ExpenseRepository>();
            service.AddScoped<IScheduleExpenseRepository, ScheduleExpenseRepository>();
            service.AddScoped<IAccountRepository, AccountRepository>();
        }

        public static void AddServices(this IServiceCollection service)
        {
            service.AddScoped<IExpenseService, ExpenseService>();
            service.AddScoped<IScheduleExpenseService, ScheduleExpenseService>();
            service.AddScoped<ISalaryRepository, SalaryRepository>();
        }
    }
}
using ExpenseService.Api.Infrastructure.DataAccess;
using ExpenseService.Api.Ports.Handlers;
using Microsoft.EntityFrameworkCore;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Darker.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
  .AddBrighter()
  .AutoFromAssemblies();

builder.Services
    .AddDarker(options =>
    {
        options.HandlerLifetime = ServiceLifetime.Scoped;
        options.QueryProcessorLifetime = ServiceLifetime.Scoped;
    })
    .AddHandlersFromAssemblies(typeof(FindExpenseByDescriptionHandler).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

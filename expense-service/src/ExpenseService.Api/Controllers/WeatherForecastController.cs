using ExpenseService.Api.Ports.Requests;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;

namespace ExpenseService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IAmACommandProcessor _commandProcessor;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IAmACommandProcessor commandProcessor)
    {
        _logger = logger;
        _commandProcessor = commandProcessor;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost(Name = "Test")]
    public async Task<ActionResult<AddExpense>> Post(AddExpense addExpense)
    {
        await _commandProcessor.SendAsync(addExpense);
        return Ok(addExpense);
    }
}

using MauiBlazor.Shared;
using Microsoft.AspNetCore.Mvc;

namespace MauiBlazor.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StockForecastController : ControllerBase
{
    private static readonly string[] Summaries = {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<StockForecastController> _logger;

    public StockForecastController(ILogger<StockForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetStockForecast")]
    public IList<StockForecast> Get()
    {
        _logger.LogInformation("GET WeatherForecast");
        return Enumerable.Range(1, 5).Select(index => new StockForecast
        {
            Date = DateTime.Now.AddDays(index),
            Price = Random.Shared.Next(-20, 55),
            Volume = Random.Shared.Next(-20, 55),
        })
        .ToArray();
    }
}

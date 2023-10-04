using MauiBlazor.Shared;
using MauiBlazor.UI.Core.Services;
using Microsoft.AspNetCore.Components;

namespace MauiBlazor.UI.Core.Pages;

public partial class FetchData
{
    [Inject]
    public IWeatherForecastClient Client { get; set; } = default!;

    private List<WeatherForecast>? _forecasts;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await Client.GetAsync();
            _forecasts = result.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}

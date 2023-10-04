using MauiBlazor.Shared;
using MauiBlazor.UI.Core.Services;
using Microsoft.AspNetCore.Components;

namespace MauiBlazor.UI.Core.Pages;

public partial class StockData
{
    [Inject]
    public IStockForecastClient Client { get; set; } = default!;

    private List<StockForecast>? _forecasts;

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

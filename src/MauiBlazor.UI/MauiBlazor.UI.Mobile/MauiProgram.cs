using MauiBlazor.UI.Core.Interfaces;
using MauiBlazor.UI.Core.Services;
using MauiBlazor.UI.Mobile.Services;

namespace MauiBlazor.UI.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        var httpClient = new HttpClient();
        var apiUrl = string.Empty;

#if DEBUG
        apiUrl = "http://10.0.2.2:6911";
#else
        apiUrl = "https://10.0.2.2:6900";
#endif
        builder.Services.AddScoped<IWeatherForecastClient>(_ => new WeatherForecastClient(apiUrl, httpClient));
        builder.Services.AddScoped<IStockForecastClient>(_1 => new StockForecastClient(apiUrl, httpClient));
        builder.Services.AddScoped<IPlatformService, PlatformService>();

        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

        return builder.Build();
    }
}

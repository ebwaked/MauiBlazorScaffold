using MauiBlazor.UI.Core.Auth0;
using MauiBlazor.UI.Core.Interfaces;
using MauiBlazor.UI.Core.Services;
using MauiBlazor.UI.Mobile.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

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
        builder.Logging.AddDebug();
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

        // App Specific Auth Code
        // https://auth0.com/blog/add-authentication-to-dotnet-maui-apps-with-auth0/#Create-the-Auth0-client
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton(new Auth0Client(new()
        {
          Domain = "<YOUR_AUTH0_DOMAIN>",
          ClientId = "<YOUR_CLIENT_ID>",
          Scope = "openid profile",
          RedirectUri = "myapp://callback"
        }));
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();
        //

        return builder.Build();
    }
}

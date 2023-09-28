﻿using MauiBlazor.UI.Core.Interfaces;
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
        // The following removes cert validation for TESTING only 
        //var handler = new HttpClientHandler();
        //handler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;
        //var httpClient = new HttpClient(handler);

        var httpClient = new HttpClient();

        builder.Services.AddScoped<IWeatherForecastClient>(_ => new WeatherForecastClient("https://10.0.2.2:5000", httpClient));
        builder.Services.AddScoped<IPlatformService, PlatformService>();


        return builder.Build();
    }
}

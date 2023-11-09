using MauiBlazor.UI.Core.Auth0;
using MauiBlazor.UI.Core.Interfaces;
using MauiBlazor.UI.Core.Services;
using MauiBlazor.UI.Web;
using MauiBlazor.UI.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Xamarin.Essentials;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(_ => httpClient);
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IWeatherForecastClient>(_ => new WeatherForecastClient(builder.Configuration["ApiUrl"], httpClient));

// App Specific Auth Code
// https://auth0.com/blog/add-authentication-to-dotnet-maui-apps-with-auth0/#Create-the-Auth0-client
builder.Services.AddSingleton<App>();

builder.Services.AddSingleton(new Auth0Client(new()
{
    Domain = "dev-ffzp0aihd2m0va0s.us.auth0.com",
    ClientId = "ZlOfHLNbk9oZCPOxFyXkPI68wnhhEVgl",
    Scope = "openid profile",
    RedirectUri = "https://localhost:7072/callback"
}));
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();
//

await builder.Build().RunAsync();

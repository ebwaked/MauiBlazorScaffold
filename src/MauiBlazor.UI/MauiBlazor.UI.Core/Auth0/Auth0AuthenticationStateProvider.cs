using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace MauiBlazor.UI.Core.Auth0;
public class Auth0AuthenticationStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly Auth0Client auth0Client;

    public Auth0AuthenticationStateProvider(Auth0Client client) {
        auth0Client = client;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(new AuthenticationState(currentUser));

    public Task LogInAsync()
    {
        var loginTask = LogInAsyncCore();
        NotifyAuthenticationStateChanged(loginTask);

        return loginTask;

        async Task<AuthenticationState> LogInAsyncCore()
        {
            var user = await LoginWithAuth0Async();
            currentUser = user;

            return new AuthenticationState(currentUser);
        }
    }

    private async Task<ClaimsPrincipal> LoginWithAuth0Async()
    {
        LoginResult loginResult = new LoginResult();
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());
        try
        {
            loginResult = await auth0Client.LoginAsync();
        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.Message.ToString()); 
        }

        if (!loginResult.IsError) {
          authenticatedUser = loginResult.User;
        }
        return authenticatedUser;
    }

    public async void LogOut()
    {
        await auth0Client.LogoutAsync();
        currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentUser)));
    }
}

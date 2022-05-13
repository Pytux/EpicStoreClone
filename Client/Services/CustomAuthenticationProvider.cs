using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorStore.Client.Services;

public class CustomAuthenticationProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;

    public CustomAuthenticationProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorageService.GetItem<string>("token");

        if (string.IsNullOrEmpty(token))
        {
            var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            return anonymous;
        }

        var userClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "JWT"));
        var loginUser = new AuthenticationState(userClaimPrincipal);
        return loginUser;
    }

    public void Notify()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
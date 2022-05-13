using Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorStore.Client.Services;

public class AuthenticationService : IAutheticationService
{
    private readonly AuthenticationStateProvider authenticationStateProvider;
    private readonly IHttpService httpService;
    private readonly ILocalStorageService localStorageService;
    private NavigationManager navigationManager;

    public AuthenticationService(
        IHttpService httpService,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService,
        AuthenticationStateProvider authenticationStateProvider
    )
    {
        this.httpService = httpService;
        this.navigationManager = navigationManager;
        this.localStorageService = localStorageService;
        this.authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> Login(UserLogin info)
    {
        try
        {
            var token = await httpService.PostString("login", info);
            await localStorageService.SetItem("token", token);
            (authenticationStateProvider as CustomAuthenticationProvider).Notify();
            return true;
        }
        catch (Exception)
        {
        }

        return false;
    }

    public async Task Logout()
    {
        await localStorageService.RemoveItem("token");
        (authenticationStateProvider as CustomAuthenticationProvider).Notify();
    }
}
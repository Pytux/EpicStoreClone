using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorStore.Client;

public class HttpService : IHttpService
{
    private readonly HttpClient httpClient;
    private readonly ILocalStorageService localStorageService;
    private IConfiguration configuration;
    private NavigationManager navigationManager;

    public HttpService(
        HttpClient httpClient,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService,
        IConfiguration configuration
    )
    {
        this.httpClient = httpClient;
        this.navigationManager = navigationManager;
        this.localStorageService = localStorageService;
        this.configuration = configuration;
    }

    public async Task<T> Get<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }

    public async Task<T> Post<T>(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await SendRequest<T>(request);
    }

    public async Task<string> PostString(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await SendRequestString(request);
    }

    public async Task<string> PutString(string uri, object value)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri);
        request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return await SendRequestString(request);
    }

    // helper methods

    private async Task<string> SendRequestString(HttpRequestMessage request)
    {
        await SetHeadersAsync(request);

        var response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            // var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception();

        return await response.Content.ReadAsStringAsync();
    }

    private async Task<T> SendRequest<T>(HttpRequestMessage request)
    {
        await SetHeadersAsync(request);

        var response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            // var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception();

        return await response.Content.ReadFromJsonAsync<T>();
    }

    private async Task SetHeadersAsync(HttpRequestMessage request)
    {
        var token = await localStorageService.GetItem<string>("token");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
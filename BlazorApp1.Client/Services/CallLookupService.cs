using BlazorApp1.Client.Models;
using System.Net.Http.Json;

namespace BlazorApp1.Client.Services;

public class CallLookupService : ICallLookupService
{
    private readonly HttpClient _httpClient;

    public CallLookupService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CallInfo?> LookupByNumberAsync(string number)
    {
        return await _httpClient.GetFromJsonAsync<CallInfo>($"call-lookup/number/{number}");
    }

    public async Task<CallInfo?> LookupByExtensionAsync(string extension)
    {
        return await _httpClient.GetFromJsonAsync<CallInfo>($"call-lookup/extension/{extension}");
    }
}

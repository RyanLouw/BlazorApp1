using BlazorApp1.Client.Models;

namespace BlazorApp1.Client.Services;

public class CallLookupService : ICallLookupService
{
    private readonly HttpClient _httpClient;

    public CallLookupService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<CallInfo?> LookupByNumberAsync(string number)
    {
        // TODO: implement API call
        return Task.FromResult<CallInfo?>(null);
    }

    public Task<CallInfo?> LookupByExtensionAsync(string extension)
    {
        // TODO: implement API call
        return Task.FromResult<CallInfo?>(null);
    }
}

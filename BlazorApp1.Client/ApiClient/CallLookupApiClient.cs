using BlazorApp1.Client.ApiClient.Interface;
using BlazorApp1.Client.Models;
using System.Net.Http.Json;

namespace BlazorApp1.Client.ApiClient;

public class CallLookupApiClient : ICallLookupApiClient
{
    private readonly HttpClient _http;

    public CallLookupApiClient(HttpClient http) => _http = http;

    public async Task<List<CallInfo>> LookupByNumberAsync(string number, DateTime start, DateTime end)
    {
        if (number !="")
        {
         var payload = new { FromDate = start, ToDate = end, PhoneNumbers = number ?? string.Empty };

         var res = await _http.PostAsJsonAsync("api/call/by-numbers", payload);
         if (!res.IsSuccessStatusCode) return new List<CallInfo>();
         return await res.Content.ReadFromJsonAsync<List<CallInfo>>() ?? new List<CallInfo>();
        }
        else return new List<CallInfo>();

    }

    public async Task<List<CallInfo>> LookupByExtensionAsync(string extension, string callType, DateTime date)
    {
        var payload = new { Extension = extension, CallType = callType, Date = date };

        var res = await _http.PostAsJsonAsync("api/call/by-extension", payload);
        if (!res.IsSuccessStatusCode) return new List<CallInfo>();
        return await res.Content.ReadFromJsonAsync<List<CallInfo>>() ?? new List<CallInfo>();
    }

    public async Task<string?> GetDownloadUrlAsync(string recordingId)
    {
        if (string.IsNullOrWhiteSpace(recordingId)) return null;

        var res = await _http.GetAsync($"api/call/call-url/{Uri.EscapeDataString(recordingId)}");
        if (!res.IsSuccessStatusCode) return null;

        var text = await res.Content.ReadAsStringAsync();
        return text?.Trim().Trim('"');
    }
}

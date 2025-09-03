using BlazorApp1.Client.Models;
using System.Net.Http.Json;

namespace BlazorApp1.Manager;

public class CallLookupService : ICallLookupService
{
    private readonly HttpClient _http;

    public CallLookupService(HttpClient http) => _http = http;

    public async Task<List<CallInfo>> LookupByNumberAsync(string number, DateTime start, DateTime end)
    {
        var payload = new
        {
            FromDate = start.ToString("yyyy-MM-ddTHH:mm:ss"),
            ToDate = end.ToString("yyyy-MM-ddTHH:mm:ss"),
            PhoneNumbers = number ?? string.Empty
        };

        using var res = await _http.PostAsJsonAsync("by-numbers", payload);
       
        if (!res.IsSuccessStatusCode)
            return new List<CallInfo>();

        return await res.Content.ReadFromJsonAsync<List<CallInfo>>()
               ?? new List<CallInfo>();
    }

    public async Task<List<CallInfo>> LookupByExtensionAsync(string extension, string callType, DateTime date)
    {
        var payload = new
        {
            extension,
            callType, // "Incoming" | "Outgoing" | "Both"
            date = date.ToString("yyyy-MM-ddTHH:mm:ss")
        };

        using var res = await _http.PostAsJsonAsync("by-extension", payload);
        if (!res.IsSuccessStatusCode)
            return new List<CallInfo>();

        return await res.Content.ReadFromJsonAsync<List<CallInfo>>()
               ?? new List<CallInfo>();
    }

    public async Task<string?> GetDownloadUrlAsync(string recordingId)
    {
        if (string.IsNullOrWhiteSpace(recordingId))
            return null;

        using var res = await _http.GetAsync($"call-url/{Uri.EscapeDataString(recordingId)}");
        if (!res.IsSuccessStatusCode)
            return null;

        var text = await res.Content.ReadAsStringAsync();
        return text?.Trim().Trim('"'); 
    }

}

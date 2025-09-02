using BlazorApp1.Client.Models;

namespace BlazorApp1.Client.ApiClient.Interface;

public interface ICallLookupApiClient
{
    Task<List<CallInfo>> LookupByNumberAsync(string number, DateTime start, DateTime end);
    Task<List<CallInfo>> LookupByExtensionAsync(string extension, string callType, DateTime date);
    Task<string?> GetDownloadUrlAsync(string recordingId);
}

using BlazorApp1.Client.Models;

namespace BlazorApp1.Manager;

public interface ICallLookupService
{
    Task<List<CallInfo>> LookupByNumberAsync(string number, DateTime start, DateTime end);
    Task<List<CallInfo>> LookupByExtensionAsync(string extension, string callType, DateTime date);
    Task<string?> GetDownloadUrlAsync(string recordingId);
}

using BlazorApp1.Client.Models;

namespace BlazorApp1.Client.Services;

public interface ICallLookupService
{
    Task<CallInfo?> LookupByNumberAsync(string number);
    Task<CallInfo?> LookupByExtensionAsync(string extension);
}

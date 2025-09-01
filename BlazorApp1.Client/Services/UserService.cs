using System.Net.Http.Json;
using BlazorApp1.Client.Models;

namespace BlazorApp1.Client.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("users") ?? Array.Empty<User>();
    }
}

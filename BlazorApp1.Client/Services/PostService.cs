using System.Net.Http.Json;
using BlazorApp1.Client.Models;

namespace BlazorApp1.Client.Services;

public class PostService : IPostService
{
    private readonly HttpClient _httpClient;

    public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Post>>("posts") ?? Array.Empty<Post>();
    }
}

using BlazorApp1.Client.Models;

namespace BlazorApp1.Client.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPostsAsync();
}

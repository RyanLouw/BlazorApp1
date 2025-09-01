using BlazorApp1.Client.Models;
using BlazorApp1.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Client.Pages;

public partial class Posts : ComponentBase
{
    [Inject]
    public IPostService PostService { get; set; } = default!;

    private IEnumerable<Post>? _posts;

    protected override async Task OnInitializedAsync()
    {
        _posts = await PostService.GetPostsAsync();
    }
}

using BlazorApp1.Client.Models;
using BlazorApp1.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Client.Pages;

public partial class Users : ComponentBase
{
    [Inject]
    public IUserService UserService { get; set; } = default!;

    private IEnumerable<User>? _users;

    protected override async Task OnInitializedAsync()
    {
        _users = await UserService.GetUsersAsync();
    }
}

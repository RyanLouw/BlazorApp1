using BlazorApp1.Client.Models;

namespace BlazorApp1.Client.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
}

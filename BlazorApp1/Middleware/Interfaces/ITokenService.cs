namespace BlazorApp1.Middleware.Interfaces;

public interface ITokenService
{
    Task<string> GetTokenAsync();
}

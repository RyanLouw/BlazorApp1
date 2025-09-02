using BlazorApp1.Middleware.Interfaces;
using System.Net.Http.Headers;

namespace BlazorApp1.Middleware;

public sealed class TokenMiddleware : DelegatingHandler
{
    private readonly ITokenService _tokenService;
    public TokenMiddleware(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetTokenAsync();

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}

using BlazorApp1.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://api.example.com/")
});

builder.Services.AddScoped<ICallLookupService, CallLookupService>();

await builder.Build().RunAsync();

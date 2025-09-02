using BlazorApp1.Client.ApiClient;
using BlazorApp1.Client.ApiClient.Interface;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// ❌ REMOVE these for Blazor Web App:
// builder.RootComponents.Add<App>("#app");
// builder.RootComponents.Add<HeadOutlet>("head::after");

// Keep DI registrations
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICallLookupApiClient, CallLookupApiClient>();

await builder.Build().RunAsync();

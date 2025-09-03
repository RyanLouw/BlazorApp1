using BlazorApp1.Client.ApiClient;
using BlazorApp1.Client.ApiClient.Interface;
using BlazorApp1.Client.Pages;
using BlazorApp1.Components;
using BlazorApp1.Manager;
using BlazorApp1.Middleware;
using BlazorApp1.Middleware.Interfaces;
using HW.CentralConfig.Package.Core;
using System.Runtime.CompilerServices;


var builder = WebApplication.CreateBuilder(args);
await builder.AddCentralConfigAsync();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();


builder.Services.AddHttpClient<ICallLookupService, CallLookupService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"] ?? "");
}).AddHttpMessageHandler<TokenMiddleware>();

builder.Services.AddSingleton<TokenMiddleware>();
builder.Services.AddHttpClient<TokenService>(client => client.BaseAddress = new Uri(builder.Configuration["CentralConfig:BaseUrl"] ?? ""));
builder.Services.AddSingleton<ITokenService>(sp => sp.GetRequiredService<TokenService>());
builder.Services.AddScoped<ICallLookupApiClient, CallLookupApiClient>();


builder.Services.AddMemoryCache();








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp1.Client._Imports).Assembly);

app.Run();

using BlazorApp1.Client.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Client.Pages;

public partial class CallLookupByNumber : ComponentBase
{
    [Inject]
    protected ICallLookupService CallLookupService { get; set; } = default!;
}

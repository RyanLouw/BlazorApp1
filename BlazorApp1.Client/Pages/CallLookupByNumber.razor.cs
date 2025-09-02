using BlazorApp1.Client.ApiClient.Interface;
using BlazorApp1.Client.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Client.Pages;

public partial class CallLookupByNumber : ComponentBase
{
    [Inject] protected ICallLookupApiClient CallApi { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    protected DateTime? StartDate { get; set; } = DateTime.Today.AddDays(-7);
    protected DateTime? EndDate { get; set; } = DateTime.Today;
    protected string? Number { get; set; }

    protected List<CallInfo> Results { get; set; } = new();
    protected bool IsLoading { get; set; }


    protected bool CanSearch => !string.IsNullOrWhiteSpace(Number);

    private Task SearchNow() => SearchAsync(true);

    protected async Task SearchAsync(bool resetPage)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                Results.Clear();
                return;
            }

            IsLoading = true;
            Results = await CallApi.LookupByNumberAsync(
                Number,
                StartDate ?? DateTime.MinValue,
                EndDate ?? DateTime.MaxValue);
        }
        finally
        {
            IsLoading = false;
            StateHasChanged();
        }
    }

    protected Task DownloadAsync(CallInfo r) => Task.CompletedTask;
}

using BlazorApp1.Client.ApiClient.Interface;
using BlazorApp1.Client.Models;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

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

    protected bool CanSearch =>
        !string.IsNullOrWhiteSpace(Number) &&
        StartDate is not null &&
        EndDate is not null &&
        StartDate <= EndDate;

    private Task SearchNow() => SearchAsync();

    protected async Task SearchAsync()
    {
        if (IsLoading) return;

        // Normalize inputs
        var number = NormalizeNumber(Number);
        if (string.IsNullOrWhiteSpace(number))
        {
            Results.Clear();
            StateHasChanged();
            return;
        }

        if (StartDate is null || EndDate is null || StartDate > EndDate)
        {
            // Could show a snackbar/toast here
            return;
        }

        // Make EndDate inclusive (23:59:59.9999999)
        var from = StartDate.Value.Date;
        var to = EndDate.Value.Date.AddDays(1).AddTicks(-1);

        try
        {
            IsLoading = true;
            Results = await CallApi.LookupByNumberAsync(number, from, to);
        }
        finally
        {
            IsLoading = false;
            StateHasChanged();
        }
    }

    // Example: strip spaces, dashes, parentheses, etc.
    private static string NormalizeNumber(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var trimmed = input.Trim();
        // Keep digits and leading '+' (if present)
        if (trimmed.StartsWith("+"))
            return "+" + Regex.Replace(trimmed[1..], "[^0-9]", "");
        return Regex.Replace(trimmed, "[^0-9]", "");
    }

    protected Task DownloadAsync(CallInfo r) => Task.CompletedTask;
}

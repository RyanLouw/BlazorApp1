// BlazorApp1/Controllers/CallController.cs
using BlazorApp1.Client.Models;
using BlazorApp1.Manager;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Controllers;

[ApiController]
[Route("api/call")]
[IgnoreAntiforgeryToken]
public class CallController : ControllerBase
{
    private readonly ICallLookupService _svc;
    public CallController(ICallLookupService svc) => _svc = svc;

    public record ByNumbersRequest(DateTime FromDate, DateTime ToDate, string PhoneNumbers);
    public record ByExtensionRequest(string Extension, string CallType, DateTime Date);

    [HttpPost("by-numbers")]
    public async Task<ActionResult<List<CallInfo>>> ByNumbers([FromBody] ByNumbersRequest req)
    {
        var list = await _svc.LookupByNumberAsync(req.PhoneNumbers, req.FromDate, req.ToDate);
        return Ok(list);
    }

    [HttpPost("by-extension")]
    public async Task<ActionResult<List<CallInfo>>> ByExtension([FromBody] ByExtensionRequest req)
    {
        var list = await _svc.LookupByExtensionAsync(req.Extension, req.CallType, req.Date);
        return Ok(list);
    }

    [HttpGet("call-url/{recordingId}")]
    public async Task<ActionResult<string>> GetCallUrl([FromRoute] string recordingId)
    {
        var url = await _svc.GetDownloadUrlAsync(recordingId);
        if (string.IsNullOrWhiteSpace(url)) return NotFound();
        return Ok(url);
    }
}

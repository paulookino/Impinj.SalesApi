using Impinj.Core.Models;
using Impinj.Services;
using Microsoft.AspNetCore.Mvc;

namespace Impinj.SalesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly SalesAnalysisService _service;

    public SalesController(SalesAnalysisService service)
    {
        _service = service;
    }

    [HttpGet("summary")]
    public ActionResult<SalesSummary> GetSummary()
    {
        var summary = _service.Analyze();
        return Ok(summary);
    }
}

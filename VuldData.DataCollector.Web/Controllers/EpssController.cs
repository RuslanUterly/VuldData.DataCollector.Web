using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VulnData.DataCollector.Application.Interfaces;

namespace VuldData.DataCollector.Web.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize]
public class EpssController : ControllerBase
{
    private readonly IEpssService _epssService;

    public EpssController(IEpssService epssService)
    {
        _epssService = epssService;
    }

    [HttpPost]
    public async Task<IActionResult> LoadEpssMetrics()
    {
        var result = await _epssService.LoadEpssToDbScopesAsync();
        
        return Ok(result);
    }
}
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using VulnData.DataCollector.Application.Interfaces;

namespace VuldData.DataCollector.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BduController : ControllerBase
{
    private readonly IBduService _bduService;
    
    public BduController(IBduService bduService)
    {
        _bduService = bduService;
    }

    [HttpPost]
    public async Task<IActionResult> LoadDbAsync()
    {
        var result = await _bduService.LoadBduToDbScopesAsync();
        
        return Ok(result);
    }
}
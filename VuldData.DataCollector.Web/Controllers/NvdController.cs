using Microsoft.AspNetCore.Mvc;
using VulnData.DataCollector.Application.Interfaces;

namespace VuldData.DataCollector.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class NvdController : ControllerBase
{
    private readonly INvdService _nvdService;

    public NvdController(INvdService nvdService)
    {
        _nvdService = nvdService;
    }
    
    [HttpPost]
    public async Task<IActionResult> GetNvdScopesAsync()
    {
        var result = await _nvdService.LoadNvdToDbScopesAsync();
        
        return Ok(result);
    }
}
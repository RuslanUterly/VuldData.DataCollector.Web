using VulnData.DataCollector.Application.Dtos.Nvd;

namespace VulnData.DataCollector.Application.Interfaces;

public interface INvdService
{
    Task<bool> LoadNvdToDbScopesAsync();
}
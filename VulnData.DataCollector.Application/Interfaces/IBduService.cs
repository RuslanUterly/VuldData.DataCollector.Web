using VulnData.DataCollector.Infrastructure.Models;
using VulnData.DataCollector.Infrastructure.Models.Bdu;

namespace VulnData.DataCollector.Application.Interfaces;

public interface IBduService
{
    Task<bool> LoadBduToDbScopesAsync();
}
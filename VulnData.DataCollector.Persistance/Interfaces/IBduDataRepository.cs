using VulnData.DataCollector.Infrastructure.Models;
using VulnData.DataCollector.Infrastructure.Models.Bdu;

namespace VulnData.DataCollector.Infrastructure.Interfaces;

public interface IBduDataRepository
{
    Task<bool> AddRange(List<BduData> bduData);
}
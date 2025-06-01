using VulnData.DataCollector.Infrastructure.Models.Epss;

namespace VulnData.DataCollector.Infrastructure.Interfaces;

public interface IEpssDataRepository
{
    Task<bool> AddRange(List<EpssData> epssData);
}
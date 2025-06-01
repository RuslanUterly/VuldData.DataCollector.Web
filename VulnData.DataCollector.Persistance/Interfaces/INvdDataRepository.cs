using VulnData.DataCollector.Persistance.Models.Nvd;

namespace VulnData.DataCollector.Infrastructure.Interfaces;

public interface INvdDataRepository
{
    Task<bool> AddRange(List<NvdData> NvdData);
}
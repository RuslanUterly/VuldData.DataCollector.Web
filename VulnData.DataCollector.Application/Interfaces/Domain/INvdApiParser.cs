using VulnData.DataCollector.Persistance.Models.Nvd;

namespace VulnData.DataCollector.Application.Interfaces.Domain;

public interface INvdApiParser
{
    Task<List<NvdData>> ParseDataAsync();
}
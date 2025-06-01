
using VulnData.DataCollector.Application.Dtos;
using EpssData = VulnData.DataCollector.Infrastructure.Models.Epss.EpssData;

namespace VulnData.DataCollector.Application.Interfaces;

public interface IEpssService
{
    Task<bool> LoadEpssToDbScopesAsync();
}
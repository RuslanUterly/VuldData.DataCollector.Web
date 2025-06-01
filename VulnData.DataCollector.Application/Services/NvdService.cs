using Microsoft.Extensions.Options;
using VulnData.DataCollector.Application.Dtos.Nvd;
using VulnData.DataCollector.Application.Interfaces;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using Polly;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Application.Mapping;
using VulnData.DataCollector.Infrastructure.Interfaces;

namespace VulnData.DataCollector.Application.Services;

public class NvdService : INvdService
{
    private readonly INvdApiParser _apiParser;
    private readonly INvdDataRepository _nvdDataRepository;

    public NvdService(INvdApiParser apiParser, INvdDataRepository nvdDataRepository)
    {
        _apiParser = apiParser;
        _nvdDataRepository = nvdDataRepository;
    }
    
    public async Task<bool> LoadNvdToDbScopesAsync()
    {
        var allVulnerabilities = await _apiParser.ParseDataAsync();
        
        if (allVulnerabilities == null || allVulnerabilities.Count == 0)
            throw new Exception("fail to load vulnlist");

        var result = await _nvdDataRepository.AddRange(allVulnerabilities);
        return result;
    }
}
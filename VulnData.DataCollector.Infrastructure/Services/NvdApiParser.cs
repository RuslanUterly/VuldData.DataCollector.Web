using VulnData.DataCollector.Application.Dtos.Nvd;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Application.Mapping;
using NvdData = VulnData.DataCollector.Persistance.Models.Nvd.NvdData;

namespace VulnData.DataCollector.Domain.Services;

public class NvdApiParser : INvdApiParser
{
    private readonly IVulnerabilityFetcher _vulnerabilityFetcher;
    
    public NvdApiParser(IVulnerabilityFetcher vulnerabilityFetcher)
    {
        _vulnerabilityFetcher = vulnerabilityFetcher;
    }
    
    public async Task<List<NvdData>> ParseDataAsync()
    {
        var totalVulnerabilities = await _vulnerabilityFetcher.GetVulnCountAsync();
        const int maxResultsPerRequest = 2000;
        const int maxConcurrentRequests = 50;
        
        var semaphore = new SemaphoreSlim(maxConcurrentRequests);
        var tasks = new List<Task>();
        var allVulnerabilities = new List<Vulnerability>();
        
        for (int startIndex = 0; startIndex < totalVulnerabilities; startIndex += maxResultsPerRequest)
        {
            await semaphore.WaitAsync();

            var currentStartIndex = startIndex;

            var task = Task.Run(async () =>
            {
                try
                {
                    var batch = await _vulnerabilityFetcher.GetDataAsync(currentStartIndex, maxResultsPerRequest);
                    allVulnerabilities.AddRange(batch);
                }
                finally
                {
                    semaphore.Release();
                }
            });

            tasks.Add(task);
        }

        await Task.WhenAll(tasks);
        
        return allVulnerabilities.ToNvdData();
    }
}
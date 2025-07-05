using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using VulnData.DataCollector.Infrastructure.Interfaces;
using VulnData.DataCollector.Infrastructure.Models.Epss;

namespace VulnData.DataCollector.Domain.Persistance.DataAccess;

public class EpssDataRepository : IEpssDataRepository
{
    private readonly IDbContextFactory<VulnContext> _contextFactory;
    // private readonly VulnContext _context;

    public EpssDataRepository(IDbContextFactory<VulnContext> contextFactory)
    {
        _contextFactory = contextFactory;
        // _context = context;
    }
    
    public async Task<bool> AddRange(List<EpssData> epssData)
    {
        if (epssData == null || epssData.Count == 0)
            return false;
        
        await using var context = await _contextFactory.CreateDbContextAsync();
        
        await context.BulkInsertOrUpdateAsync(epssData);
        return true;
    }
}
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using VulnData.DataCollector.Infrastructure.Interfaces;
using VulnData.DataCollector.Persistance.Models.Nvd;

namespace VulnData.DataCollector.Domain.Persistance.DataAccess;

public class NvdDataRepository : INvdDataRepository
{
    private readonly IDbContextFactory<VulnContext> _contextFactory;
    // private readonly VulnContext _context;

    public NvdDataRepository(IDbContextFactory<VulnContext> contextFactory)
    {
        _contextFactory = contextFactory;
        // _context = context;
    }
    
    public async Task<bool> AddRange(List<NvdData> nvdData)
    {
        if (nvdData == null || nvdData.Count == 0)
            return false;
        
        await using var context = await _contextFactory.CreateDbContextAsync();
        
        await context.BulkInsertOrUpdateAsync(nvdData);
        return true;
    }
}
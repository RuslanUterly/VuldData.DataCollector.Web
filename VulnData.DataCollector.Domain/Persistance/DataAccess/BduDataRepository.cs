using System.Data;
using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VulnData.DataCollector.Infrastructure.Interfaces;
using VulnData.DataCollector.Infrastructure.Models;
using VulnData.DataCollector.Infrastructure.Models.Bdu;

namespace VulnData.DataCollector.Domain.Persistance.DataAccess;

public class BduDataRepository : IBduDataRepository
{
    private readonly IDbContextFactory<VulnContext> _contextFactory;
    // private readonly VulnContext _context;

    public BduDataRepository(IDbContextFactory<VulnContext> contextFactory)
    {
        // _context = context;
        _contextFactory = contextFactory;
    }

    public async Task<bool> AddRange(List<BduData> bduData)
    {
        if (bduData == null || bduData.Count == 0)
            return false;
        
        await using var context = await _contextFactory.CreateDbContextAsync();
        
        await context.BulkInsertOrUpdateAsync(bduData);
        return true;
    }
}
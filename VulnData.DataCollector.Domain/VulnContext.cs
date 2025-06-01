using Microsoft.EntityFrameworkCore;
using VulnData.DataCollector.Domain.Persistance.Configurations;
using VulnData.DataCollector.Infrastructure.Data;
using VulnData.DataCollector.Infrastructure.Models.Bdu;
using VulnData.DataCollector.Infrastructure.Models.Epss;
using VulnData.DataCollector.Persistance.Models.Nvd;

namespace VulnData.DataCollector.Domain;

public class VulnContext(DbContextOptions<VulnContext> options) : DbContext(options), IVulnContext
{
    public DbSet<BduData> BduData { get; set; }
    public DbSet<EpssData> EpssData { get; set; }
    public DbSet<NvdData> NvdData { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EpssDataConfiguration());
        modelBuilder.ApplyConfiguration(new BduDataConfiguration());
        modelBuilder.ApplyConfiguration(new NvdDataConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
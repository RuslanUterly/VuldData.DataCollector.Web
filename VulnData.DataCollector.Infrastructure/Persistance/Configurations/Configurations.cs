using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VulnData.DataCollector.Infrastructure.Models;
using VulnData.DataCollector.Infrastructure.Models.Bdu;
using VulnData.DataCollector.Infrastructure.Models.Epss;
using VulnData.DataCollector.Persistance.Models.Nvd;

namespace VulnData.DataCollector.Domain.Persistance.Configurations;

public class BduDataConfiguration : IEntityTypeConfiguration<BduData>
{
    public void Configure(EntityTypeBuilder<BduData> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id).HasMaxLength(14);
        builder.Property(b => b.Name).HasMaxLength(1000);
        builder.Property(b => b.Description).HasMaxLength(1300);
        builder.Property(b => b.System).HasMaxLength(32800);
        builder.Property(b => b.Fixes).HasMaxLength(32800);
        builder.Property(b => b.Links).HasMaxLength(13500);
        builder.Property(b => b.VulnClass).HasMaxLength(30);
        builder.Property(b => b.Status).HasMaxLength(40);
        builder.Property(b => b.OtherId).HasMaxLength(600);
        builder.Property(b => b.OtherInfo).HasMaxLength(2000);
        builder.Property(b => b.WayToDestroy).HasMaxLength(120);
        builder.Property(b => b.WayToFix).HasMaxLength(40);
        
        builder.OwnsOne(b => b.Vendor, vendor =>
        {
            vendor.Property(v => v.Type).HasMaxLength(450);
            vendor.Property(v => v.Version).HasMaxLength(32800);
            vendor.Property(v => v.Name).HasMaxLength(14500);
            vendor.Property(v => v.Title).HasMaxLength(350);
        });

        builder.OwnsOne(b => b.Cwe, cwe =>
        {
            cwe.Property(c => c.Type).HasMaxLength(60);
            cwe.Property(c => c.Description).HasMaxLength(330);
        });
    }
}

public class EpssDataConfiguration : IEntityTypeConfiguration<EpssData>
{
    public void Configure(EntityTypeBuilder<EpssData> builder)
    {
        builder.HasKey(b => b.Cve);
        
        builder.Property(b => b.Cve).HasMaxLength(16);
    }
}

public class NvdDataConfiguration : IEntityTypeConfiguration<NvdData>
{
    public void Configure(EntityTypeBuilder<NvdData> builder)
    {
        builder.HasKey(b => b.Cve);
        
        builder.Property(b => b.Cve).HasMaxLength(16);
    }
}
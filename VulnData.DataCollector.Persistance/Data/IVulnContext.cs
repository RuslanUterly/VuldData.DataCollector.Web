namespace VulnData.DataCollector.Infrastructure.Data;

public interface IVulnContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
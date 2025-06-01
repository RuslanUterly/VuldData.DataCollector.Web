namespace VulnData.DataCollector.Infrastructure.Models.Epss;

public class EpssData
{
    public string? Cve  { get; set; }
    public double? Epss { get; set; }
    public double? Percentile { get; set; }
}
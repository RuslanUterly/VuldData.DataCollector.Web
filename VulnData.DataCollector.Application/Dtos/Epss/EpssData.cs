namespace VulnData.DataCollector.Application.Dtos;

public class EpssData
{
    public string? Cve  { get; set; }
    public double? Epss { get; set; }
    public double? Percentile { get; set; }
}
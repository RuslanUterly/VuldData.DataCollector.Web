namespace VulnData.DataCollector.Application.Dtos.Nvd;

public class CvssMetricV31
{
    public string Source { get; set; }
    public string Type { get; set; }
    public CvssData CvssData { get; set; }
    public double ExploitabilityScore { get; set; }
    public double ImpactScore { get; set; }
}
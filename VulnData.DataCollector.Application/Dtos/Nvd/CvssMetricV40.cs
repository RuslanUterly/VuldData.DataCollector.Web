namespace VulnData.DataCollector.Application.Dtos.Nvd;

public class CvssMetricV40
{
    public string Source { get; set; }
    public string Type { get; set; }
    public CvssData CvssData { get; set; }
}
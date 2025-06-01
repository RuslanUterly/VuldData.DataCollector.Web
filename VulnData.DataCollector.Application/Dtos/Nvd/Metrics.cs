namespace VulnData.DataCollector.Application.Dtos.Nvd;

public class Metrics
{
    public List<CvssMetricV31> CvssMetricV31 { get; set; }
    public List<CvssMetricV40> CvssMetricV40 { get; set; }
    public List<CvssMetricV2> CvssMetricV2 { get; set; }
}
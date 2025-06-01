namespace VulnData.DataCollector.Application.Dtos.Nvd;

public class Reference
{
    public string Url { get; set; }
    public string Source { get; set; }
    public List<string> Tags { get; set; }
}
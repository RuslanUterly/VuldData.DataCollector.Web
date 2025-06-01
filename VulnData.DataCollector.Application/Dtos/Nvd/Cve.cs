namespace VulnData.DataCollector.Application.Dtos.Nvd;

public class Cve
{
    public string Id { get; set; }
    public string SourceIdentifier  { get; set; }
    public DateTime Published { get; set; }
    public DateTime LastModified { get; set; }
    public List<Description> Descriptions { get; set; }
    public Metrics Metrics { get; set; }
    public List<Weakness> Weaknesses { get; set; }
    public List<Reference> References { get; set; }
    public string CisaExploitAdd { get; set; }
    public string CisaActionDue { get; set; }
    public string CisaRequiredAction { get; set; }
    public string CisaVulnerabilityName { get; set; }
}
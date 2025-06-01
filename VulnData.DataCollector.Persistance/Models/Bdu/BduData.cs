namespace VulnData.DataCollector.Infrastructure.Models.Bdu;

public class BduData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Vendor Vendor { get; set; }
    public string System { get; set; }
    public string VulnClass { get; set; }
    public DateTime Created { get; set; }
    public double Cvss2 { get; set; }
    public double Cvss3 { get; set; }
    public string Fixes { get; set; }
    public string Status { get; set; }
    public bool HasExploit { get; set; }
    public bool HasEliminated { get; set; }
    public string Links { get; set; }
    public string OtherId { get; set; }
    public string OtherInfo { get; set; }
    public bool HasIncidents { get; set; }
    public string WayToDestroy { get; set; }
    public string WayToFix { get; set; }
    public Cwe Cwe { get; set; }
}
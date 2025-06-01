namespace VulnData.DataCollector.Persistance.Models.Nvd;

public class NvdData
{
    public string Cve { get; set; }
    public string Description { get; set; }
    public string Cwe { get; set; }
    public double Cvss2 { get; set; }
    public double Cvss3 { get; set; }
    public double Cvss4 { get; set; }
    public string Reference  { get; set; }
}
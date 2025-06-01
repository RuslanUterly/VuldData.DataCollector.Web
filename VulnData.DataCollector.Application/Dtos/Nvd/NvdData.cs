namespace VulnData.DataCollector.Application.Dtos.Nvd;

public class NvdData
{
    public int ResultPerPage { get; set; }
    public int StartIndex { get; set; }
    public int TotalResults { get; set; }
    public List<Vulnerability> Vulnerabilities { get; set; }
}
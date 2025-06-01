namespace VulnData.DataCollector.Application.Dtos;

public class EpssResponse
{
    public int Total { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    
    public List<EpssData>? Data { get; set; } = new ();
}
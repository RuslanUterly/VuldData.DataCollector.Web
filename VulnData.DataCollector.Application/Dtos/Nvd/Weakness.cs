namespace VulnData.DataCollector.Application.Dtos.Nvd;

public class Weakness
{
    public string Type { get; set; }
    public List<Description> Description { get; set; }
}
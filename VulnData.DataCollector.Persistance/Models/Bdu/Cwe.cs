using Microsoft.EntityFrameworkCore;

namespace VulnData.DataCollector.Infrastructure.Models.Bdu;

[Owned]
public class Cwe 
{
    public string Type { get; set; }
    public string Description { get; set; }
}
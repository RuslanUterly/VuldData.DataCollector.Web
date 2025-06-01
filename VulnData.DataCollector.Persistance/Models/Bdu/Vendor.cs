using Microsoft.EntityFrameworkCore;

namespace VulnData.DataCollector.Infrastructure.Models.Bdu;

[Owned]
public class Vendor
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Version { get; set; }
    public string Type { get; set; }
}
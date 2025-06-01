using VulnData.DataCollector.Application.Dtos.Nvd;
using NvdData = VulnData.DataCollector.Persistance.Models.Nvd.NvdData;

namespace VulnData.DataCollector.Application.Mapping;

public static class NvdMapping
{
    public static List<NvdData> ToNvdData(this IEnumerable<Vulnerability> vulnerabilities)
    {
        return vulnerabilities
            .Select(v => new NvdData
            {
                Cve = v.Cve.Id,
                Description = v.Cve.Descriptions.FirstOrDefault(d => d.Lang == "en")?.Value ?? string.Empty,
                Cvss2 = v.Cve.Metrics.CvssMetricV2?.FirstOrDefault()?.CvssData.BaseScore ?? -1,
                Cvss3 = v.Cve.Metrics.CvssMetricV31?.FirstOrDefault()?.CvssData.BaseScore ?? -1,
                Cvss4 = v.Cve.Metrics?.CvssMetricV40?.FirstOrDefault()?.CvssData.BaseScore ?? -1,
                Cwe = v.Cve.Weaknesses?.FirstOrDefault()?.Description.FirstOrDefault(d => d.Lang == "en")?.Value ?? string.Empty,
                Reference = string.Join("\n", v.Cve.References?.Select(r => r.Url) ?? new List<string>())
            })
            .ToList();
    }
}
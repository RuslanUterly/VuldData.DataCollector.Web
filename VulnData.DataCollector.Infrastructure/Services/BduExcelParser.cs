using System.Text.RegularExpressions;
using Aspose.Cells;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Infrastructure.Models.Bdu;

namespace VulnData.DataCollector.Domain.Services;

public class BduExcelParser : IExcelParser<BduData>
{
    public async Task<List<BduData>> ParseDataAsync(string filePath)
    {
        var workbook = new Workbook(filePath);
        var worksheet = workbook.Worksheets[0];
        
        List<BduData> bduData = new List<BduData>();

        for (int row = 3; row <= worksheet.Cells.MaxDataRow; row++)
        {
            var data = ParseRow(worksheet.Cells, row);
            
            if (data.OtherId.Contains("CVE"))
            {
                bduData.Add(data);
            }
        }
        
        return bduData;
    }

    private BduData ParseRow(Cells cells, int row)
    {
        BduData bduData = new BduData();
        bduData.Vendor = new Vendor();
        bduData.Cwe = new Cwe();
        
        bduData.Id = cells.CheckCell(row, 0)?.StringValue ?? string.Empty;
        bduData.Name = cells.CheckCell(row, 1)?.StringValue ?? string.Empty;
        bduData.Description = cells.CheckCell(row, 2)?.StringValue ?? string.Empty;
        bduData.Vendor.Title = cells.CheckCell(row, 3)?.StringValue ?? string.Empty;
        bduData.Vendor.Name = cells.CheckCell(row, 4)?.StringValue ?? string.Empty;
        bduData.Vendor.Version = cells.CheckCell(row, 5)?.StringValue ?? string.Empty;
        bduData.Vendor.Type = cells.CheckCell(row, 6)?.StringValue ?? string.Empty;
        bduData.System = cells.CheckCell(row, 7)?.StringValue ?? string.Empty;
        bduData.VulnClass = cells.CheckCell(row, 8)?.StringValue ?? string.Empty;
        
        var dateCell = cells.CheckCell(row, 9);
        
        if (dateCell != null)
        {
            bduData.Created = DateTime.Parse(dateCell.StringValue);
        }
        else
        {
            bduData.Created = new DateTime(1900, 1, 1);
        }
        
        string cvssData = cells.CheckCell(row, 12)?.StringValue ?? string.Empty;
        
        var cvssMatches = Regex.Matches(cvssData, @"CVSS\s*(\d+\.\d+)[^\d]*(\d+[\.,]\d+|\d+)", RegexOptions.IgnoreCase);
        
        foreach (Match m in cvssMatches)
        {
            if (m.Groups[1].Value == "2.0")
            {
                if (double.TryParse(m.Groups[2].Value, out double scope))
                    bduData.Cvss2 = scope;
            }
            else if (m.Groups[1].Value == "3.0")
            {
                if (double.TryParse(m.Groups[2].Value, out double scope))
                    bduData.Cvss3 = scope;
            }
        }
        
        bduData.Fixes = cells.CheckCell(row, 13)?.StringValue ?? string.Empty;
        bduData.Status = cells.CheckCell(row, 14)?.StringValue ?? string.Empty;
        
        string hasExploit = cells.CheckCell(row, 15)?.StringValue ?? string.Empty;
        
        if (hasExploit.Contains("существует", StringComparison.OrdinalIgnoreCase))
            bduData.HasExploit = true;
        else
            bduData.HasExploit = false;
        
        string hasEliminated = cells.CheckCell(row, 16)?.StringValue ?? string.Empty;
        
        if (hasEliminated.Contains("отсутствует", StringComparison.OrdinalIgnoreCase))
            bduData.HasEliminated = false;
        else
            bduData.HasEliminated = true;
        
        bduData.Links = cells.CheckCell(row, 17)?.StringValue ?? string.Empty;
        bduData.OtherId = cells.CheckCell(row, 18)?.StringValue ?? string.Empty;
        bduData.OtherInfo = cells.CheckCell(row, 19)?.StringValue ?? string.Empty;
        
        bduData.HasIncidents = (cells.CheckCell(row, 21)?.StringValue ?? "0") == "1" ? true : false;
        
        bduData.WayToDestroy = cells.CheckCell(row, 21)?.StringValue ?? string.Empty;
        bduData.WayToFix = cells.CheckCell(row, 22)?.StringValue ?? string.Empty;
        bduData.Cwe.Description = cells.CheckCell(row, 23)?.StringValue ?? string.Empty;
        bduData.Cwe.Type = cells.CheckCell(row, 24)?.StringValue ?? string.Empty;
        
        return bduData;
    }
}
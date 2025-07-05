using System.Globalization;
using Aspose.Cells;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Infrastructure.Models.Epss;

namespace VulnData.DataCollector.Domain.Services;

public class EpssExcelParser : IExcelParser<EpssData>
{
    public async Task<List<EpssData>> ParseDataAsync(string filePath)
    {
        var loadOptions = new LoadOptions(LoadFormat.Csv);
        
        var workbook = new Workbook(filePath, loadOptions);
        var worksheet = workbook.Worksheets[0];
        
        List<EpssData> epssData = new List<EpssData>();

        for (int row = 5; row <= worksheet.Cells.MaxDataRow; row++)
        {
            epssData.Add(ParseRow(worksheet.Cells, row));
        }
        
        return epssData;
    }

    private EpssData ParseRow(Cells cells, int row)
    {
        EpssData epssData = new EpssData();
        
        epssData.Cve = cells.CheckCell(row, 0)?.StringValue ?? string.Empty;
        string epss = cells.CheckCell(row, 1)?.StringValue ?? string.Empty;
        string percentile = cells.CheckCell(row, 2)?.StringValue ?? string.Empty;
        
        if (double.TryParse(epss, NumberStyles.Any, CultureInfo.InvariantCulture, out double epssScope))
        {
            epssData.Epss = epssScope;
        }

        if (double.TryParse(percentile, NumberStyles.Any, CultureInfo.InvariantCulture, out double percentileScope))
        {
            epssData.Percentile = percentileScope;
        }
        
        return epssData;
    }
}
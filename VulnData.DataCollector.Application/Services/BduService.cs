using System.Text.RegularExpressions;
using Aspose.Cells;
using VulnData.DataCollector.Infrastructure.Models;
using VulnData.DataCollector.Application.Interfaces;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Infrastructure.Interfaces;
using VulnData.DataCollector.Infrastructure.Models.Bdu;

namespace VulnData.DataCollector.Application.Services;

public class BduService : IBduService
{
    private readonly IBduDataRepository _bduDataRepository;
    private readonly IFileDownloader _fileDownloader;
    private readonly IExcelParser<BduData> _excelParser;
    
    public BduService(IFileDownloader fileDownloader, IExcelParser<BduData> excelParser, IBduDataRepository bduDataRepository)
    {
        _fileDownloader = fileDownloader;
        _excelParser = excelParser;
        _bduDataRepository = bduDataRepository;
    }
    
    public async Task<bool> LoadBduToDbScopesAsync()
    {
        string requestUrl = "https://bdu.fstec.ru/files/documents/vullist.xlsx";
        string filePath = "";
        string fileName = "bduDB.xlsx";
        
        fileName = await _fileDownloader.DownloadFileAsync(requestUrl, filePath, fileName);
        
        if (fileName == null)
            throw new Exception("fail to load vulnlist");
        
        var bduScopes = await _excelParser.ParseDataAsync(fileName);

        if (bduScopes == null)
            throw new Exception("fail to load bdu scopes");
        
        var result = await _bduDataRepository.AddRange(bduScopes);
        
        return result;
    }
}
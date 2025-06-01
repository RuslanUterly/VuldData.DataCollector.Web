using System.Globalization;
using System.IO.Compression;
using System.Text.RegularExpressions;
using Aspose.Cells;
using Microsoft.AspNetCore.Http.Timeouts;
using VulnData.DataCollector.Application.Dtos;
using VulnData.DataCollector.Application.Interfaces;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Infrastructure.Interfaces;
using EpssData = VulnData.DataCollector.Infrastructure.Models.Epss.EpssData;

namespace VulnData.DataCollector.Application.Services;

public class EpssService : IEpssService
{
    private readonly IEpssDataRepository _epssDataRepository;
    private readonly IFileDownloader _fileDownloader;
    private readonly IExcelParser<EpssData> _excelParser;
    private readonly IDecompressor _decompressor;
    
    public EpssService(IEpssDataRepository epssDataRepository, IFileDownloader fileDownloader, IExcelParser<EpssData> excelParser, IDecompressor decompressor)
    {
        _epssDataRepository = epssDataRepository;
        _fileDownloader = fileDownloader;
        _excelParser = excelParser;
        _decompressor = decompressor;
    }

    public async Task<bool> LoadEpssToDbScopesAsync()
    {
        string requestUrl = "https://epss.empiricalsecurity.com/epss_scores-current.csv.gz";
        string filePath = "";
        string compressedFileName = "epssDB.csv.gz";
        string decompressedFileName = "epssDB.csv";
        
        compressedFileName = await _fileDownloader.DownloadFileAsync(requestUrl, filePath, compressedFileName);
        
        if (compressedFileName == null)
            throw new Exception("fail to load vulnlist");
        
        decompressedFileName = await _decompressor.DecompressAsync(compressedFileName, decompressedFileName);

        if (decompressedFileName == null)
            throw new Exception("fail to decompressed vulnlist");
        
        var epssScopes = await _excelParser.ParseDataAsync(decompressedFileName);
        
        if (epssScopes == null)
            throw new Exception("fail to load epss scopes");
        
        var result = await _epssDataRepository.AddRange(epssScopes);
        
        return result;
    }
}
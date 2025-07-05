using VulnData.DataCollector.Application.Interfaces;
using VulnData.DataCollector.Application.Interfaces.Domain;

namespace VulnData.DataCollector.Domain.Services;

public class FileDownloader : IFileDownloader
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _clientName;

    public FileDownloader(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _clientName = configuration["ClientName"] ?? "";
    }

    public async Task<string> DownloadFileAsync(string requestUrl, string filePath, string fileName)
    {
        var httpClient = string.IsNullOrEmpty(_clientName) ? 
            _httpClientFactory.CreateClient() : 
            _httpClientFactory.CreateClient(_clientName);

        await using var stream = await httpClient.GetStreamAsync(requestUrl);
    
        filePath = Path.Combine(filePath, fileName);
        
        using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
    
        await stream.CopyToAsync(fileStream);
    
        return fileName;
    }
}
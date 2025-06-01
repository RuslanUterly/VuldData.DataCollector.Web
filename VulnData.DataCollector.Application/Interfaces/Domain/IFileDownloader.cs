namespace VulnData.DataCollector.Application.Interfaces.Domain;

public interface IFileDownloader
{
    Task<string> DownloadFileAsync(string requestUrl, string filePath, string fileName);
}
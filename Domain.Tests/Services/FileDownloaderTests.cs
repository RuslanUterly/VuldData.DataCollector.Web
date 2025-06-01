using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using VulnData.DataCollector.Application.Interfaces;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Domain.Services;
using Xunit;

namespace Domain.Tests.Services;

public class FileDownloaderTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<IConfiguration> _configurationMock;

    public FileDownloaderTests()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _configurationMock = new Mock<IConfiguration>();
    }

    [Fact]
    public async Task FileDownloader_DownloadBduFileAsync_ReturnObject()
    {
        //Arrange
        const string requestUrl = "https://bdu.fstec.ru/files/documents/vullist.xlsx";
        const string filePath = "";
        const string fileName = "documents.xlsx";

        const string clientName = "vuln";
        
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        };
        
        var httpClient = new HttpClient(handler);
        
        _httpClientFactoryMock
            .Setup(x => x.CreateClient(clientName))
            .Returns(httpClient);
        
        _configurationMock.Setup(x => x[It.IsAny<string>()]).Returns("vuln");
        
        IFileDownloader downloader = new FileDownloader(_httpClientFactoryMock.Object, _configurationMock.Object);

        //Act
        var result = await downloader.DownloadFileAsync(requestUrl, filePath, fileName);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().Be(fileName);
    }
    
    [Fact]
    public async Task FileDownloader_DownloadEpssFileAsync_ReturnObject()
    {
        //Arrange
        const string requestUrl = "https://epss.empiricalsecurity.com/epss_scores-current.csv.gz";
        const string filePath = "";
        const string fileName = "documents.csv.gz";
        
        _httpClientFactoryMock
            .Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient());
        
        _configurationMock.Setup(x => x[It.IsAny<string>()]).Returns("");
        
        IFileDownloader downloader = new FileDownloader(_httpClientFactoryMock.Object, _configurationMock.Object);

        //Act
        var result = await downloader.DownloadFileAsync(requestUrl, filePath, fileName);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().Be(fileName);
    }
}
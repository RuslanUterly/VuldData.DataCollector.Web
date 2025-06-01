using FluentAssertions;
using Moq;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Application.Services;
using VulnData.DataCollector.Infrastructure.Interfaces;
using VulnData.DataCollector.Infrastructure.Models.Epss;
using Xunit;

namespace BduController.Tests.Services;

public class EpssServiceTests
{
    private readonly EpssService _epssService;
    private readonly Mock<IEpssDataRepository> _repositoryMock;
    private readonly Mock<IFileDownloader> _fileDownloaderMock;
    private readonly Mock<IExcelParser<EpssData>> _excelParserMock;
    private readonly Mock<IDecompressor> _decompressorMock;

    public EpssServiceTests()
    {
        _repositoryMock = new Mock<IEpssDataRepository>();
        _fileDownloaderMock = new Mock<IFileDownloader>();
        _excelParserMock = new Mock<IExcelParser<EpssData>>();
        _decompressorMock = new Mock<IDecompressor>();
        _epssService = new EpssService(_repositoryMock.Object, _fileDownloaderMock.Object, _excelParserMock.Object, _decompressorMock.Object);
    }

    [Fact]
    public async Task EpssService_LoadEpssToDbScopesAsync_ReturnObject()
    {
        //Arrange
        const string requestUrl = "https://epss.empiricalsecurity.com/epss_scores-current.csv.gz";
        const string filePath = "";
        const string comfileName = "epssDB.csv.gz";
        const string decomfileName = "epssDB.csv";
        
        //Act
        _fileDownloaderMock.Setup(f => f.DownloadFileAsync(requestUrl, filePath, comfileName)).ReturnsAsync(comfileName);
        _excelParserMock.Setup(f => f.ParseDataAsync(decomfileName)).ReturnsAsync(new List<EpssData>());
        _repositoryMock.Setup(f => f.AddRange(It.IsAny<List<EpssData>>())).ReturnsAsync(true);
        _decompressorMock.Setup(d => d.DecompressAsync(comfileName,decomfileName)).ReturnsAsync(decomfileName);

        var result = await _epssService.LoadEpssToDbScopesAsync();

        //Assert
        result.Should().BeTrue();
    }
}
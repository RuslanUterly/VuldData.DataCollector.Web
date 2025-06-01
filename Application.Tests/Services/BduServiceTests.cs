using FluentAssertions;
using Moq;
using VulnData.DataCollector.Application.Interfaces;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Application.Services;
using VulnData.DataCollector.Domain.Services;
using VulnData.DataCollector.Infrastructure.Interfaces;
using VulnData.DataCollector.Infrastructure.Models.Bdu;
using Xunit;

namespace BduController.Tests.Services;

public class BduServiceTests
{
    private readonly BduService _bduService;
    private readonly Mock<IBduDataRepository> _repositoryMock;
    private readonly Mock<IFileDownloader> _fileDownloaderMock;
    private readonly Mock<IExcelParser<BduData>> _excelParserMock;

    public BduServiceTests()
    {
        _repositoryMock = new();
        _fileDownloaderMock = new();
        _excelParserMock = new();
        _bduService = new(_fileDownloaderMock.Object, _excelParserMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public async Task BduService_LoadBduToDbScopesAsync_ReturnObject()
    {
        //Arrange 
        const string requestUrl = "https://bdu.fstec.ru/files/documents/vullist.xlsx";
        const string filePath = "";
        const string fileName = "bduDB.xlsx";
        
        //Act
        _fileDownloaderMock.Setup(f => f.DownloadFileAsync(requestUrl, filePath, fileName)).ReturnsAsync(fileName);
        _excelParserMock.Setup(f => f.ParseDataAsync(fileName)).ReturnsAsync(new List<BduData>());
        _repositoryMock.Setup(f => f.AddRange(It.IsAny<List<BduData>>())).ReturnsAsync(true);
        
        var result = await _bduService.LoadBduToDbScopesAsync();

        //Assert
        result.Should().BeTrue();
    }
}
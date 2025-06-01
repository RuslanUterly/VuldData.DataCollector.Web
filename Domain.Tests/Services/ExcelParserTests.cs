using FluentAssertions;
using VulnData.DataCollector.Domain.Services;
using Xunit;

namespace Domain.Tests.Services;

public class ExcelParserTests
{
    public ExcelParserTests()
    {
        
    }

    [Fact]
    public async Task BduExcelParser_ParseDataAsync_ReturnObject()
    {
        //Arrange
        const string FILE_PATH = @"C:\Users\najmi\Desktop\Project\microservices\VuldData.DataCollector.Web\VuldData.DataCollector.Web\bduDB.xlsx";
        var parser = new BduExcelParser();

        //Act
        var result = await parser.ParseDataAsync(FILE_PATH);

        //Assert
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task BduExcelParser_ParseDataAsync_NotAllDefaultDate()
    {
        //Arrange
        const string FILE_PATH = @"C:\Users\najmi\Desktop\Project\microservices\VuldData.DataCollector.Web\VuldData.DataCollector.Web\bduDB.xlsx";
        var parser = new BduExcelParser();
    
        //Act
        var result = await parser.ParseDataAsync(FILE_PATH);

        var isAllDefaultCreated = result.All(r => r.Created == new DateTime(1900, 1, 1));
    
        //Assert
        isAllDefaultCreated.Should().BeFalse();
    }
    
    [Fact]
    public async Task EpssExcelParser_ParseDataAsync_ReturnObject()
    {
        //Arrange
        const string FILE_PATH = @"C:\Users\najmi\Desktop\Project\microservices\VuldData.DataCollector.Web\VuldData.DataCollector.Web\epssDB.csv";
        var parser = new EpssExcelParser();

        //Act
        var result = await parser.ParseDataAsync(FILE_PATH);

        //Assert
        result.Should().NotBeNull();
    }
}
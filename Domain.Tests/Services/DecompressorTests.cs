using FluentAssertions;
using VulnData.DataCollector.Domain.Services;
using Xunit;

namespace Domain.Tests.Services;

public class DecompressorTests
{
    public DecompressorTests()
    {
        
    }

    [Fact]
    public async Task Decompressor_DecompressAsync_ReturnedObject()
    {
        const string COMPRESSED_FILE_PATH = @"C:\Users\najmi\Desktop\Project\microservices\VuldData.DataCollector.Web\VuldData.DataCollector.Web\epssDB.csv.gz";
        const string DECOMPRESSED_FILE_PATH = @"C:\Users\najmi\Desktop\Project\microservices\VuldData.DataCollector.Web\VuldData.DataCollector.Web\epssDB.csv";

        var decompressor = new Decompressor();
        
        //Act
        var result = await decompressor.DecompressAsync(COMPRESSED_FILE_PATH, DECOMPRESSED_FILE_PATH);

        //Assert
        result.Should().NotBeNull();
    }
}
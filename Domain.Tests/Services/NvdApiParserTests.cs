using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Domain.Options;
using VulnData.DataCollector.Domain.Services;
using Xunit;

namespace Domain.Tests.Services;

public class NvdApiParserTests
{
    private readonly NvdApiParser _nvdApiParser;
    private readonly VulnerabilityFetcher _vulnerabilityFetcher;
    private readonly string TestApiKey = "";
    
    public NvdApiParserTests()
    {
        var services = new ServiceCollection();
        services.AddHttpClient();
        services.AddSingleton<IOptions<NvdOptions>>(new OptionsWrapper<NvdOptions>(new NvdOptions { ApiKey = TestApiKey }));
        
        var provider = services.BuildServiceProvider();
        _vulnerabilityFetcher = new VulnerabilityFetcher(
            provider.GetRequiredService<IOptions<NvdOptions>>(),
            provider.GetRequiredService<IHttpClientFactory>()
        );
        
        _nvdApiParser = new NvdApiParser(_vulnerabilityFetcher);

    }

    [Fact]
    public async Task NvdApiParser_ParseDataAsync_ReturnObject()
    {
        //Act
        var result = await _nvdApiParser.ParseDataAsync();

        //Assert
        
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(1000);
        result.Should().OnlyHaveUniqueItems<string>(d => d.Cve);
    }
}
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using VuldData.DataCollector.Web.Controllers;
using VulnData.DataCollector.Application.Interfaces;
using VulnData.DataCollector.Application.Services;
using VulnData.DataCollector.Infrastructure.Interfaces;

namespace BduController.Tests;

public class BduControllerTests
{
    private readonly VuldData.DataCollector.Web.Controllers.BduController _bduController;
    private readonly Mock<IBduService> _bduServiceMock;
    private readonly Mock<IBduDataRepository> _repositoryMock;

    public BduControllerTests()
    {
        _repositoryMock = new();
        _bduServiceMock = new ();
        _bduController = new (_bduServiceMock.Object);
    }

    [Fact]
    public async Task BduController_LoadDbAsync_ReturnObject()
    {
        //Act
        _bduServiceMock.Setup(b => b.LoadBduToDbScopesAsync()).ReturnsAsync(true);
        
        var result = await _bduController.LoadDbAsync();
        
        //Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be(true);
    }
}
using DataCollector.Сommand;
using MassTransit;
using VulnData.DataCollector.Application.Interfaces;

namespace VulnData.DataCollector.Application.Consumers;

public class DataCollectionConsumer : IConsumer<StartDataCollectionCommand>
{
    private readonly IEpssService _epssService;
    private readonly IBduService _bduService;
    private readonly INvdService _nvdService;
    private readonly ILogger<DataCollectionConsumer> _logger;

    public DataCollectionConsumer(ILogger<DataCollectionConsumer> logger, IEpssService epssService, IBduService bduService, INvdService nvdService)
    {
        _logger = logger;
        _epssService = epssService;
        _bduService = bduService;
        _nvdService = nvdService;
    }

    public async Task Consume(ConsumeContext<StartDataCollectionCommand> context)
    {
        _logger.LogInformation("Starting data collection triggered at {Time}", 
            context.Message.TriggerTime);

        new Task(async () =>
        {
            var results = await _bduService.LoadBduToDbScopesAsync();
            _logger.LogInformation("Bdu success");
        });
        
        var isLoadBdu = await _bduService.LoadBduToDbScopesAsync();
        var isLoadEpss = await _epssService.LoadEpssToDbScopesAsync();
        var isLoadNvd = await _nvdService.LoadNvdToDbScopesAsync();
        
        _logger.LogInformation(@"Results:
                BDU: {isLoadBdu}
                EPSS: {isLoadEpss}
                NVD: {isLoadNvd}");
        

        // var results = await Task.WhenAll(
        //     _bduService.LoadBduToDbScopesAsync(),
        //     _epssService.LoadEpssToDbScopesAsync()
        //     // _nvdService.LoadNvdToDbScopesAsync()
        // );
        //
        // if (results.Any(r => !r)) 
        //     _logger.LogInformation("Some db not updated");
    }
}
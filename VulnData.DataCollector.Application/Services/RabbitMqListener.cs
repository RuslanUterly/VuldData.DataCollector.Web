using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VulnData.DataCollector.Application.Interfaces;

namespace VulnData.DataCollector.Application.Services;

public class RabbitMqListener : BackgroundService
{
    private readonly IBduService _bduService;
    private readonly IEpssService _epssService;
    private readonly INvdService _nvdService;
    
    public RabbitMqListener(IBduService bduService, IEpssService epssService, INvdService nvdService)
    {
        _bduService = bduService;
        _epssService = epssService;
        _nvdService = nvdService;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync(stoppingToken);
        using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
    
        await channel.QueueDeclareAsync(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false,
            arguments: null, cancellationToken: stoppingToken);
    
        Console.WriteLine(" [*] Waiting for messages.");
    
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var isLoadBdu = await _bduService.LoadBduToDbScopesAsync();
            Console.WriteLine(" [x] Load bdu {0}", isLoadBdu);
            
            var isLoadEpss = await _epssService.LoadEpssToDbScopesAsync();
            Console.WriteLine(" [x] Load epss {0}", isLoadEpss);
        };
    
        await channel.BasicConsumeAsync("MyQueue", autoAck: true, consumer: consumer, cancellationToken: stoppingToken);
        
        Console.WriteLine(" [x] Done");
        Console.ReadLine();
    }
    
    // private IConnection _connection;
    // private IChannel _channel;
    //
    // public RabbitMqListener()
    // {
    // }
    //
    // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    // {
    //     var factory = new ConnectionFactory { HostName = "localhost" };
    //     _connection = await factory.CreateConnectionAsync(stoppingToken);
    //     _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
    //     await _channel.QueueDeclareAsync(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null, cancellationToken: stoppingToken);
    //     
    //     stoppingToken.ThrowIfCancellationRequested();
    //
    //     var consumer = new AsyncEventingBasicConsumer(_channel);
    //     consumer.ReceivedAsync += async (ch, ea) =>
    //     {
    //         var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			 //
    //         // Каким-то образом обрабатываем полученное сообщение
    //         Console.WriteLine($"Получено сообщение: {content}");
    //
    //         await _channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);
    //     };
    //
    //     await _channel.BasicConsumeAsync("MyQueue", false, consumer, cancellationToken: stoppingToken);
    // }
	   //
    // public override async void Dispose()
    // {
    //     await _channel.CloseAsync();
    //     await _connection.CloseAsync();
    //     base.Dispose();
    // }
}

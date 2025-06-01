using DataCollector.Сommand;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using VuldData.DataCollector.Web.Extensions;
using VuldData.DataCollector.Web.Middlewares;
using VulnData.DataCollector.Application.Consumers;
using VulnData.DataCollector.Application.Interfaces;
using VulnData.DataCollector.Application.Interfaces.Domain;
using VulnData.DataCollector.Application.Services;
using VulnData.DataCollector.Domain;
using VulnData.DataCollector.Domain.Options;
using VulnData.DataCollector.Domain.Persistance.DataAccess;
using VulnData.DataCollector.Domain.Services;
using VulnData.DataCollector.Infrastructure.Data;
using VulnData.DataCollector.Infrastructure.Interfaces;
using VulnData.DataCollector.Infrastructure.Models.Bdu;
using VulnData.DataCollector.Infrastructure.Models.Epss;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpClient("vuln")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
    });

builder.Services.AddTransient<IEpssService, EpssService>();
builder.Services.AddTransient<INvdService, NvdService>();
builder.Services.AddTransient<IBduService, BduService>();

builder.Services.AddTransient<IExcelParser<BduData>, BduExcelParser>();
builder.Services.AddTransient<IExcelParser<EpssData>, EpssExcelParser>();
builder.Services.AddTransient<IFileDownloader, FileDownloader>();
builder.Services.AddTransient<IDecompressor, Decompressor>();
builder.Services.AddTransient<IVulnerabilityFetcher, VulnerabilityFetcher>();
builder.Services.AddTransient<INvdApiParser, NvdApiParser>();

builder.Services.AddTransient<IBduDataRepository, BduDataRepository>();
builder.Services.AddTransient<IEpssDataRepository, EpssDataRepository>();
builder.Services.AddTransient<INvdDataRepository, NvdDataRepository>();

RabbitMQOptions rabbitMqOptions = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQOptions>() ?? new RabbitMQOptions();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<DataCollectionConsumer>(cfg =>
    {
        cfg.Options<JobOptions<StartDataCollectionCommand>>(options => options
            .SetJobTimeout(TimeSpan.FromMinutes(15)));
        // .SetRetry(r => r.Interval(5,30000)));
    });
    
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqOptions.Host, host => 
        {
            host.Username(rabbitMqOptions.Username);
            host.Password(rabbitMqOptions.Password);
        });

        cfg.ReceiveEndpoint(rabbitMqOptions.Queue, e =>
        {
            e.ConfigureConsumer<DataCollectionConsumer>(context);
            e.UseTimeout((configurator =>
            {
                configurator.Timeout = TimeSpan.FromMinutes(5);
            }));
        });
    });
});

// builder.Services.AddHostedService<RabbitMqListener>();

builder.Services.AddDbContextFactory<VulnContext>(options =>
{
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), opt =>
        {
            opt.CommandTimeout(60);
        })
        .EnableSensitiveDataLogging();
});

builder.Services.Configure<NvdOption>(builder.Configuration.GetSection("Nvd"));

builder.UseKeycloakAuth();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VULN_COLL API");
        c.EnablePersistAuthorization();
    });
}

// app.UseAuthorization();
// app.UseAuthorization();

app.UseExceptionHandler();

app.UseHttpsRedirection();
app.MapControllers();
// app.UseMiddleware<RestrictAccessMiddleware>();

app.UseCors("AllowAll");

app.Run();
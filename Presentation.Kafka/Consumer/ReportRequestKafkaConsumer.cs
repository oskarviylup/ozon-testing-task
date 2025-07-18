using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using project.Application.Models;

namespace Presentation.Kafka.Consumer;

public class ReportRequestKafkaConsumer : BackgroundService, IReportRequestKafkaConsumer
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly IOptions<KafkaConsumerOptions> _options;
    private readonly IServiceProvider _provider;
    
    public ReportRequestKafkaConsumer(IServiceProvider provider)
    {
        _provider = provider;
        _options = _provider.GetRequiredService<IOptions<KafkaConsumerOptions>>();
        
        var config = new ConsumerConfig
        {
            BootstrapServers = _options.Value.ConnectionString,
            GroupId = "default-group-4",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("✅ Kafka Consumer запускается...");
        _consumer.Subscribe(_options.Value.Topic);
        
        Console.WriteLine("✅ Kafka Consumer подписался на топик");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                Console.WriteLine("✅ Kafka Consumer получил сообщение...");

                if (consumeResult?.Message != null)
                {
                    var jsonRequest = consumeResult.Message.Value;
                    KafkaReportRequest? reportRequest;
                    using var scope = _provider.CreateScope();
                    var handler = scope.ServiceProvider.GetRequiredService<ReportRequestHandler>();

                    try
                    {
                        reportRequest = JsonSerializer.Deserialize<KafkaReportRequest>(jsonRequest);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"[Kafka] Ошибка десериализации: {e.Message}");
                        continue;
                    }

                    if (reportRequest != null) { await handler.HandleReportRequestAsync(reportRequest);}
                }
            }
            catch (ConsumeException ex)
            {
                Console.WriteLine($"[Kafka] Ошибка: {ex.Error.Reason}");
            }

            await Task.Yield();
        }
        _consumer.Close();
    }

    public Task StartConsumingAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
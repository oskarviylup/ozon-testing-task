using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using project.Application.Models;

namespace Presentation.Kafka.Producer;

public class ReportRequestKafkaProducer : IReportRequestKafkaProducer, IDisposable
{
    private readonly IProducer<Null, string> _producer;
    private readonly IOptions<KafkaProducerOptions> _options;

    public ReportRequestKafkaProducer(IServiceProvider provider)
    {
        _options = provider.GetRequiredService<IOptions<KafkaProducerOptions>>();
        var config = new ProducerConfig
        {
            BootstrapServers = _options.Value.ConnectionString
        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceAsync(KafkaReportRequest reportRequest, CancellationToken cancellationToken = default)
    {
        var message = new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(reportRequest)
        };
        await _producer.ProduceAsync(_options.Value.Topic, message, cancellationToken);
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}
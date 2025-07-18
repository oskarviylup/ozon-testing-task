using project.Application.Models;

namespace Presentation.Kafka.Producer;

public interface IReportRequestKafkaProducer
{
    Task ProduceAsync(KafkaReportRequest reportRequest, CancellationToken cancellationToken = default);
}
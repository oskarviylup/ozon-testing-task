namespace Presentation.Kafka.Consumer;

public interface IReportRequestKafkaConsumer
{
    Task StartConsumingAsync(CancellationToken token);
}
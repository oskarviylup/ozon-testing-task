using Microsoft.Extensions.DependencyInjection;
using Presentation.Kafka.Consumer;
using Presentation.Kafka.Producer;
using project.Application.Models;

namespace Presentation.Kafka;

public static class KafkaServiceCollectionExtension
{
    public static void AddKafkaOptions(this IServiceCollection collection)
    {
        collection.Configure<KafkaProducerOptions>(op =>
        {
            op.Topic = "report_requests";
            op.ConnectionString = "localhost:9092";
        });
        collection.Configure<KafkaConsumerOptions>(op =>
        {
            op.Topic = "report_requests";
            op.ConnectionString = "localhost:9092";
        });
        
    }

    public static void AddKafkaProducer(this IServiceCollection collection)
    {
        collection.AddScoped<IReportRequestKafkaProducer, ReportRequestKafkaProducer>();
    }

    public static void AddKafkaConsumer(this IServiceCollection collection)
    {
        collection.AddScoped<IReportRequestKafkaConsumer, ReportRequestKafkaConsumer>();
    }
}
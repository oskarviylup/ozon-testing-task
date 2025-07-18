namespace project.Application.Models;

public class KafkaConsumerOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public string Topic { get; set; } = string.Empty;
}
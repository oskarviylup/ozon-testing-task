namespace project.Application.Models;

public class KafkaProducerOptions
{
    public string Topic { get; set; } = string.Empty;

    public string ConnectionString { get; set; } = string.Empty;
}
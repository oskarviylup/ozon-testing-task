namespace project.Application.Models;

public class KafkaReportRequest(ReportRequestParameters requestParameters)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid UserId { get; set; }
    
    public DateTime DateTimeCreated { get; set; } = DateTime.Now;
    public ReportRequestParameters RequestParameters { get; set; } = requestParameters;
}
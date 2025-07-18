namespace project.Application.Models;

public class ReportRequestInputDto
{
    public Guid UserId { get; set; }
    
    public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
    
    public Guid ItemId { get; set; }
    
    public DateOnly ConversionCheckPeriodStart { get; set; }
    
    public DateOnly ConversionCheckPeriodEnd { get; set; }
    
    public Guid OrderId { get; set; }
}
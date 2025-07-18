namespace project.Application.Models;

public class ReportRequestParameters
{
    public DateOnly ConversionCheckPeriodStart { get; set; }
    public DateOnly ConversionCheckPeriodEnd { get; set; }
    public Guid ItemId { get; set; }
    public Guid OrderId { get; set; }
}
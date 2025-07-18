using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Application.Models.DbModels;

[Table("reports")]
public class ReportRequest
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("created_at")]
    public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;

    [Column("conversion_check_period_start")]
    public DateOnly ConversionCheckPeriodStart { get; set; }

    [Column("conversion_check_period_end")]
    public DateOnly ConversionCheckPeriodEnd { get; set; }

    [Column("item_id")]
    public Guid ItemId { get; set; }

    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Column("conversion")]
    public decimal? Conversion { get; set; }

    [Column("item_payments_amount")]
    public int? ItemPaymentsAmount { get; set; }

    [Column("status")]
    public string Status { get; set; } = "Pending";

    [Column("error")]
    public string? Error { get; set; }
}
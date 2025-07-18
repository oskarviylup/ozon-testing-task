using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Application.Models.DbModels;

[Table("item_payments")]
public class ItemPayment
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("item_id")]
    public Guid ItemId { get; set; }

    [Column("paid_at")]
    public DateTime PaidAt { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("order_id")]
    public Guid OrderId { get; set; }
}
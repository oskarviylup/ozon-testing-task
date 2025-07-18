using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Application.Models.DbModels;

[Table("item_views")]
public class ItemView
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("item_id")]
    public Guid ItemId { get; set; }

    [Column("viewed_at")]
    public DateTime ViewedAt { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }
}
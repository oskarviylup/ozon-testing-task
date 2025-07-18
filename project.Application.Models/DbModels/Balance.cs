using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Application.Models.DbModels;

[Table("balances")]
public class Balance
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Column("amount")]
    public int Amount { get; set; }
}
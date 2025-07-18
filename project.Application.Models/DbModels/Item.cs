using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Application.Models.DbModels;
[Table("items")]
public class Item
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("name")]
    public string Name { get; set; }

    [Required]
    [Column("description")]
    public string Description { get; set; }

    [Column("price")]
    public decimal Price { get; set; }
}
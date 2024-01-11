using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("disciplines")]
public class Discipline
{
    [Key]
    [Column("id")]
    [Required]
    public int Id { get; set; }
    
    [Column("name")]
    [Required]
    public string? Name { get; set; }
}
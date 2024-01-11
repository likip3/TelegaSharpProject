using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;


[Table("rating")]
public class Rating
{
    [Key]
    [Column("id")]
    [Required]
    public User? User { get; set; }
    
    [Column("discipline")]
    [Required]
    public Discipline? Discipline { get; set; }
    
    [Column("amount")]
    [Required]
    public double Amount { get; set; }
}
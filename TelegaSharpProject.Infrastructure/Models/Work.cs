using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("works")]
public class Work
{
    [Key]
    [Column("id")]
    [Required]
    public int Id { get; set; }
    
    [Required]
    [Column("mentor")]
    public User? MentorUser { get; set; }
    
    [Column("mentored_user")]
    public User? MentoredUser { get; set; }
    
    [Required]
    [Column("mentor_start")]
    public DateTime MentorStart { get; set; }
    
    [Column("mentor_end")]
    public DateTime MentorEnd { get; set; }
    
    [Required]
    [Column("price")]
    public double Price { get; set; }
    
    [Required]
    [Column("done")]
    public bool Done { get; set; }
    
    [Required]
    [Column("discipline")]
    public Discipline? Discipline { get; set; }
}
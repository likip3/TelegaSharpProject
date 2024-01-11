using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("messages")]
public class Message
{
    [Key]
    [Column("id")]
    [Required]
    public int Id { get; set; }
    
    [Column("text")]
    [Required]
    public string? Text { get; set; }
    
    [Column("message_time")]
    [Required]
    public DateTime MessageTime { get; set; }
}
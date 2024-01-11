using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("dialogs")]
public class Dialog
{
    [Key]
    [Column("id")]
    [Required]
    public int Id { get; set; }
    
    [Column("user1")]
    [Required]
    public User? User1 { get; set; }
    
    [Column("user2")]
    [Required]
    public User? User2 { get; set; }
    
    [Column("messages")]
    [Required]
    public ICollection<Message>? Messages { get; set; }
}
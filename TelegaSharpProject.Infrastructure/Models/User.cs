using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    [Required]
    public int Id { get; set; }
    
    [Column("user_name")]
    [Required]
    public string? UserName { get; set; }
    
    [Column("tg_user_name")]
    [Required]
    public string? TgUserName { get; set; }
    
    [Column("registered_at")]
    [Required]
    public DateTime RegisteredAt { get; set; }
    
    [Column("money")]
    [Required]
    public double Money { get; set; }
}
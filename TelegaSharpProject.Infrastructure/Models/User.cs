using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("users")]
public class User
{
    public User(long id, string userName, long chatId)
    {
        Id = id;
        RegisteredAt = DateTime.Now;
        Points = 0;
        UserName = userName;
        ChatId = chatId;
    }

    [Key]
    [Column("id")]
    [Required]
    public long Id { get; set; }
    
    [Column("chat_id")]
    [Required]
    public long ChatId { get; set; }
    
    [Column("user_name")]
    [MaxLength(50)]
    [Required]
    public string UserName { get; set; }

    [Column("registered_at")]
    [Required]
    public DateTime RegisteredAt { get; set; }

    [Column("points")]
    public long Points { get; set; }
}
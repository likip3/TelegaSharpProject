using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("users")]
public class User
{
    public User(long id, string userName)
    {
        Id = id;
        RegisteredAt = DateTime.Now;
        Points = 0;
        UserName = userName;
    }

    [Key]
    [Column("id")]
    [Required]
    public long Id { get; set; }
    
    [Column("user_name")]
    [MaxLength(50)]
    [Required]
    public string UserName { get; set; }

    //[Column("tg_user_name")]
    //public string? TgUserName { get; set; }

    [Column("registered_at")]
    [Required]
    public DateTime RegisteredAt { get; set; }

    [Column("points")]
    public int Points { get; set; }
}
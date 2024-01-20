using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("comments")]
public class Comment
{
    public Comment()
    {
    }

    public Comment(long taskId, User? byUser, string? text)
    {
        TaskID = taskId;
        Text = text;
        ByUser = byUser;
        MessageTime = DateTime.Now;
        var gb = Guid.NewGuid().ToByteArray();
        Id = BitConverter.ToInt64(gb, 0);
    }


    [Key]
    [Column("id")]
    [Required]
    public long Id { get; set; }

    [Column("task_id")]
    [Required]
    public long TaskID { get; set; }

    [Column("text")]
    [Required]
    public string? Text { get; set; }

    [Required]
    [Column("user")]
    public User? ByUser { get; set; }

    [Column("message_time")]
    [Required]
    public DateTime MessageTime { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("comments")]
public class Answer
{
    public Answer()
    {
    }

    public Answer(long taskId, User? byUser, string? text)
    {
        TaskId = taskId;
        Text = text;
        ByUser = byUser;
        AnswerTime = DateTime.Now;
        var gb = Guid.NewGuid().ToByteArray();
        Id = BitConverter.ToInt64(gb, 0);
    }

    [Key]
    [Column("id")]
    [Required]
    public long Id { get; set; }

    [Column("task_id")]
    [Required]
    public long TaskId { get; set; }

    [Column("text")]
    [Required]
    public string Text { get; set; }

    [Required]
    [Column("user")]
    public User ByUser { get; set; }

    [Column("message_time")]
    [Required]
    public DateTime AnswerTime { get; set; }
}
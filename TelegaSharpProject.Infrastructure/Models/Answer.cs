using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("answers")]
public class Answer
{
    public Answer()
    {
    }

    public Answer(long taskId, long byUserId, string text)
    {
        TaskId = taskId;
        Text = text;
        ByUserId = byUserId;
        AnswerTime = DateTime.Now;
        
        var gb = Guid.NewGuid().ToByteArray();
        Id = BitConverter.ToInt64(gb, 0);
    }
    
    public void Close()
    {
        Closed = true;
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
    
    [Column("closed")]
    public bool Closed { get; set; }

    [Required]
    [Column("user")]
    public long ByUserId { get; set; }

    [Column("message_time")]
    [Required]
    public DateTime AnswerTime { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("works")]
public class Work
{
    public Work()
    {
    }

    public Work(User topicCreator, string task)
    {
        CreatorId = topicCreator.Id;
        Task = task;
        TopicStart = DateTime.Now;
        var gb = Guid.NewGuid().ToByteArray();
        Id = BitConverter.ToInt64(gb, 0);
        Price = 5;
    }

    public void Close(Answer answer)
    {
        MentorEnd = DateTime.Now;
        Done = true;
        Answer = answer;
    }

    [Key]
    [Column("id")]
    [Required]
    public long Id { get; set; }
    
    [Required]
    [Column("topic_creator")]
    public long CreatorId { get; set; }

    [Required]
    [Column("task")]
    public string Task { get; set; }

    [Required]
    [Column("topic_start")]
    public DateTime TopicStart { get; set; }
    
    [Column("mentor_end")]
    public DateTime? MentorEnd { get; set; }
    
    [Column("answer")]
    public Answer? Answer { get; set; }
    
    [Required]
    [Column("price")]
    public long Price { get; set; }
    
    [Required]
    [Column("done")]
    public bool Done { get; set; }
}
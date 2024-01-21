using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegaSharpProject.Infrastructure.Models;

[Table("works")]
public class Work
{
    public Work()
    {
    }
    public Work(User? topicaster, string task)
    {
        Topicaster = topicaster;
        Task = task;
        TopicStart = DateTime.Now;
        var gb = Guid.NewGuid().ToByteArray();
        Id = BitConverter.ToInt64(gb, 0);
        Price = 5;
    }

    public void Close()
    {
        MentorEnd = DateTime.Now;
        Done = true;
    }

    [Key]
    [Column("id")]
    [Required]
    public long Id { get; set; }
    
    [Required]
    [Column("topicaster")]
    public User? Topicaster { get; set; }

    [Required]
    [Column("task")]
    public string Task { get; set; }

    [Required]
    [Column("topic_start")]
    public DateTime TopicStart { get; set; }
    
    [Column("mentor_end")]
    public DateTime MentorEnd { get; set; }
    
    [Required]
    [Column("price")]
    public double Price { get; set; }
    
    [Required]
    [Column("done")]
    public bool Done { get; set; }
    
    //[Required]
    //[Column("discipline")]
    //public Discipline? Discipline { get; set; }
}
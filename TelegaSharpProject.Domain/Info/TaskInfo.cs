using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Info;

public class TaskInfo : ITaskInfo
{
    public TaskInfo(Work work)
    {
        Id = work.Id;
        
        TopicStart = work.TopicStart;
        Price = work.Price;
        MentorEnd = work.MentorEnd;
        Done = work.Done;
        Text = work.Task;
    }

    public TaskInfo(Work work, User user) : this(work)
    {
        TopicCreator = new UserInfo(user);
    }

    public long Id { get; }

    public IUserInfo TopicCreator { get; }
        
    public DateTime TopicStart { get; }
        
    public DateTime? MentorEnd { get; }
    
    public IUserInfo? Mentor { get; }

    public string Text { get; }
        
    public double Price { get; }

    public bool Done { get; }
}
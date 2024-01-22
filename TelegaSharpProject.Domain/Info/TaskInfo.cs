using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Info;

public class TaskInfo : ITaskInfo
{
    public TaskInfo(Work work)
    {
        Id = work.Id;
        TaskStart = work.TopicStart;
        Price = work.Price;
        MentorEnd = work.MentorEnd;
        Done = work.Done;
        Text = work.Task;
    }

    public TaskInfo(Work work, User user) : this(work)
    {
        Creator = new UserInfo(user);
    }

    public long Id { get; }

    public IUserInfo Creator { get; }
        
    public DateTime TaskStart { get; }
        
    public DateTime? MentorEnd { get; }
    
    public IUserInfo? Mentor { get; }

    public string Text { get; }
        
    public double Price { get; }

    public bool Done { get; }
}
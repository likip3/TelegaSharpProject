namespace TelegaSharpProject.Domain.Interfaces;

public interface ITaskInfo
{
    public long Id { get; }

    public IUserInfo Creator { get;  }
        
    public DateTime TaskStart { get; }
    
    public IAnswerInfo AnswerInfo { get; }
        
    public DateTime? MentorEnd { get; }
        
    public IUserInfo? Mentor { get; }
        
    public string Text { get; }
        
    public double Price { get; }

    public bool Done { get; }
}
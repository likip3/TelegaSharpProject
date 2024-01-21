namespace TelegaSharpProject.Domain.Interfaces;

public interface IUserInfo
{
    public long Id { get; }
        
    public long ChatId { get; }

    public string UserName { get; }

    public DateTime? RegisteredAt { get; }

    public long? Points { get; }
        
    public int? CompletedTasks { get; }
}
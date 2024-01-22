using TelegaSharpProject.Domain.Interfaces;

namespace TelegaSharpProject.Application.Bot.Chats.Interfaces;

public interface ITaskChatInfo
{
    public ITaskInfo? LastTask { get; }
    public int Page { get; }

    public int TrySetPage(int delta);
    public void Reset();
    public void SetTask(ITaskInfo taskInfo);
}
using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Domain.Interfaces;

namespace TelegaSharpProject.Application.Bot.Chats.Interfaces;

public interface ITaskChatInfo
{
    public ITaskInfo? LastTask { get; }
    public int Page { get; }
    public int TrySetDeltaPage(int delta);
    public void Reset();
    public void SetTask(ITaskInfo taskInfo);
    public void SetFrom(From from);
    public From From { get; }
}
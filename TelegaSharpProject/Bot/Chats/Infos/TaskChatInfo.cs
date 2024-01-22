using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Domain.Interfaces;

namespace TelegaSharpProject.Application.Bot.Chats.Infos;

public class TaskChatInfo : ITaskChatInfo
{
    public int TrySetDeltaPage(int delta)
    {
        if (Page + delta < 0)
            return Page;

        Page += delta;
        return Page;
    }

    public void Reset()
    {
        Page = 0;
    }

    public void SetTask(ITaskInfo taskInfo)
    {
        LastTask = taskInfo;
    }

    public void SetTaskFrom(TaskFrom taskFrom)
    {
        TaskFrom = taskFrom;
    }

    public TaskFrom TaskFrom { get; private set; }
    public ITaskInfo? LastTask { get; private set; }
    public int Page { get; private set; }
}
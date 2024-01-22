using TelegaSharpProject.Application.Bot.Chats.Enums;

namespace TelegaSharpProject.Application.Bot.Chats.Interfaces;

public interface ISolverChat
{
    public ChatState ChatState { get; }
    public InputType InputType { get; }
    public long ChatId { get; }
    public void Reset();
    public void SetToInputState(InputType inputType);
    public void SetToCommandState();
    public int PageNum { get; }
    public ITaskChatInfo TaskChatInfo { get; }
    public IAnswerChatInfo AnswerChatInfo { get; }
}
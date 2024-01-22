using TelegaSharpProject.Application.Bot.Chats.Enums;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Chats.Interfaces;

public interface ISolverChat
{
    public ChatState ChatState { get; }
    public InputType InputType { get; }
    public Chat Chat { get; }
    public void Reset();
    public bool TrySetPage(int page);
    public void SetToInputState(InputType inputType);
    public void SetToCommandState();
    public int PageNum { get; }
    public ITaskChatInfo TaskChatInfo { get; }
    
}
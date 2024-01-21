using TelegaSharpProject.Application.Bot.Chats.Enums;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Chats.Interfaces;

public interface ISolverChat
{
    public ChatState ChatState { get; }
    public Chat Chat { get; }
    public Task ToTitle();
    public bool TrySetPage(int page);
    public void SetToInputState();
    public void SetToCommandState();
    public int PageNum { get; }
}
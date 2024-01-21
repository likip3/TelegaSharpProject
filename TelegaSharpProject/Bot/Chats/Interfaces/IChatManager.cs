using TelegaSharpProject.Application.Bot.Chats.Enums;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Chats.Interfaces;

public interface IChatManager
{
    public bool TryGetChat(long chatId, out ISolverChat chat);
    public Task<ISolverChat> StartChat(Chat chat, User user);
}
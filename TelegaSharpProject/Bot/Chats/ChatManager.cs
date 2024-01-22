using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Chats;

public class ChatManager : IChatManager
{
    private readonly Dictionary<long, ISolverChat> _chats = new();

    public bool TryGetChat(long chatId, out ISolverChat chat)
    {
        return _chats.TryGetValue(chatId, out chat);
    }

    public ISolverChat GetChat(Chat chat)
    {
        if (TryGetChat(chat.Id, out var solverChat))
            return solverChat;

        var sChat = new SolverChat(chat);
        _chats.Add(chat.Id, sChat);

        return sChat;
    }
}
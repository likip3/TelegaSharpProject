using TelegaSharpProject.Application.Bot.Chats.Interfaces;

namespace TelegaSharpProject.Application.Bot.Chats;

public class ChatManager : IChatManager
{
    private readonly Dictionary<long, ISolverChat> _chats = new();

    public bool TryGetChat(long chatId, out ISolverChat chat)
    {
        return _chats.TryGetValue(chatId, out chat);
    }

    public ISolverChat GetChat(long chatId)
    {
        if (TryGetChat(chatId, out var solverChat))
            return solverChat;

        var sChat = new SolverChat(chatId);
        _chats.Add(chatId, sChat);

        return sChat;
    }
}
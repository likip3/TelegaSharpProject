using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Chats;

public class ChatManager : IChatManager
{
    private readonly Lazy<IButtonManager> _buttonManagerFactory;
    private readonly Lazy<ITelegramBotClient> _botClientFactory;
    private readonly Dictionary<long, ISolverChat> _chats = new();

    public ChatManager(Lazy<IButtonManager> buttonManagerFactory, Lazy<ITelegramBotClient> botClientFactory)
    {
        _buttonManagerFactory = buttonManagerFactory;
        _botClientFactory = botClientFactory;
    }
    
    public bool TryGetChat(long chatId, out ISolverChat chat)
    {
        return _chats.TryGetValue(chatId, out chat);
    }

    public ISolverChat StartChat(Chat chat)
    {
        if (TryGetChat(chat.Id, out var solverChat))
            return solverChat;

        var sChat = new SolverChat(chat, _buttonManagerFactory.Value, _botClientFactory.Value);
        _chats.Add(chat.Id, sChat);

        return sChat;
    }
}
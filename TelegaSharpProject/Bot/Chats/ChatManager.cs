using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Domain.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Chats;

public class ChatManager : IChatManager
{
    private readonly Lazy<IButtonManager> _buttonManagerFactory;
    private readonly Lazy<ITelegramBotClient> _botClientFactory;
    private readonly IDBWorker _worker;
    private readonly Dictionary<long, ISolverChat> _chats = new();

    public ChatManager(
        Lazy<IButtonManager> buttonManagerFactory, 
        Lazy<ITelegramBotClient> botClientFactory,
        IDBWorker worker)
    {
        _buttonManagerFactory = buttonManagerFactory;
        _botClientFactory = botClientFactory;
        _worker = worker;
    }
    
    public bool TryGetChat(long chatId, out ISolverChat chat)
    {
        return _chats.TryGetValue(chatId, out chat);
    }

    public async Task<ISolverChat> StartChat(Chat chat, User user)
    {
        if (TryGetChat(chat.Id, out var solverChat))
            return solverChat;

        var sChat = new SolverChat(chat, _buttonManagerFactory.Value, _botClientFactory.Value);
        _chats.Add(chat.Id, sChat);

        await _worker.TryRegisterUser(new UserInfo(user.Id, user.Username, chat.Id));

        return sChat;
    }
}
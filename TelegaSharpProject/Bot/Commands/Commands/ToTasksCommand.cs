using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("/tasks")]
public class ToTasksCommand : Command
{
    private readonly Lazy<ITelegramBotClient> _botClientFactory;
    private readonly IChatManager _chatManager;

    public ToTasksCommand(Lazy<ITelegramBotClient> botClientFactory, IChatManager chatManager)
    {
        _botClientFactory = botClientFactory;
        _chatManager = chatManager;
    }


    public override async void Execute(Message message)
    {
        if (!_chatManager.TryGetChat(message.Chat.Id, out var chat))
            return;

        chat.TrySetPage(1);
        
        await _botClientFactory.Value.SendTextMessageAsync(
            message.Chat.Id,
            MessageBuilder.GetTasks(1),
            replyMarkup: MessageBuilder.GetTasksMarkup());
    }
}
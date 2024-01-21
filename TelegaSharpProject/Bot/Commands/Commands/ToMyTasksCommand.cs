using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;


[SolverCommand("/mytasks")]
public class ToMyTasksCommand : Command
{
    private readonly Lazy<ITelegramBotClient> _botClientFactory;
    
    public ToMyTasksCommand(
        Lazy<ITelegramBotClient> botClientFactory,
        IMessageBuilder messageBuilder) : base(messageBuilder)
    {
        _botClientFactory = botClientFactory;
    }
    
    public override async void Execute(Message message)
    {
        await _botClientFactory.Value.SendTextMessageAsync(
            message.Chat.Id,
            MessageBuilder1.GetMyTasks(message.From));
    }
}
using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("/ab")]
public class AbobusCommand : Command
{
    private readonly Lazy<ITelegramBotClient> _botClientFactory;
    
    public AbobusCommand(
        Lazy<ITelegramBotClient> botClientFactory,
        IMessageBuilder messageBuilder) : base(messageBuilder)
    {
        _botClientFactory = botClientFactory;
    }
    
    public override async void Execute(Message message)
    {
        await _botClientFactory.Value.SendTextMessageAsync(
            message.Chat.Id, 
            "Сам такой");
    }
}
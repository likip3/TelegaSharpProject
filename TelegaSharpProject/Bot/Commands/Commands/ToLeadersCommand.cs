using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("/leaders")]
public class ToLeadersCommand : Command
{
    private readonly Lazy<ITelegramBotClient> _botClientFactory;
    
    public ToLeadersCommand(
        Lazy<ITelegramBotClient> botClientFactory,
        IMessageBuilder messageBuilder) : base(messageBuilder)
    {
        _botClientFactory = botClientFactory;
    }
    
    public override async void Execute(Message message)
    {
        await _botClientFactory.Value.SendTextMessageAsync(
            message.Chat.Id,
            await MessageBuilder.GetLeaderBoard());
    }
}
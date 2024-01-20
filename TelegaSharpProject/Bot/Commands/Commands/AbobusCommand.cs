using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[Attributes.SolverCommand("/ab")]
public class AbobusCommand : ICommand
{
    private readonly Lazy<ITelegramBotClient> _botClientFactory;
    
    public AbobusCommand(Lazy<ITelegramBotClient> botClientFactory)
    {
        _botClientFactory = botClientFactory;
    }
    
    public async void Execute(Message message)
    {
        await _botClientFactory.Value.SendTextMessageAsync(
            message.Chat.Id, 
            "Сам такой");
    }
}
using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[Attributes.SolverCommand("input", false)]
public class InputCommand : ICommand
{
    private readonly Lazy<ITelegramBotClient> _botFactory;
    
    public InputCommand(Lazy<ITelegramBotClient> botFactory)
    {
        _botFactory = botFactory;
    }
    
    public async void Execute(Message message)
    {
        await _botFactory.Value.SendTextMessageAsync(
            message.Chat.Id,
            "Записал!"
        );
    }
}
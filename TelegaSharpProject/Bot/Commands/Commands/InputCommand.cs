using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("input", false)]
public class InputCommand : Command
{
    private readonly Lazy<ITelegramBotClient> _botFactory;
    
    public InputCommand(Lazy<ITelegramBotClient> botFactory)
    {
        _botFactory = botFactory;
    }
    
    public override async void Execute(Message message)
    {
        await _botFactory.Value.SendTextMessageAsync(
            message.Chat.Id,
            "Записал!"
        );
    }
}
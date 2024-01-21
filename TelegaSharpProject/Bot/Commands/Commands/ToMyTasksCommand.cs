using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;


[SolverCommand("/mytasks")]
public class ToMyTasksCommand : Command
{
    private readonly Lazy<ITelegramBotClient> _botClientfactory;
    
    public ToMyTasksCommand(Lazy<ITelegramBotClient> botClientfactory)
    {
        _botClientfactory = botClientfactory;
    }
    
    public override async void Execute(Message message)
    {
        await _botClientfactory.Value.SendTextMessageAsync(
            message.Chat.Id,
            MessageBuilder.GetMyTasks(message.From));
    }
}